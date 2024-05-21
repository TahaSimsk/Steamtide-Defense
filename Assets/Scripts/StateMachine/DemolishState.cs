using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemolishState : BaseState
{
    GameStateManager gameStateManager;
    CursorManager cursorManager;
    MoneyManager moneyManager;
    TooltipManager tooltipManager;

    DemolishInfo hoveredTileDemolishInfo;
    DataDemolish hoveredTileDemolishData;
    float hoveredTileDemolishCost;
    public override void EnterState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
        cursorManager = gameStateManager.cursorManager;
        moneyManager = gameStateManager.moneyManager;
        tooltipManager = gameStateManager.tooltipManager;
    }

    public override void ExitState()
    {
        cursorManager.SetCursor(null);
        tooltipManager.DisableTip();
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        DemolishObjects();
    }

    void DemolishObjects()
    {
        //get the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //check if mouse is over demolishable object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gameStateManager.demolishLayer))
        {

            //when mouse hovers demolishable object, change cursor
            cursorManager.SetCursor(cursorManager.demolishCursorTexture);

            hoveredTileDemolishInfo = hit.transform.GetComponent<DemolishInfo>();
            hoveredTileDemolishData = hoveredTileDemolishInfo.dataDemolish;
            //get the cost from demolishable object
            hoveredTileDemolishCost = hoveredTileDemolishData.objectCost_MoneyDrop;

            //check if there is enough money to demolish
            if (gameStateManager.moneyManager.IsAffordable(hoveredTileDemolishCost))
            {
                //show tooltip
                tooltipManager.ShowTip("Cost To Demolish: " + hoveredTileDemolishCost, Input.mousePosition, true);

                //check if player presses lmb
                if (Input.GetMouseButtonDown(0))
                {
                    hoveredTileDemolishInfo.StartCoroutine(StartDemolishSequence());
                }
            }
            else
            {
                //if there isn't enough money to demolish, show different tooltip when hovered over demolishable object
                tooltipManager.ShowTip("Need " + hoveredTileDemolishCost + " To Demolish", Input.mousePosition, false);
            }
        }
        else
        {
            //if mouse is not over demolishable object, set cursor to default, and disable shown tips 
            cursorManager.SetCursor(null);
            tooltipManager.DisableTip();
        }

    }


    public IEnumerator StartDemolishSequence()
    {
        if (hoveredTileDemolishInfo.CurrentGatherState != GatherState.Gatherable)
        {
            yield break;
        }
        DemolishInfo selectedTileDemolishInfo = hoveredTileDemolishInfo;
        DataDemolish selectedTileDemolishData = hoveredTileDemolishData;
        float selectedTileDemolishCost = hoveredTileDemolishCost;
        //decrease money
        moneyManager.DecreaseMoney(selectedTileDemolishCost);

        //change state from gatherable to gathering
        selectedTileDemolishInfo.CurrentGatherState = GatherState.Gathering;
        float timer = selectedTileDemolishData.GatherDuration;
        selectedTileDemolishInfo.slider.gameObject.SetActive(true);
        //start countdown && wait
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            selectedTileDemolishInfo.slider.value = timer / selectedTileDemolishData.GatherDuration;
            //start cutting or mining animation
            yield return null;
        }
        selectedTileDemolishInfo.slider.gameObject.SetActive(false);
        //add resources to the bank
        foreach (var item in selectedTileDemolishInfo.Resources)
        {
            item.Drop();
        }

        gameStateManager.onDemolished.RaiseEvent(selectedTileDemolishInfo.gameObject);
        //spawn normal tile
        Object.Instantiate(selectedTileDemolishInfo.NormalTile, selectedTileDemolishInfo.transform.position, Quaternion.identity);
        //destroy selected tile
        Object.Destroy(selectedTileDemolishInfo.gameObject);
    }
}
