using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    BlasterData blasterData;
    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        blasterData = (BlasterData)towerData;
        upgradeCost = blasterData.BurstUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                blasterData.BurstCount = blasterData.BurstUpgradedValues[upgradeOrder - 1];
                break;
            case 2:
                blasterData.BurstCount = blasterData.BurstUpgradedValues[upgradeOrder - 1];
                break;
            case 3:
                blasterData.BurstCount = blasterData.BurstUpgradedValues[upgradeOrder - 1];
                break;
            case 4:
                blasterData.canDoubleBarrel = true;
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
