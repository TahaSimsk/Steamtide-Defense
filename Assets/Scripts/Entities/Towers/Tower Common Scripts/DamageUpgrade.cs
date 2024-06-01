using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : UpgradeBaseClass
{
    public Action<object> OnDamageUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.ProjectileDamageUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeMoneyCost = towerData.ProjectileDamageUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        //towerData.ProjectileDamage = (towerInfo.DefTowerData.ProjectileDamage * towerData.ProjectileDamageUpgradeValues[counter] * 0.01f) + towerInfo.DefTowerData.ProjectileDamage;

        OnDamageUpgraded?.Invoke(towerData.ProjectileDamageUpgradeValues[counter]);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeMoneyCost = towerData.ProjectileDamageUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeMoneyCost = towerData.ProjectileDamageUpgradeCosts[counter];
        }
    }


}

