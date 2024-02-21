using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : UpgradeBaseClass
{
    [SerializeField] TargetScanner targetScanner;

  
    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.RangeUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeCost = towerData.RangeUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {


        float upgradeValue = towerData.RangeUpgradeValues[counter];

        targetScanner.ChangeRange((towerInfo.DefITower.TowerRange * upgradeValue * 0.01f) + towerInfo.DefITower.TowerRange);
        towerData.TowerRange = upgradeValue;

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeCost = towerData.RangeUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = towerData.RangeUpgradeCosts[counter];
        }
    }
}
