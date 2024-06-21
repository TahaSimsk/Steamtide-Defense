
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

    List<float> highestPoints = new List<float>();

    void Anan()
    {
        int closestTargetPoint = 5;
        //int furthestTargetPoint = 5;

        int selectedTargetPoint = 2;

        //int lowHpTargetPoint = 3;
        //int highHpTargetPoint = 3;

        //int fastTargetPoint = 3;
        //int slowTargetPoint = 3;

        //int clusterTargetPoint = 3;


        //float distance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();
        Dictionary<GameObject, float> enemyPointPairsFromDistance = new Dictionary<GameObject, float>();
        Dictionary<GameObject, float> enemyPointPairs = new Dictionary<GameObject, float>();

        foreach (var item in targetScanner.targetsInRange)
        {
            enemyDistancePairs.Add(item, 0);
            enemyPointPairsFromDistance.Add(item, 0);
            enemyPointPairs.Add(item, 0);
        }
        highestPoints.Clear();
        if (!targetScanner.targetsInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        //Dictionary<GameObject, float> tempDictionary = new Dictionary<GameObject, float>(enemyDistancePairs);

        //foreach (var pair in tempDictionary)
        //{
        //    if (!targetScanner.targetsInRange.Contains(pair.Key))
        //    {
        //        enemyDistancePairs.Remove(pair.Key);
        //        enemyPointPairsFromDistance.Remove(pair.Key);
        //        enemyPointPairs.Remove(pair.Key);

        //        if (currentTarget==pair.Key)
        //        {
        //            currentTarget = null;
        //        }
        //    }
        //}

        //foreach (var enemy in targetScanner.targetsInRange)
        //{
        //    if (!enemyDistancePairs.ContainsKey(enemy))
        //    {
        //        enemyDistancePairs.Remove(enemy);
        //        enemyPointPairsFromDistance.Remove(enemy);
        //        enemyPointPairs.Remove(enemy);
        //    }
        //}





        float shortestDistance = Mathf.Infinity;
        float highestPointFromDistanceOnly = 0;


        foreach (var enemy in targetScanner.targetsInRange)
        {
            float currentEnemyDistance = (transform.position - enemy.transform.position).sqrMagnitude;


            //enemyDistancePairs[enemy] = currentEnemyDistance;

            //float currentDistance = pair.Value;

            //if (currentDistance <= shortestDistance)
            //{
            //    shortestDistance = currentDistance;
            //}

            //float newPoint = shortestDistance / enemyDistancePairs[pair.Key] * closestTargetPoint;
            //tempUpdates[pair.Key] = newPoint;


            //enemyPointPairs[pair.Key] = newPoint;


            if (enemyDistancePairs.ContainsKey(enemy))
            {
                enemyDistancePairs[enemy] = currentEnemyDistance;
            }
            else
            {
                enemyDistancePairs.Add(enemy, currentEnemyDistance);
                enemyPointPairsFromDistance.Add(enemy, 0);
            }

        }

        shortestDistance = FindShortestDistance(enemyDistancePairs, shortestDistance);

        //if (currentEnemyDistance < shortestDistance)
        //{
        //    shortestDistance = currentEnemyDistance;
        //    //finalPoint = closestTargetPoint;
        //    //return;
        //}

        //float finalPoint = shortestDistance / currentEnemyDistance * closestTargetPoint;



        Dictionary<GameObject, float> tempUpdates = new Dictionary<GameObject, float>(enemyPointPairsFromDistance);

        foreach (var pair in enemyPointPairsFromDistance)
        {
            float newPoint = shortestDistance / enemyDistancePairs[pair.Key] * closestTargetPoint;
            tempUpdates[pair.Key] = newPoint;

            if (enemyPointPairs.ContainsKey(pair.Key))
            {
                enemyPointPairs[pair.Key] = newPoint;
            }
            else
            {
                enemyPointPairs.Add(pair.Key, newPoint);
            }

            //if (pair.Value >= highestPoint)
            //{
            //    highestPoint = pair.Value;
            //    highestPointGameObject = pair.Key;
            //    Debug.Log(highestPoint);
            //}
            if (newPoint >= highestPointFromDistanceOnly)
            {
                highestPointFromDistanceOnly = newPoint;
                highestPointGameObjectFromDistanceOnly = pair.Key;
                Debug.Log(highestPointFromDistanceOnly);
            }
        }


        if (currentTarget != null)
        {
            enemyPointPairs[currentTarget] += selectedTargetPoint;
        }



        //if (previousTarget != null && enemyPointPairs.ContainsKey(previousTarget))
        //{
        //    enemyPointPairs[previousTarget] -= selectedTargetPoint;
        //}


        float highestPoint = 0;
        GameObject highestPointGameObject = null;
        foreach (var item in enemyPointPairs)
        {
            if (item.Value > highestPoint)
            {
                highestPoint = item.Value;
                highestPointGameObject = item.Key;
            }

            highestPoints.Add(item.Value);

        }

        currentTarget = highestPointGameObject;

        enemyPointPairs[currentTarget] += selectedTargetPoint;

        if (currentTarget == previousTarget)
        {
            enemyPointPairs[currentTarget] -= selectedTargetPoint;
        }

        previousTarget = currentTarget;
    }

    private float FindShortestDistance(Dictionary<GameObject, float> enemyDistancePairs, float shortestDistance)
    {
        foreach (var pair in enemyDistancePairs)
        {
            float currentDistance = pair.Value;

            if (currentDistance <= shortestDistance)
            {
                shortestDistance = currentDistance;
            }
        }

        return shortestDistance;
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
