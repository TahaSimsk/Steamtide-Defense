using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PierceUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    BallistaData ballistaData;

    protected override void Awake()
    {
        base.Awake();
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ballistaData = (BallistaData)towerData;
        upgradeMoneyCost = ballistaData.pierceUpgradeMoneyCosts[upgradeOrder - 1];
        upgradeWoodCost = ballistaData.pierceUpgradeWoodCosts[upgradeOrder - 1];
        upgradeRockCost = ballistaData.pierceUpgradeRockCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        ballistaData.pierceLimit = ballistaData.pierceLimitUpgrades[upgradeOrder - 1];
        ballistaData.canPierce = true;

        switch (upgradeOrder)
        {
            case 1:
                ballistaData.pierceDamage = ballistaData.pierce1DamageUpgrades;
                break;
            case 2:
                ballistaData.pierceDamage = ballistaData.pierce2DamageUpgrades;
                break;
            case 3:
                ballistaData.pierceDamage = ballistaData.pierce3DamageUpgrades;
                break;
            case 4:
                ballistaData.pierceDamage = ballistaData.pierce4DamageUpgrades;
                break;
        }
        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
