using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMPUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    [SerializeField] GameObject empCanvas;
    ShockData shockData;
    protected override void Awake()
    {
        base.Awake();
        maxUpgradeCount = 1;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        shockData = (ShockData)towerData;
        upgradeMoneyCost = shockData.empUpgradeMoneyCosts[upgradeOrder - 1];
        upgradeWoodCost = shockData.empUpgradeWoodCosts[upgradeOrder - 1];
        upgradeRockCost = shockData.empUpgradeRockCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                empCanvas.SetActive(true);
                break;
            case 2:
                shockData.freezeDuration = shockData.upgradedFreezeDuration;
                break;
            case 3:
                shockData.empCooldown = shockData.upgradedEmpCooldown;
                break;
            case 4:
                shockData.canFreezeBosses = true;
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
