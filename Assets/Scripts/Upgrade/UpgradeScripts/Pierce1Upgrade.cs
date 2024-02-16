using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pierce1Upgrade : UpgradeBaseClass
{
    public int upgradeOrder;

    ArrowData arrow;

    [SerializeField] ProjectileHitBehaviours hitBehaviour;

    public Shooting shooting;

    private void Awake()
    {
        maxUpgrateCount = 1;
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

        var hit = Instantiate(hitBehaviour);


        int index = shooting.hitBehaviours.FindIndex(a => a.NameOfType == hitBehaviour.NameOfType);
        if (index == -1)
        {
            shooting.hitBehaviours.Add(hit);
        }
        else
        {
            shooting.hitBehaviours[index] = hit;
        }



    }
}
