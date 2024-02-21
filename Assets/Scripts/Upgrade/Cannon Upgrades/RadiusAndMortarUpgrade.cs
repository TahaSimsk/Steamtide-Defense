using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiusAndMortarUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    [SerializeField] GameObject mortarCanvas;
    BallData ball;
    CannonData cannonData;
    BallData defBall;

    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ball = (BallData)iProjectile;
        cannonData = (CannonData)towerData;
        defBall = (BallData)towerInfo.DefIProjectile;

        upgradeCost = upgradeOrder <= 3 ? ball.ExplosionRadiusUpgradeCosts[upgradeOrder - 1] : cannonData.mortarUpgradeCost;
        
    }


    protected override void DoUpgrade()
    {
        if (upgradeOrder <= 3)
        {
            ball.ExplosionRadius = (defBall.ExplosionRadius * ball.ExplosionRadiusUpgradeValues[upgradeOrder - 1] * 0.01f) + defBall.ExplosionRadius;
        }
        else
        {
            mortarCanvas.SetActive(true);
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
