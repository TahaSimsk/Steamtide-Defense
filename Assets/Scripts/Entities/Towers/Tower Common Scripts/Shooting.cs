
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] protected TargetScanner targetScanner;
    [SerializeField] protected Transform projectilePos;
    [SerializeField] protected AmmoManager ammoManager;

    protected float timer;

    protected TowerData towerData;
    IPoolable iPoolableProjectile;


    protected virtual void Start()
    {
        towerData = towerInfo.InstTowerData;
        if (towerData is IPoolable i)
        {
            iPoolableProjectile = i;
        }
    }


    protected virtual void Update()
    {

        Shoot();
    }


    protected virtual void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count > 0 && timer >= towerData.ShootingDelay)
        {
            if (ammoManager != null && ammoManager.ReduceAmmoAndCheckHasAmmo())
            {
                GetProjectileFromPoolAndActivate(projectilePos);
                timer = 0;
            }
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

        projectile.target = targetScanner.targetsInRange[0].transform;

        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        //pooledProjectile.transform.rotation = transform.rotation;
        pooledProjectile.transform.LookAt(projectile.target);
    }

}
