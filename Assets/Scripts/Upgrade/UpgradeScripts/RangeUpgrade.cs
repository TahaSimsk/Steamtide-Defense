using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : UpgradeBaseClass
{
    [SerializeField] TargetScanner targetScanner;

    
    protected override void OnEnable()
    {
        base.OnEnable();
        upgradeCost = iTower.WeaponRangeUpgradeCosts[counter];
        maxUpgrateCount = iTower.WeaponRangeUpgradeValues.Count;
    }

    protected override void DoUpgrade()
    {
        float upgradeValue = iTower.WeaponRangeUpgradeValues[counter];

        targetScanner.ChangeRange((towerInfo.DefITower.WeaponRange * upgradeValue * 0.01f) + towerInfo.DefITower.WeaponRange);
        iTower.WeaponRange = upgradeValue;

        if (counter + 1 < maxUpgrateCount)
        {
            upgradeCost = iTower.WeaponRangeUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = iTower.WeaponRangeUpgradeCosts[counter];
        }
    }
}
