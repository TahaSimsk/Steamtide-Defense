
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] Transform projectilePos;

    GameObject pooledProjectile;

    float timer;

    TowerData towerData;
    ProjectileData projectileData;
    IPoolable iPoolableProjectile;

    private void Start()
    {
        towerData = towerInfo.InstITower;
        projectileData = towerInfo.InstIProjectile;
        iPoolableProjectile = (IPoolable)projectileData;
    }


    protected virtual void Update()
    {

        Shoot();
    }


    void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count > 0 && timer >= towerData.ShootingDelay)
        {
            GetProjectileFromPoolAndActivate();
            timer = 0;
        }
    }


    /*
     * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
     */
    void GetProjectileFromPoolAndActivate()
    {
        pooledProjectile = iPoolableProjectile.GetObject();

        if (pooledProjectile == null) return;
        Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        projectile.SetProjectile(projectileData);

        projectile.target = targetScanner.targetsInRange[0].transform;

        pooledProjectile.transform.position = projectilePos.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.rotation = transform.rotation;

        pooledProjectile = null;
    }

}



