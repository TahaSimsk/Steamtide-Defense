using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PierceUpgrade : UpgradeBaseClass
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
        upgradeCost = arrow.pierceUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        arrow.pierceLimit = arrow.pierceLimitUpgrades[upgradeOrder - 1];
        arrow.canPierce = true;

        switch (upgradeOrder)
        {
            case 1:
                arrow.pierceDamage = arrow.pierce1DamageUpgrades;
                break;
            case 2:
                arrow.pierceDamage = arrow.pierce2DamageUpgrades;
                break;
            case 3:
                arrow.pierceDamage = arrow.pierce3DamageUpgrades;
                break;
            case 4:
                arrow.pierceDamage = arrow.pierce4DamageUpgrades;
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
