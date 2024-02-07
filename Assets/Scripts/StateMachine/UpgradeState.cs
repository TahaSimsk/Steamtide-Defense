using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeState : BaseState
{
    GameStateManager gameStateManager;
    MoneySystem moneySystem;
    TooltipManager tooltipManager;
    CursorManager cursorManager;
    public override void EnterState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState(GameStateManager gameStateManager)
    {

    }

    void Upgrade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gameStateManager.upgradeLayer))
        {
            
        }
    }



}
