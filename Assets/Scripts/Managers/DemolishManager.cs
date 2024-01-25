using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class DemolishManager : MonoBehaviour
{
    [SerializeField] LayerMask demolishLayer;
    [SerializeField] GameObject tile;

    FlagManager flagManager;
    CursorManager cursorManager;
    MoneySystem moneySystem;
    TooltipManager tooltipManager;

    void Start()
    {
        flagManager = FindObjectOfType<FlagManager>();
        cursorManager = FindObjectOfType<CursorManager>();
        moneySystem = FindObjectOfType<MoneySystem>();
        tooltipManager = FindObjectOfType<TooltipManager>();
    }
    
    

    void Update()
    {
        DemolishObjects();
    }


    void DemolishObjects()
    {
        if (flagManager.demolishMode)
        {

            //get the ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            //check if mouse is over demolishable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, demolishLayer))
            {

                //when mouse hovers demolishable object, change cursor
                cursorManager.SetCursor(cursorManager.demolishCursorTexture);


                //get the cost from demolishable object
                float moneyCost = hit.transform.GetComponent<DemolishInfo>().moneyCost;

                //check if there is enough money to demolish
                if (moneySystem.IsPlaceable(moneyCost))
                {

                    //show tooltip
                    tooltipManager.ShowTip("Cost To Demolish: " + moneyCost, Input.mousePosition, true);


                    //check if player presses lmb
                    if (Input.GetMouseButtonDown(0))
                    {

                        //decrease and update money
                        moneySystem.DecreaseMoney(moneyCost);
                        moneySystem.UpdateMoneyDisplay();


                        //instantiate normal tile
                        Instantiate(tile, hit.transform.position, Quaternion.identity);


                        //destroy demolishable tile
                        Destroy(hit.transform.gameObject);

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
}
