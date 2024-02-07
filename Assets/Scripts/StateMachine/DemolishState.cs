using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolishState : BaseState
{
    GameStateManager gameStateManager;
    CursorManager cursorManager;
    MoneyManager moneyManager;
    TooltipManager tooltipManager;
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


            //get the cost from demolishable object
            float moneyCost = hit.transform.GetComponent<DemolishInfo>().dataDemolish.objectCost_MoneyDrop;

            //check if there is enough money to demolish
            if (gameStateManager.moneyManager.IsPlaceable(moneyCost))
            {

                //show tooltip
                tooltipManager.ShowTip("Cost To Demolish: " + moneyCost, Input.mousePosition, true);


                //check if player presses lmb
                if (Input.GetMouseButtonDown(0))
                {

                    //decrease and update money
                    moneyManager.DecreaseMoney(moneyCost);

                    EventManager.OnDemolished(hit.transform.gameObject);

                    //instantiate normal tile. can instantiate different tiles. just need to specify them in the demolishInfo script and get the tile from there.
                    Object.Instantiate(gameStateManager.tile, hit.transform.position, Quaternion.identity);


                    //destroy demolishable tile
                   Object.Destroy(hit.transform.gameObject);

                }
            }
            else
            {
                //if there isn't enough money to demolish, show different tooltip when hovered over demolishable object
                tooltipManager.ShowTip("Need " + moneyCost + " To Demolish", Input.mousePosition, false);
            }


        }
        else
        {
            //if mouse is not over demolishable object, set cursor to default, and disable shown tips 
            cursorManager.SetCursor(null);
            tooltipManager.DisableTip();
        }

    }
}
