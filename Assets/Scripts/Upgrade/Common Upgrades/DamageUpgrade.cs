using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DamageUpgrade : UpgradeBaseClass
{


    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = iProjectile.ProjectileDamageUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeCost = iProjectile.ProjectileDamageUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        iProjectile.ProjectileDamage = (towerInfo.DefIProjectile.ProjectileDamage * iProjectile.ProjectileDamageUpgradeValues[counter] * 0.01f) + towerInfo.DefIProjectile.ProjectileDamage;
        Debug.Log(iProjectile.ProjectileDamage);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeCost = iProjectile.ProjectileDamageUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = iProjectile.ProjectileDamageUpgradeCosts[counter];
        }
    }


}

