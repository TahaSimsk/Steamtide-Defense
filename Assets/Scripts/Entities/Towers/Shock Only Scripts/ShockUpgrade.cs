using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    ShockData shockData;
    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        shockData = (ShockData)towerData;
        upgradeCost = shockData.shockUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                shockData.projectileCount = shockData.upgradedProjectileCount1;
                break;
            case 2:
                shockData.slowAmount = shockData.upgradedSlowAmount;
                break;
            case 3:
                shockData.slowDuration = shockData.upgradedSlowDuration;
                break;
            case 4:
                shockData.projectileCount = shockData.upgradedProjectileCount2;
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
