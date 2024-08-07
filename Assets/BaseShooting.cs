using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooting : MonoBehaviour
{
    [SerializeField] ObjectInfo baseInfo;

    //[SerializeField] ObjectToPool projectileData;
    [SerializeField] Transform partToRotate;
    [SerializeField] Transform projectileSpawnPoint;

    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] ShootingDelayUpgrade shootingDelayUpgrade;

    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onGlobalTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;

    EnemyEnterBaseSequence enemyEnterBaseSequence;

    BaseData baseData;
    float timer;
    private float combinedDamagePercentages;
    private float combinedShootingDelayPercentages;

    private void Awake()
    {
        enemyEnterBaseSequence = GetComponent<EnemyEnterBaseSequence>();
    }

    private void Start()
    {
        baseData = baseInfo.InstTowerData as BaseData;
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

    private void Update()
    {
        if (enemyEnterBaseSequence.EnemiesInBase.Count == 0) return;
        timer += Time.deltaTime;
        HelperFunctions.LookAtTarget(enemyEnterBaseSequence.EnemiesInBase[0].transform.position, partToRotate, baseData.TowerRotationSpeed);
        //partToRotate.LookAt(enemyEnterBaseSequence.enemyList[0].transform);
        if (timer >= baseData.ShootingDelay)
        {
            timer = 0;



            //GameObject spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            GameObject spawnedProjectile = ObjectPool.Instance.GetObject(baseData.TowerProjectilePrefab.GetHashCode(), baseData.TowerProjectilePrefab);
            StartCoroutine(MoveProjectile(spawnedProjectile.transform, enemyEnterBaseSequence.EnemiesInBase[0].transform));
        }

    }

    IEnumerator MoveProjectile(Transform projectile, Transform target)
    {
        Vector3 targetPos = target.position;
        projectile.gameObject.SetActive(true);
        projectile.position = projectileSpawnPoint.position;
        while ((projectile.position - targetPos).sqrMagnitude > .1f)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.position, targetPos, baseData.ProjectileSpeed * Time.deltaTime);
            yield return null;
        }
        if (target.gameObject.activeInHierarchy)
        {
            target.GetComponent<EnemyHealth>().ReduceHealth(baseData.ProjectileDamage);
        }
        projectile.gameObject.SetActive(false);

    }

    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedDamagePercentages += fl;
            baseData.ProjectileDamage = HelperFunctions.CalculatePercentage(baseInfo.DefTowerData.ProjectileDamage, combinedDamagePercentages);
        }
    }

    void HandleShootingDelayUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedShootingDelayPercentages += fl;
            baseData.ShootingDelay = HelperFunctions.CalculatePercentage(baseInfo.DefTowerData.ShootingDelay, combinedShootingDelayPercentages);

        }
    }
}
