using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDelayUpgrade : UpgradeBaseClass
{

    protected override void OnEnable()
    {
        base.OnEnable();
        upgradeCost = iTower.ShootingDelayUpgradeCosts[counter];
        maxUpgrateCount = iTower.ShootingDelayUpgradeValues.Count;
    }

    protected override void DoUpgrade()
    {
        iTower.ShootingDelay = towerInfo.DefITower.ShootingDelay - (towerInfo.DefITower.ShootingDelay * iTower.ShootingDelayUpgradeValues[counter] * 0.01f);

        Debug.Log(iTower.ShootingDelay);

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
