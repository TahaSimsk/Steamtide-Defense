using System.Collections;
using TMPro;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class DemolishState : BaseState
{
    GameStateManager gameStateManager;
    CursorManager cursorManager;
    MoneyManager moneyManager;

    DemolishInfo hoveredTileDemolishInfo;
    float hoveredTileDemolishCost;

    public override void EnterState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
        cursorManager = gameStateManager.cursorManager;
        moneyManager = gameStateManager.moneyManager;
    }

    public override void ExitState()
    {
        cursorManager.SetCursor(null);
        TooltipManager.Instance.DisableTip();
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        DemolishObjects();
    }

    void DemolishObjects()
    {
        Ray ray = gameStateManager.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if mouse is over demolishable object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gameStateManager.demolishLayer))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            //when mouse hovers demolishable object, change cursor
            cursorManager.SetCursor(cursorManager.demolishCursorTexture);

            hoveredTileDemolishInfo = hit.transform.GetComponent<DemolishInfo>();
            //get the cost from object
            hoveredTileDemolishCost = hoveredTileDemolishInfo.DemolishCost;

            //check if there is enough money to demolish
            if (gameStateManager.moneyManager.IsAffordable(hoveredTileDemolishCost))
            {
                //show tooltip
                ShowTipWhenHoveredDemolishableObject();

                //when player presses lmb start demolish sequence
                if (Input.GetMouseButtonDown(0))
                {
                    gameStateManager.StartCoroutine(StartDemolishSequence());
                }
            }
            else
            {
                //if there isn't enough money to demolish, show different tooltip when hovered over demolishable object
                TooltipManager.Instance.ShowTip("Need " + hoveredTileDemolishCost + " To Demolish", Input.mousePosition, false);
            }
        }
        else
        {
            //if mouse is not over demolishable object, set cursor to default, and disable shown tips 
            cursorManager.SetCursor(null);
            TooltipManager.Instance.DisableTip();
        }

    }

    void ShowTipWhenHoveredDemolishableObject()
    {
        string resourceNamesAndDropAmounts = "";

        foreach (var resource in hoveredTileDemolishInfo.Resources)
        {
            resourceNamesAndDropAmounts += $"\n {resource.NameOfResource}: {resource.DropAmount}";
        }

        string tipMessage = $"Cost To Demolish: {hoveredTileDemolishCost}\n RESOURCES: {resourceNamesAndDropAmounts}";
        TooltipManager.Instance.ShowTip(tipMessage, Input.mousePosition, true);
    }


    public IEnumerator StartDemolishSequence()
    {
        //if selected tile's state is not gatherable return
        if (hoveredTileDemolishInfo.CurrentGatherState != GatherState.Gatherable)
        {
            yield break;
        }

        //cashe the required components so that they are locked in
        DemolishInfo selectedTileDemolishInfo = hoveredTileDemolishInfo;
        float selectedTileDemolishCost = hoveredTileDemolishCost;


        //decrease money
        moneyManager.DecreaseMoney(selectedTileDemolishCost);

        //change state from gatherable to gathering so that when clicked again during the waiting period, algorithm doesnt start again
        selectedTileDemolishInfo.CurrentGatherState = GatherState.Gathering;

        //set the timer to the selected tile's timer
        //float timer = selectedTileDemolishData.GatherDuration;
        float timer = selectedTileDemolishInfo.DurationToDemolish;

        //activate countdown slider
        selectedTileDemolishInfo.slider.gameObject.SetActive(true);

        //start countdown && wait
        while (timer >= 0)
        {
            timer -= Time.deltaTime;

            //update countdown slider
            //selectedTileDemolishInfo.slider.value = timer / selectedTileDemolishData.GatherDuration;

            selectedTileDemolishInfo.slider.value = timer / selectedTileDemolishInfo.DurationToDemolish;

            //TODO: start cutting or mining animation
            yield return null;
        }

        //after the waiting period is over, deactivate countdown slider
        selectedTileDemolishInfo.slider.gameObject.SetActive(false);

        //create an empty string to store all the resource info 
        string resourceNamesAndDropAmounts = "";

        //cycle through each resource to be dropped and initialize drop func which adds the resources to the bank and also store the name and drop amount of that resource
        foreach (var item in selectedTileDemolishInfo.Resources)
        {
            item.Drop();
            resourceNamesAndDropAmounts += $"{item.NameOfResource}: +{item.DropAmount}\n";
        }

        //when the resources are added to the bank, spawn a text in that location to show which resource and how many resources dropped from that tile
        gameStateManager.StartCoroutine(SpawnFlyingText(resourceNamesAndDropAmounts, selectedTileDemolishInfo.transform.position));

        //raise an ondemolished event
        gameStateManager.onDemolished.RaiseEvent(selectedTileDemolishInfo.gameObject);

        //spawn normal tile
        Object.Instantiate(selectedTileDemolishInfo.NormalTile, selectedTileDemolishInfo.transform.position, Quaternion.identity);

        //destroy selected tile
        Object.Destroy(selectedTileDemolishInfo.gameObject);
    }

    IEnumerator SpawnFlyingText(string text, Vector3 textPos)
    {

        //GameObject go = gameStateManager.objectPool.GetObject();
        GameObject go = gameStateManager.objectPool.GetObject(gameStateManager.resourceFloatingText.hashCode, gameStateManager.resourceFloatingText.objectToPoolPrefab);
        go.GetComponentInChildren<TextMeshProUGUI>().text = text;
        go.transform.position = textPos;
        go.SetActive(true);

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            go.transform.Translate(Vector3.up * Time.deltaTime * 20f);
            yield return null;
        }
        go.SetActive(false);
    }
}
