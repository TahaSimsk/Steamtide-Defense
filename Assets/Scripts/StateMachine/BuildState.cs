using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildState : BaseState
{
    GameStateManager gameStateManager;



    public override void EnterState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
        CreateTower();
    }
    public override void ExitState()
    {
        DestroyTowers();
    }


    public override void UpdateState(GameStateManager gameStateManager)
    {
        PositionHoverTower();
    }

    //instantiated towers
    GameObject instHoverTower;
    GameObject instHoverUnplaceableTower;
    GameObject instTower;


    float currentTowerMoneyCost;
    Data currentTowerData;

    void CreateTower()
    {
        //gets the button from currently pressed button
        Button button = gameStateManager.pressedButton;

        //gets the tower data from the ButtonInfo which is a component of button
        DataTower dataTower = (DataTower)button.GetComponent<ButtonColorChanger>().data;

        //instantiation of towers from the data
        instHoverTower = Object.Instantiate(dataTower.towerHoverPrefab, Input.mousePosition, Quaternion.identity);

        instHoverUnplaceableTower = Object.Instantiate(dataTower.towerNPHoverPrefab, Input.mousePosition, Quaternion.identity);
        instHoverUnplaceableTower.SetActive(false);

        instTower = Object.Instantiate(dataTower.objectPrefab, Input.mousePosition, Quaternion.identity);
        instTower.SetActive(false);
        //---------------------------------

        //gets the cost of the tower
        currentTowerMoneyCost = dataTower.objectCost_MoneyDrop;
    }







    void PositionHoverTower()
    {
        //before showing and placing anything check if mouse is over a ui object, if so, deactivate towers and return
        if (gameStateManager.isHoveringUI)
        {
            instHoverTower.SetActive(false);
            instHoverUnplaceableTower.SetActive(false);
            return;
        }

        //checks if mouse is over a placeable layer when hovering
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gameStateManager.placeableLayer))
        {
            //stores tileInfo of the tile at the mouse pos
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();

            if (tileInfo == null) return;

            //check if there is enough money to place and tile is currently empty and placeable
            if (gameStateManager.moneyManager.IsPlaceable(currentTowerMoneyCost) && tileInfo.placeable)
            {
                //don't show unplaceable tower and show placeable tower
                instHoverUnplaceableTower.SetActive(false);
                instHoverTower.SetActive(true);

                //set the position of the placeable tower at the tile of mousePos
                instHoverTower.transform.position = hit.transform.position + gameStateManager.offsetForTowerPlacement;

                PlaceTower(hit.transform.position, ref tileInfo.placeable);

            }
            else
            {
                //if there isn't enough money or tile is not placeable show "unplaceable" tower when hovered
                instHoverUnplaceableTower.SetActive(true);
                instHoverTower.SetActive(false);

                instHoverUnplaceableTower.transform.position = hit.transform.position + gameStateManager.offsetForTowerPlacement;
            }
        }
        else
        {
            //if mouse is not hovering a placeable layer, deactivate currently showed tower
            instHoverTower.SetActive(false);
            instHoverUnplaceableTower.SetActive(false);
        }
    }

    void PlaceTower(Vector3 pos, ref bool info)
    {
        //check if lmb is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //if so, set the position of the tower at the tile's position and activate tower
            instTower.transform.position = pos + gameStateManager.offsetForTowerPlacement;
            instTower.SetActive(true);

            //mark the tile as not placeable
            info = false;

            gameStateManager.moneyManager.DecreaseMoney(currentTowerMoneyCost);

            //trigger a tower placed event
            EventManager.OnTowerPlaced(instTower);

            //create new tower to be placed at the next cycle and deactivate it
            instTower = Object.Instantiate(instTower, Input.mousePosition, Quaternion.identity);
            instTower.SetActive(false);

        }
    }

    void DestroyTowers()
    {
        Object.Destroy(instTower);
        instTower = null;
        Object.Destroy(instHoverTower);
        instHoverTower = null;
        Object.Destroy(instHoverUnplaceableTower);
        instHoverUnplaceableTower = null;

    }
}
