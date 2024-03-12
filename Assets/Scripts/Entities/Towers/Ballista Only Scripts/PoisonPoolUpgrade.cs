using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonPoolUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    BallistaData ballistaData;

    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ballistaData = (BallistaData)towerData;
        upgradeCost = ballistaData.poolUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {

        switch (upgradeOrder)
        {
            case 1:
                ballistaData.canPoison = true;
                ballistaData.dropPoolOnFirstEnemy = true;
                break;
            case 2:
                ballistaData.poolDropChance = ballistaData.poolDropChanceUpgradedValue;
                break;
            case 3:
                ballistaData.poolDuration = ballistaData.poolDurationUpgradedValue;
                break;
            case 4:
                ballistaData.dropPoolOnFirstEnemy = false;
                ballistaData.poolDamage = ballistaData.poolDamageUpgradedValue;
                break;
        }

        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;


    }
}
