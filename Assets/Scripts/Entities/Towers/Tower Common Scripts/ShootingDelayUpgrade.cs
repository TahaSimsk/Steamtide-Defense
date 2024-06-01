using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDelayUpgrade : UpgradeBaseClass
{
    public Action<object> OnShootingDelayUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.ShootingDelayUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeMoneyCost = towerData.ShootingDelayUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        //towerData.ShootingDelay = towerInfo.DefTowerData.ShootingDelay - (towerInfo.DefTowerData.ShootingDelay * towerData.ShootingDelayUpgradeValues[counter] * 0.01f);

        OnShootingDelayUpgraded?.Invoke(towerData.ShootingDelayUpgradeValues[counter]);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeMoneyCost = towerData.ShootingDelayUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeMoneyCost = towerData.ShootingDelayUpgradeCosts[counter];
        }
    }
}
