
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

        if (targetScanner.targetsInRange.Contains(targetScanner.CurrentTarget))
        {

            HelperFunctions.LookAtTarget(targetScanner.CurrentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);
        }

        //HelperFunctions.LookAtTarget(targetScanner.Target(towerData.TargetPriority).position, partToRotate, towerData.TowerRotationSpeed);
        if (timer < towerData.ShootingDelay) return;

        if (ammoManager != null && ammoManager.ReduceAmmoAndCheckHasAmmo())
        {
            //Anan();
            //partToRotate.LookAt(currentTarget.transform.position);
            //HelperFunctions.LookAtTarget(currentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);
            targetScanner.Anan();
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
        //projectile.target = currentTarget.transform;
       
        projectile.target = targetScanner.CurrentTarget.transform;
        projectile.xpManager = xpManager;
        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.LookAt(projectile.target);
        //Anan();
    }
    GameObject highestPointGameObjectFromDistanceOnly;
    GameObject previousTarget;

    GameObject currentTarget;

    [SerializeField] int closestTargetPoint = 1;
    //[SerializeField] int furthestTargetPoint = 5;

    [SerializeField] int selectedTargetPoint = 2;

    [SerializeField] int lowestHpTargetPoint = 5;
    [SerializeField] int highestHpTargetPoint = 3;

    [SerializeField] int fastTargetPoint = 3;
    [SerializeField] int slowTargetPoint = 3;

    [SerializeField] int clusterTargetPoint = 3;


    //float distance = Mathf.Infinity;


    void Anan()
    {

        Dictionary<GameObject, float> enemyPointPairs = new Dictionary<GameObject, float>();

        foreach (var target in targetScanner.targetsInRange)
        {
            enemyPointPairs.Add(target, 0);
        }

        if (!targetScanner.targetsInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        HandleClosestTargetSelection(closestTargetPoint, enemyPointPairs);

        HandleTargetSelectionBasedOnHealth(enemyPointPairs, lowestHpTargetPoint);

        HandleSelectedTargetTargetSelection(selectedTargetPoint, enemyPointPairs);

        HandleTargetSelectionBasedOnMoveSpeed(enemyPointPairs, highestHpTargetPoint);

        HandleTargetSelectionBasedOnCluster(enemyPointPairs, clusterTargetPoint);

        GameObject highestPointGameObject = RecalculateTotalPointsAndReturnEnemyWithHighestPoint(enemyPointPairs);

        FinilizeTargetSelection(selectedTargetPoint, enemyPointPairs, highestPointGameObject);
    }


    #region
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
        values.Clear();
        objects.Clear();
        foreach (var item in enemyPointPairs)
        {
            values.Add(item.Value);
            objects.Add(item.Key);
            if (item.Value > highestPoint)
            {
                highestPoint = item.Value;
                highestPointGameObject = item.Key;
            }
        }

        return highestPointGameObject;
    }

    #endregion




    private void HandleClosestTargetSelection(int closestTargetPoint, Dictionary<GameObject, float> enemyPointPairs)
    {
        //calculate shortest distance and store distances to dictionary
        float shortestDistance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();

        foreach (var enemy in targetScanner.targetsInRange)
        {
            float currentEnemyDistance = (transform.position - enemy.transform.position).sqrMagnitude;

            enemyDistancePairs.Add(enemy, currentEnemyDistance);

            if (currentEnemyDistance < shortestDistance)
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

    private void HandleTargetSelectionBasedOnHealth(Dictionary<GameObject, float> enemyPointPairs, int lowestHPTargetPoint)
    {
        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHP = Mathf.Infinity;
        float highestHP = 0;
        foreach (var pair in targetScanner.targetsInRange)
        {
            float currentEnemyHp = pair.GetComponent<EnemyHealth>().CurrentHealth;

            enemyHPPairs.Add(pair, currentEnemyHp);

            if (currentEnemyHp < lowestEnemyHP)
            {
                lowestEnemyHP = currentEnemyHp;
            }
            if (currentEnemyHp > highestHP)
            {
                highestHP = currentEnemyHp;
            }

        }

        foreach (var pair in enemyHPPairs)
        {
            float pointFromLowHp = lowestEnemyHP / pair.Value * lowestHPTargetPoint;
            float pointFromHighHp = pair.Value / highestHP * highestHpTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowHp + pointFromHighHp;
        }

    }
    public List<float> values = new List<float>();
    public List<GameObject> objects = new List<GameObject>();
    [SerializeField] LayerMask enemyLayer;

    private void HandleTargetSelectionBasedOnMoveSpeed(Dictionary<GameObject, float> enemyPointPairs, int pointMultiplier)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        float lowestMoveSpeed = Mathf.Infinity;
        float highestMoveSpeed = 0;
        foreach (var enemy in targetScanner.targetsInRange)
        {
            float currentEnemyMoveSpeed = enemy.GetComponent<EnemyMovement>().CurrentMoveSpeed;

            enemyValuePairs.Add(enemy, currentEnemyMoveSpeed);

            if (currentEnemyMoveSpeed < lowestMoveSpeed)
            {
                lowestMoveSpeed = currentEnemyMoveSpeed;
            }
            if (currentEnemyMoveSpeed > highestMoveSpeed)
            {
                highestMoveSpeed = currentEnemyMoveSpeed;
            }
        }

        foreach (var pair in enemyValuePairs)
        {
            float pointFromLowMoveSpeed = lowestMoveSpeed / pair.Value * slowTargetPoint;
            float pointFromHighMoveSpeed = pair.Value / highestMoveSpeed * fastTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowMoveSpeed + pointFromHighMoveSpeed;
        }


    }

    private void HandleTargetSelectionBasedOnCluster(Dictionary<GameObject, float> enemyPointPairs, int pointMultiplier)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        int enemyCountInRange = 0;
        foreach (var pair in targetScanner.targetsInRange)
        {
            Collider[] enemies = Physics.OverlapSphere(pair.transform.position, 10f, enemyLayer);
            int currentEnemyEnemyCountInRange;
            if (enemies != null)
            {
                currentEnemyEnemyCountInRange = enemies.Length;
            }
            else
            {
                currentEnemyEnemyCountInRange = 0;
            }

            enemyValuePairs.Add(pair, currentEnemyEnemyCountInRange);

            if (currentEnemyEnemyCountInRange > enemyCountInRange)
            {
                enemyCountInRange = currentEnemyEnemyCountInRange;
            }
        }

        foreach (var pair in enemyValuePairs)
        {
            float pointFromHighestCluster = pair.Value / enemyCountInRange * clusterTargetPoint;

            enemyPointPairs[pair.Key] += pointFromHighestCluster;
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
