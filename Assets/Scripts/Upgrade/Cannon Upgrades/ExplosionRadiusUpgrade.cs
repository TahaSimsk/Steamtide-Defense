using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionRadiusUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    BallData ball;
    BallData defBall;

    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ball = (BallData)iProjectile;
        defBall = (BallData)towerInfo.DefIProjectile;

        upgradeCost = ball.ExplosionRadiusUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
            ball.ExplosionRadius = (defBall.ExplosionRadius * ball.ExplosionRadiusUpgradeValues[upgradeOrder - 1] * 0.01f) + defBall.ExplosionRadius;
        

        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
