
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] protected XPManager xpManager;
    [SerializeField] protected TowerTargetScanner targetScanner;
    [SerializeField] protected Transform projectilePos;
    [SerializeField] protected AmmoManager ammoManager;
    [SerializeField] protected Transform partToRotate;
    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] ShootingDelayUpgrade shootingDelayUpgrade;
    [SerializeField] protected TargetingSystem targetingSystem;
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onGlobalTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;


    protected float timer;

    protected TowerData towerData;
    float combinedDamagePercentages;
    float combinedShootingDelayPercentages;

    protected virtual void Start()
    {
        //percentage += GlobalPercantageManager.Instance.GlobalTowerDamagePercentage;
        towerData = towerInfo.InstTowerData;

        HandleDamageUpgrade(GlobalPercentageManager.Instance.GlobalTowerDamagePercentage);
        HandleShootingDelayUpgrade(GlobalPercentageManager.Instance.GlobalShootingDelayPercentage);
    }


    protected virtual void Update()
    {

        Shoot();
    }


    protected virtual void Shoot()
    {
        timer += Time.deltaTime;
        if (timer >= towerData.ShootingDelay)
            projectilePos.gameObject.SetActive(true);
        if (targetScanner.targetsInRange.Count == 0) return;


        if (timer >= towerData.ShootingDelay - towerData.ShootingDelay * 10 / 100 && timer < towerData.ShootingDelay)
        {
            targetingSystem.GetTarget(targetScanner.targetsInRange);
        }

        if (targetScanner.targetsInRange.Contains(targetingSystem.CurrentTarget))
        {

            HelperFunctions.LookAtTarget(targetingSystem.CurrentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);
        }
        if (timer < towerData.ShootingDelay) return;

        if (ammoManager != null && ammoManager.ReduceAmmoAndCheckHasAmmo())
        {

            GetProjectileFromPoolAndActivate(projectilePos);
            projectilePos.gameObject.SetActive(false);
            timer = 0;
        }
    }


    /*
     * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
     */
    protected void GetProjectileFromPoolAndActivate(Transform projectileSpawnPoint)
    {
        GameObject pooledProjectile = ObjectPool.Instance.GetObject(towerData.TowerProjectilePrefab.GetHashCode(), towerData.TowerProjectilePrefab);
        if (pooledProjectile == null || targetScanner.targetsInRange.Count == 0) return;
        Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        projectile.SetProjectile(towerData);

        projectile.target = targetingSystem.CurrentTarget.transform;
        projectile.xpManager = xpManager;
        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.LookAt(projectile.target);
    }



    protected virtual void OnEnable()
    {
        damageUpgrade.OnDamageUpgraded += HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised += HandleDamageUpgrade;
        xpManager.OnLevelUp += HandleDamageUpgrade;

        shootingDelayUpgrade.OnShootingDelayUpgraded += HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised += HandleShootingDelayUpgrade;
    }


    protected virtual void OnDisable()
    {
        damageUpgrade.OnDamageUpgraded -= HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised -= HandleDamageUpgrade;
        xpManager.OnLevelUp -= HandleDamageUpgrade;

        shootingDelayUpgrade.OnShootingDelayUpgraded -= HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised -= HandleShootingDelayUpgrade;
    }


    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedDamagePercentages += fl;
            towerData.ProjectileDamage = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ProjectileDamage, combinedDamagePercentages);
        }
    }


    void HandleShootingDelayUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedShootingDelayPercentages += fl;
            towerData.ShootingDelay = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ShootingDelay, combinedShootingDelayPercentages);
        }
    }
}
