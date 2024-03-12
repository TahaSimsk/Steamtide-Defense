using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDelayUpgrade : UpgradeBaseClass
{
   
    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.ShootingDelayUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeCost = towerData.ShootingDelayUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        towerData.ShootingDelay = towerInfo.DefTowerData.ShootingDelay - (towerInfo.DefTowerData.ShootingDelay * towerData.ShootingDelayUpgradeValues[counter] * 0.01f);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeCost = towerData.ShootingDelayUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = towerData.ShootingDelayUpgradeCosts[counter];
        }
    }
}
