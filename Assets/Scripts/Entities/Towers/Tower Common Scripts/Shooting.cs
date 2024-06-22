
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Shooting : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] protected XPManager xpManager;
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

        if (targetScanner.targetsInRange.Contains(currentTarget))
        {

            HelperFunctions.LookAtTarget(currentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);
        }

        //HelperFunctions.LookAtTarget(targetScanner.Target(towerData.TargetPriority).position, partToRotate, towerData.TowerRotationSpeed);
        if (timer < towerData.ShootingDelay) return;

        if (ammoManager != null && ammoManager.ReduceAmmoAndCheckHasAmmo())
        {
            Anan();
            //partToRotate.LookAt(currentTarget.transform.position);
            //HelperFunctions.LookAtTarget(currentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);

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
        GameObject pooledProjectile = ObjectPool.Instance.GetObject(towerData.PoolableProjectile.hashCode, towerData.PoolableProjectile.objectToPoolPrefab);
        if (pooledProjectile == null || targetScanner.targetsInRange.Count == 0) return;
        Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        projectile.SetProjectile(towerData);
        //projectile.target = targetScanner.Target(towerData.TargetPriority);
        projectile.target = currentTarget.transform;
        projectile.xpManager = xpManager;
        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.LookAt(projectile.target);
        //Anan();
    }
    GameObject highestPointGameObjectFromDistanceOnly;
    GameObject previousTarget;

    GameObject currentTarget;


    void Anan()
    {
        int closestTargetPoint = 5;
        //int furthestTargetPoint = 5;

        int selectedTargetPoint = 2;

        int lowestHpTargetPoint = 3;
        int highestHpTargetPoint = 3;

        //int fastTargetPoint = 3;
        //int slowTargetPoint = 3;

        //int clusterTargetPoint = 3;


        //float distance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyPointPairs = new Dictionary<GameObject, float>();

       
        if (!targetScanner.targetsInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        HandleClosestTargetSelection(closestTargetPoint, enemyPointPairs);

        HandleSelectedTargetTargetSelection(selectedTargetPoint, enemyPointPairs);

        GameObject highestPointGameObject = RecalculateTotalPointsAndReturnEnemyWithHighestPoint(enemyPointPairs);

        FinilizeTargetSelection(selectedTargetPoint, enemyPointPairs, highestPointGameObject);
    }





    private void FinilizeTargetSelection(int selectedTargetPoint, Dictionary<GameObject, float> enemyPointPairs, GameObject highestPointGameObject)
    {
        currentTarget = highestPointGameObject;

        enemyPointPairs[currentTarget] += selectedTargetPoint;

        if (currentTarget == previousTarget)
        {
            enemyPointPairs[currentTarget] -= selectedTargetPoint;
        }

        previousTarget = currentTarget;
    }
    private void HandleSelectedTargetTargetSelection(int selectedTargetPoint, Dictionary<GameObject, float> enemyPointPairs)
    {
        if (currentTarget != null)
        {
            enemyPointPairs[currentTarget] += selectedTargetPoint;
        }
    }
    private GameObject RecalculateTotalPointsAndReturnEnemyWithHighestPoint(Dictionary<GameObject, float> enemyPointPairs)
    {
        GameObject highestPointGameObject = null;
        float highestPoint = 0;
        foreach (var item in enemyPointPairs)
        {
            if (item.Value > highestPoint)
            {
                highestPoint = item.Value;
                highestPointGameObject = item.Key;
            }
        }

        return highestPointGameObject;
    }

    private void HandleClosestTargetSelection(int closestTargetPoint, Dictionary<GameObject, float> enemyPointPairs)
    {
        //calculate shortest distance and store distances to dictionary
        float shortestDistance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();

        foreach (var enemy in targetScanner.targetsInRange)
        {
            float currentEnemyDistance = (transform.position - enemy.transform.position).sqrMagnitude;

            enemyDistancePairs.Add(enemy, currentEnemyDistance);

            if (currentEnemyDistance <= shortestDistance)
            {
                shortestDistance = currentEnemyDistance;
            }
        }

        //add points to enemies based off of distance
        foreach (var pair in enemyDistancePairs)
        {
            enemyPointPairs[pair.Key] += shortestDistance / pair.Value * closestTargetPoint;
        }
    }

    private void HandleLowestHpTargetSelection(Dictionary<GameObject, float> enemyPointPairs, int lowestHPTargetPoint)
    {
        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHP = Mathf.Infinity;
        foreach (var pair in targetScanner.targetsInRange)
        {
            float currentEnemyHp = pair.GetComponent<EnemyHealth>().CurrentHealth;

            enemyHPPairs.Add(pair, currentEnemyHp);

            if (currentEnemyHp < lowestEnemyHP)
            {
                lowestEnemyHP = currentEnemyHp;
            }
        }

        foreach (var pair in enemyHPPairs)
        {
            enemyPointPairs[pair.Key] += lowestEnemyHP / pair.Value * lowestHPTargetPoint;
        }

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
            Debug.Log(towerData.ProjectileDamage);
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
