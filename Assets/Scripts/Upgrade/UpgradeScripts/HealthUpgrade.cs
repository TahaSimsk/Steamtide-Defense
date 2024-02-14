using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : UpgradeBaseClass
{

    [SerializeField] TowerHealth towerHealth;

    protected override void OnEnable()
    {
        base.OnEnable();
        upgradeCost = iTower.MaxHealthUpgradeCosts[counter];
        maxUpgrateCount = iTower.MaxHealthUpgradeValues.Count;
    }

    protected override void DoUpgrade()
    {

        towerHealth.SetMaxHP(iTower.MaxHealthUpgradeValues[counter]);

        if (counter + 1 < maxUpgrateCount)
        {
            upgradeCost = iTower.MaxHealthUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = iTower.MaxHealthUpgradeCosts[counter];
        }
    }
}
