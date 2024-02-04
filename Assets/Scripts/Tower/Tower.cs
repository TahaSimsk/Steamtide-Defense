using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] SphereCollider targetScanner;
    [SerializeField] Transform projectilePos;
    [SerializeField] DataTower towerData;
    [SerializeField] DataProjectile projectileData;

    [SerializeField] LayerMask enemyLayer;

    ObjectPool objectPool;


    public List<GameObject> enemies = new List<GameObject>();



    float timer;
    private void OnEnable()
    {
        EventManager.onEnemyDeath += RemoveEnemy;
    }

    private void OnDisable()
    {
        EventManager.onEnemyDeath -= RemoveEnemy;
    }

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        //targetScanner.radius = weaponRange;
    }

    private void Update()
    {
        LookAtTarget();

        Shoot();
    }


    void Shoot()
    {
        timer -= Time.deltaTime;
        if (enemies.Count > 0 && timer <= 0)
        {
            GetProjectileFromPoolAndActivate();
            timer = towerData.shootingDelay;
        }
    }


    /*
     * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
     */
    void GetProjectileFromPoolAndActivate()
    {

        //GameObject pooledProjectile = objectPool.GetObjectFromPool(projectileData.hashCode);

        GameObject pooledProjectile= null;
        foreach (var obj in projectileData.objList)
        {
            if (!obj.activeInHierarchy)
            {
                pooledProjectile = obj;
                goto Found;
            }
        }
        Found:
        pooledProjectile.GetComponent<Projectile2>().target = enemies[0].transform;

        pooledProjectile.transform.position = projectilePos.position;
        pooledProjectile.SetActive(true);
    }



    void LookAtTarget()
    {
        if (enemies.Count == 0) return;

        Vector3 dir = enemies[0].transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * towerData.weaponRotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(rotation);

    }


    
    //this method is used to remove enemy when an enemy dies based off an event
    public void RemoveEnemy(GameObject enemy, Data data)
    {
        enemies.Remove(enemy);
    }


    //these methods are used to add and remove enemies when they entered or left target scanner's range
    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}



