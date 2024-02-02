using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static DelegateManager;

public class Tower2 : MonoBehaviour
{
    [SerializeField] SphereCollider targetScanner;
    [SerializeField] Transform projectilePos;
    [SerializeField] DataTower towerData;

    [SerializeField] LayerMask enemyLayer;

    ObjectPool objectPool;


    public List<GameObject> enemies = new List<GameObject>();



    float timer;
    private void OnEnable()
    {
        EnemyHealth.onEnemyDeath += RemoveEnemy;
    }
    private void OnDisable()
    {
        EnemyHealth.onEnemyDeath -= RemoveEnemy;
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
            StartCoroutine(GetProjectileFromPoolAndActivate());
            timer = towerData.shootingDelay;
        }
    }


    /*
     * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
     */
    IEnumerator GetProjectileFromPoolAndActivate()
    {

        GameObject pooledProjectile = objectPool.GetObjectFromPool(objectPool.ballistaProjectileName, true);
        pooledProjectile.GetComponent<Projectile2>().target = enemies[0].transform;

        pooledProjectile.transform.position = projectilePos.position;
        pooledProjectile.SetActive(true);

        yield return null;


    }



    void LookAtTarget()
    {
        if (enemies.Count == 0) return;

        Vector3 dir = enemies[0].transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * towerData.weaponRotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(rotation);

    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}



