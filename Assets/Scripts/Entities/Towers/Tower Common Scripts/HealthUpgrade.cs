using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : UpgradeBaseClass
{

    [SerializeField] TowerHealth towerHealth;


    protected override void OnEnable()
    {
        base.OnEnable();
        maxUpgradeCount = towerData.MaxHealthUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeMoneyCost = towerData.MaxHealthUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {

        towerHealth.SetMaxHP(towerData.MaxHealthUpgradeValues[counter]);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeMoneyCost = towerData.MaxHealthUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeMoneyCost = towerData.MaxHealthUpgradeCosts[counter];
        }
    }
}
