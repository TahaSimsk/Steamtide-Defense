using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DamageUpgrade : UpgradeBaseClass
{


    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.ProjectileDamageUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeCost = towerData.ProjectileDamageUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        towerData.ProjectileDamage = (towerInfo.DefTowerData.ProjectileDamage * towerData.ProjectileDamageUpgradeValues[counter] * 0.01f) + towerInfo.DefTowerData.ProjectileDamage;

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeCost = towerData.ProjectileDamageUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = towerData.ProjectileDamageUpgradeCosts[counter];
        }
    }


}

