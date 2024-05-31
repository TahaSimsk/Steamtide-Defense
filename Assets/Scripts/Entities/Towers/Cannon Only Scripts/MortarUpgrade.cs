using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MortarUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    [SerializeField] GameObject mortarButton;
    CannonData cannonData;

    protected override void Awake()
    {
        base.Awake();
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        cannonData = (CannonData)towerData;

        upgradeMoneyCost = cannonData.mortarUpgradeMoneyCosts[upgradeOrder - 1];
        upgradeWoodCost = cannonData.mortarUpgradeWoodCosts[upgradeOrder - 1];
        upgradeRockCost = cannonData.mortarUpgradeRockCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                cannonData.canMortar = true;
                mortarButton.SetActive(true);
                break;
            case 2:
                cannonData.bombRadius = cannonData.upgradedBombRadius;
                break;
            case 3:
                cannonData.numOfMissilesToLaunch = cannonData.upgradedNumOfMissiles;
                break;
            case 4:
                cannonData.MortarCooldown = cannonData.mortarUpgradedCooldown;
                break;
        }


        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
