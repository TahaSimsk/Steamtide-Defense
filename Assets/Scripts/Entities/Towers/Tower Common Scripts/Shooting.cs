
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;

    [SerializeField] protected TargetScanner targetScanner;
    [SerializeField] protected Transform projectilePos;
    [SerializeField] protected AmmoManager ammoManager;
    [SerializeField] protected Transform partToRotate;
    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] ShootingDelayUpgrade shootingDelayUpgrade;

    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onGlobalTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;


    protected float timer;

    protected TowerData towerData;
    IPoolable iPoolableProjectile;
    float combinedDamagePercentages;
    float combinedShootingDelayPercentages;
    protected virtual void Start()
    {
        //percentage += GlobalPercantageManager.Instance.GlobalTowerDamagePercentage;
        towerData = towerInfo.InstTowerData;
        if (towerData is IPoolable i)
        {
            iPoolableProjectile = i;
        }
        HandleDamageUpgrade(GlobalPercantageManager.Instance.GlobalTowerDamagePercentage);
        HandleShootingDelayUpgrade(GlobalPercantageManager.Instance.GlobalShootingDelayPercentage);
    }


    protected virtual void Update()
    {

        Shoot();
    }


    protected virtual void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count == 0) return;

        HelperFunctions.LookAtTarget(targetScanner.Target(towerData.TargetPriority).position, partToRotate, towerData.TowerRotationSpeed);

        if (timer < towerData.ShootingDelay) return;

        if (ammoManager != null && ammoManager.ReduceAmmoAndCheckHasAmmo())
        {
            GetProjectileFromPoolAndActivate(projectilePos);
            timer = 0;
        }

    }


    /*
     * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
     */
    protected void GetProjectileFromPoolAndActivate(Transform projectileSpawnPoint)
    {
        GameObject pooledProjectile = iPoolableProjectile.GetObject();

        if (pooledProjectile == null || targetScanner.targetsInRange.Count == 0) return;
        Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        projectile.SetProjectile(towerData);
        projectile.target = targetScanner.Target(towerData.TargetPriority);

        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        //pooledProjectile.transform.rotation = transform.rotation;
        pooledProjectile.transform.LookAt(projectile.target);
    }

    protected virtual void OnEnable()
    {
        damageUpgrade.OnDamageUpgraded += HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised += HandleDamageUpgrade;

        shootingDelayUpgrade.OnShootingDelayUpgraded += HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised += HandleShootingDelayUpgrade;
    }
    protected virtual void OnDisable()
    {
        damageUpgrade.OnDamageUpgraded -= HandleDamageUpgrade;
        onGlobalTowerDamageUpgrade.onEventRaised -= HandleDamageUpgrade;

        shootingDelayUpgrade.OnShootingDelayUpgraded -= HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised -= HandleShootingDelayUpgrade;
    }


    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedDamagePercentages += fl;
            //towerData.ProjectileDamage = towerInfo.DefTowerData.ProjectileDamage + towerInfo.DefTowerData.ProjectileDamage * combinedDamagePercentages * 0.01f;
            towerData.ProjectileDamage = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ProjectileDamage, combinedDamagePercentages);
        }
    }

    void HandleShootingDelayUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedShootingDelayPercentages += fl;
            //towerData.ShootingDelay = towerInfo.DefTowerData.ShootingDelay + towerInfo.DefTowerData.ShootingDelay * combinedShootingDelayPercentages * 0.01f;

            towerData.ShootingDelay = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ShootingDelay, combinedShootingDelayPercentages);

            Debug.Log(towerData.ShootingDelay);
            Debug.Log(combinedShootingDelayPercentages);
        }
    }
}
