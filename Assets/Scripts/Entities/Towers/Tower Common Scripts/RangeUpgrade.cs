using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : UpgradeBaseClass
{

    public event Action OnRangeUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.RangeUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeMoneyCost = towerData.RangeUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        towerData.TowerRange = (towerInfo.DefTowerData.TowerRange * towerData.RangeUpgradeValues[counter] * 0.01f) + towerInfo.DefTowerData.TowerRange;

        OnRangeUpgraded?.Invoke();

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeMoneyCost = towerData.RangeUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeMoneyCost = towerData.RangeUpgradeCosts[counter];
        }
    }
}
