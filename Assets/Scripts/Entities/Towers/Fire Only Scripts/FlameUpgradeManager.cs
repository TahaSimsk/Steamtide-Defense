using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameUpgradeManager : MonoBehaviour
{
    [SerializeField] Transform[] flames;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] XPManager xpManager;
    [SerializeField] RangeUpgrade rangeUpgrade;
    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalTowerRangeUpgrade;

    [SerializeField] ShootingDelayUpgrade shootingDelayUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;

    FireData fireData;

    float damagePercentage;
    float rangePercentage;

    float combinedShootingDelayPercentages;

    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        SetFlameScale(GlobalPercentageManager.Instance.GlobalRangePercentage);

        HandleDamageUpgrade(GlobalPercentageManager.Instance.GlobalTowerDamagePercentage);

        HandleShootingDelayUpgrade(GlobalPercentageManager.Instance.GlobalShootingDelayPercentage);
    }


    private void OnEnable()
    {
        damageUpgrade.OnDamageUpgraded += HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised += HandleDamageUpgrade;
        xpManager.OnLevelUp += HandleDamageUpgrade;

        rangeUpgrade.OnRangeUpgraded += SetFlameScale;
        onGlobalTowerRangeUpgrade.onEventRaised += SetFlameScale;

        shootingDelayUpgrade.OnShootingDelayUpgraded += HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised += HandleShootingDelayUpgrade;
    }


    private void OnDisable()
    {
        damageUpgrade.OnDamageUpgraded -= HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised -= HandleDamageUpgrade;
        xpManager.OnLevelUp -= HandleDamageUpgrade;

        rangeUpgrade.OnRangeUpgraded -= SetFlameScale;
        onGlobalTowerRangeUpgrade.onEventRaised -= SetFlameScale;

        shootingDelayUpgrade.OnShootingDelayUpgraded -= HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised -= HandleShootingDelayUpgrade;
    }


    void SetFlameScale(object _amount)
    {
        if (_amount is float fl)
        {
            rangePercentage += fl;
            fireData.TowerRange = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.TowerRange, rangePercentage);

            foreach (var flame in flames)
            {

               flame.localScale = new Vector3(flame.localScale.x, flame.localScale.y, fireData.TowerRange);
            }
        }
    }


    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            damagePercentage += fl;
            fireData.ProjectileDamage = towerInfo.DefTowerData.ProjectileDamage + towerInfo.DefTowerData.ProjectileDamage * damagePercentage * 0.01f;
        }
    }


    void HandleShootingDelayUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedShootingDelayPercentages += Mathf.Abs(fl);
            fireData.ShootingDelay = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ShootingDelay, combinedShootingDelayPercentages);
        }
    }
}
