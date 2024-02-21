using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonPoolUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    ArrowData arrow;

    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        arrow = (ArrowData)iProjectile;
        upgradeCost = arrow.poolUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {

        switch (upgradeOrder)
        {
            case 1:
                arrow.canPoison = true;
                arrow.dropPoolOnFirstEnemy = true;
                break;
            case 2:
                arrow.poolDropChance = arrow.poolDropChanceUpgradedValue;
                break;
            case 3:
                arrow.poolDuration = arrow.poolDurationUpgradedValue;
                break;
            case 4:
                arrow.dropPoolOnFirstEnemy = false;
                arrow.poolDamage = arrow.poolDamageUpgradedValue;
                break;
        }

        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;


    }
}
