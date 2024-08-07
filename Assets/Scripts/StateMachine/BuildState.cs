using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildState : BaseState
{
    GameStateManager gameStateManager;

    GameObject instPlaceableHoverTower;
    GameObject instUnplaceableHoverTower;
    GameObject instTower;

    float currentTowerMoneyCost;
    bool tilePlaceable;


    public override void EnterState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
        gameStateManager.StartCoroutine(CreateTower());
    }


    public override void ExitState()
    {
        DestroyTowers();
    }


    public override void UpdateState(GameStateManager gameStateManager)
    {
        HandleTowerHoverAndPlacement();
    }


    IEnumerator CreateTower()
    {
        //gets the button from currently pressed button
        Button button = gameStateManager.pressedButton;

        TowerData dataTower = button.GetComponent<ButtonColorChanger>().towerGameData;

        //instantiation of towers from the data
        instPlaceableHoverTower = Object.Instantiate(dataTower.TowerHoverPrefab, Input.mousePosition, Quaternion.identity);

        instUnplaceableHoverTower = Object.Instantiate(dataTower.TowerNPHoverPrefab, Input.mousePosition, Quaternion.identity);
        instUnplaceableHoverTower.SetActive(false);

        instTower = Object.Instantiate(dataTower.TowerPrefab, Input.mousePosition, Quaternion.identity);

        yield return null;

        HandleRangeIndicator();

        instTower.SetActive(false);

        //gets the cost of the tower
        currentTowerMoneyCost = dataTower.TowerPlacementCost;
    }


    private void HandleRangeIndicator()
    {
        TargetScanner targetScanner = instTower.GetComponentInChildren<TargetScanner>();
        if (targetScanner != null)
        {
            GameObject rangeIndicator1 = Object.Instantiate(gameStateManager.RangeIndicator, instPlaceableHoverTower.transform.position, Quaternion.identity, instPlaceableHoverTower.transform);

            rangeIndicator1.transform.localScale = targetScanner.transform.localScale;


            GameObject rangeIndicator2 = Object.Instantiate(gameStateManager.RangeIndicator, instUnplaceableHoverTower.transform.position, Quaternion.identity, instUnplaceableHoverTower.transform);

            rangeIndicator2.transform.localScale = targetScanner.transform.localScale;
        }
    }


    void HandleTowerHoverAndPlacement()
    {
        //before showing and placing anything check if mouse is over a ui object, if so, deactivate towers and return
        if (EventSystem.current.IsPointerOverGameObject())
        {
            instPlaceableHoverTower.SetActive(false);
            instUnplaceableHoverTower.SetActive(false);
            return;
        }

        //checks if mouse is over a placeable layer when hovering
        Ray ray = gameStateManager.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //stores tileInfo of the tile at the mouse pos
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();

            if (tileInfo == null)
            {
                return;
            }

            Collider[] colliders = Physics.OverlapBox(hit.point, Vector3.one * 3, Quaternion.identity, ~gameStateManager.ignoreLayers);

            foreach (var item in colliders)
            {
                if ((1 << item.gameObject.layer) != gameStateManager.placeableLayer.value)
                {
                    tilePlaceable = false;
                    break;
                }
                else
                {
                    tilePlaceable = true;
                }
            }

            //check if there is enough money to place and tile is currently empty and placeable
            if (gameStateManager.moneyManager.IsAffordable(currentTowerMoneyCost) && tilePlaceable)
            {
                //don't show unplaceable tower and show placeable tower
                instUnplaceableHoverTower.SetActive(false);
                instPlaceableHoverTower.SetActive(true);

                //set the position of the placeable tower at the tile of mousePos
                instPlaceableHoverTower.transform.position = hit.point + gameStateManager.offsetForTowerPlacement;

                PlaceTower(hit.point, ref tileInfo.placeable);
            }
            else
            {
                //if there isn't enough money or tile is not placeable show "unplaceable" tower when hovered
                instUnplaceableHoverTower.SetActive(true);
                instPlaceableHoverTower.SetActive(false);

                instUnplaceableHoverTower.transform.position = hit.point + gameStateManager.offsetForTowerPlacement;
            }
        }
        else
        {
            //if mouse is not hovering a placeable layer, deactivate currently showed tower
            instPlaceableHoverTower.SetActive(false);
            instUnplaceableHoverTower.SetActive(false);
        }
    }


    void PlaceTower(Vector3 pos, ref bool info)
    {
        //check if lmb is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //if so, set the position of the tower at the tile's position and activate tower
            instTower.transform.position = pos;
            instTower.SetActive(true);

            //mark the tile as unplaceable
            info = false;

            //trigger a tower placed event
            gameStateManager.onTowerPlaced.RaiseEvent(instTower, currentTowerMoneyCost);

            //decrease money
            gameStateManager.moneyManager.DecreaseMoney(currentTowerMoneyCost);

            //create new tower to be placed at the next cycle and deactivate it
            instTower = Object.Instantiate(instTower, Input.mousePosition, Quaternion.identity);
            instTower.SetActive(false);

        }
    }

    void DestroyTowers()
    {
        Object.Destroy(instTower);
        instTower = null;
        Object.Destroy(instPlaceableHoverTower);
        instPlaceableHoverTower = null;
        Object.Destroy(instUnplaceableHoverTower);
        instUnplaceableHoverTower = null;
    }
}
