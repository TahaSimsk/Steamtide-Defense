
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] Transform projectilePos;

    GameObject pooledProjectile;

    public List<ProjectileHitBehaviours> hitBehaviours;

    float timer;

    ITower iTower;
    IProjectile iProjectile;
    IPoolable iPoolableProjectile;

    private void Start()
    {
        iTower = towerInfo.InstITower;
        iProjectile = towerInfo.InstIProjectile;
        iPoolableProjectile = (IPoolable)iProjectile;
    }


    private void Update()
    {

        Shoot();
    }


    void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count > 0 && timer >= iTower.ShootingDelay)
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
        projectile.SetProjectile(iProjectile);

        if (hitBehaviours.Count > 0)
        {
            projectile.hitBehaviours = hitBehaviours;
        }
        projectile.target = targetScanner.targetsInRange[0].transform;

        pooledProjectile.transform.position = projectilePos.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.rotation = transform.rotation;

        pooledProjectile = null;
    }

}



