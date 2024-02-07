//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class Upgrade : MonoBehaviour
//{
//    [Header("Weapon Attributes")]
//    [SerializeField] float upgradedProjectileSpeed;
//    [SerializeField] float upgradedProjectileDmg;
//    [SerializeField] float upgradedProjectileLife;
//    [SerializeField] float upgradedShootingDelay;
//    [SerializeField] float upgradedTowerRange;
//    [SerializeField] float upgradeCost;


//    //Tower tower;
//    MeshRenderer meshRenderer;
//    MoneySystem moneySystem;
//    TooltipManager tooltipManager;
//    FlagManager flagManager;
//    CursorManager cursorManager;

//    bool upgraded;

//    private void Start()
//    {
//        tooltipManager = FindObjectOfType<TooltipManager>();
//        cursorManager = FindObjectOfType<CursorManager>();
//        moneySystem = FindObjectOfType<MoneySystem>();
//        flagManager = FindObjectOfType<FlagManager>();
//        meshRenderer = GetComponent<MeshRenderer>();
//        //tower = GetComponent<Tower>();
//    }

//    private void OnMouseDown()
//    {
//        if (flagManager.currentMode == FlagManager.CurrentMode.upgrade && moneySystem.IsPlaceable(upgradeCost) && !upgraded)
//        {
//            UpgradeWeapon();
//            //moneySystem.DecreaseMoney(upgradeCost);
//            //moneySystem.UpdateMoneyDisplay();
//        }
//    }

//    //void OnMouseOver()
//    //{
//    //    if (flagManager.currentMode == FlagManager.CurrentMode.upgrade)
//    //    {
//    //        cursorManager.SetCursor(cursorManager.upgradeCursorTexture);

//    //        if (upgraded)
//    //        {
//    //            tooltipManager.ShowTip("Can't upgrade further!", Input.mousePosition, false);
//    //        }
//    //        else
//    //        {
//    //            tooltipManager.ShowTip("Upgrade Cost: " + upgradeCost + "$", Input.mousePosition, moneySystem.IsPlaceable(upgradeCost));
//    //        }
//    //    }
//    //}

//    //private void OnMouseExit()
//    //{
//    //    if (flagManager.currentMode == FlagManager.CurrentMode.upgrade)
//    //    {
//    //        cursorManager.SetCursor(null);
//    //        tooltipManager.DisableTip();
//    //    }
//    //}

//    void UpgradeWeapon()
//    {
//        foreach (var material in meshRenderer.materials)
//        {
//            material.color = Color.red;
//        }

//        //tower.SetWeaponUpgradeAttributes(upgradedProjectileSpeed, upgradedProjectileDmg, upgradedProjectileLife, upgradedShootingDelay, upgradedTowerRange);
//        //upgraded = true;
//    }
//}
