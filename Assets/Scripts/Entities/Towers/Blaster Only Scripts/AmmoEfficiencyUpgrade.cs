using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoEfficiencyUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    [SerializeField] AmmoManager ammoManager;
    BlasterData blasterData;
    protected override void Awake()
    {
        base.Awake();
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        blasterData = (BlasterData)towerData;
        upgradeMoneyCost = blasterData.AmmoUpgradeMoneyCosts[upgradeOrder - 1];
        upgradeWoodCost = blasterData.AmmoUpgradeWoodCosts[upgradeOrder - 1];
        upgradeRockCost = blasterData.AmmoUpgradeRockCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                blasterData.AmmoEfficiency = blasterData.AmmoEfficiencyUpgradedValues[upgradeOrder - 1];
                break;
            case 2:
                blasterData.AmmoEfficiency = blasterData.AmmoEfficiencyUpgradedValues[upgradeOrder - 1];
                break;
            case 3:
                blasterData.AmmoEfficiency = blasterData.AmmoEfficiencyUpgradedValues[upgradeOrder - 1];
                break;
            case 4:
                ammoManager.UpgradeAmmoCapacity(blasterData.UpgradedAmmoCapacity);
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
