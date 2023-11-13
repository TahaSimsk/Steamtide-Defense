using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Weapon Type")]
    [SerializeField] bool ballista;
    [SerializeField] bool blaster;
    [SerializeField] bool cannon;

    [Header("Weapon Attributes")]
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float projectileLife;
    [SerializeField] float shootingDelay;

    [Header("Projectile Positions")]
    [SerializeField] GameObject ballistaProjectilePos;
    [SerializeField] GameObject blasterProjectilePos;
    [SerializeField] GameObject cannonProjectilePos;


    ObjectPool objectPool;
    GameObject pooledProjectile;
    List<GameObject> enemies = new List<GameObject>();
    Rigidbody pooledProjectileRb;


    bool hasProjectile, isProjectileFired;

    float timerForShootingDelay, timerForProjectileLife;



    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

    }

    void Update()
    {
        LookAtEnemy();
        SearchListToRemoveEnemy();

        if (ballista)
            GetProjectile(objectPool.GetWeaponBallistaArrow(), ballistaProjectilePos);
        if (blaster)
            GetProjectile(objectPool.GetWeaponBlasterLaser(), blasterProjectilePos);
        if (cannon)
            GetProjectile(objectPool.GetWeaponCannonBall(), cannonProjectilePos);

        Shoot();
        DeactivateProjectile();
    }




    void GetProjectile(GameObject projectileFromPool, GameObject gameObjectToSetProjectilePosition)
    {
        timerForShootingDelay -= Time.deltaTime;

        if (!hasProjectile && timerForShootingDelay <= 0)
        {
            if (ballista)
            {
                ballistaProjectilePos.SetActive(true);
            }
            pooledProjectile = projectileFromPool;
            pooledProjectileRb = pooledProjectile.GetComponent<Rigidbody>();
            pooledProjectile.GetComponent<Projectile>().projetileDmg = this.projectileDmg;


            pooledProjectile.transform.parent = gameObjectToSetProjectilePosition.transform.parent;
            pooledProjectile.transform.SetPositionAndRotation(gameObjectToSetProjectilePosition.transform.position, gameObjectToSetProjectilePosition.transform.rotation);

            hasProjectile = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }


    void LookAtEnemy()
    {
        if (enemies.Count == 0)
        {
            return;
        }

        transform.LookAt(enemies[0].transform.position);
    }



    void SearchListToRemoveEnemy()
    {
        if (enemies.Count == 0)
        {
            return;
        }
        foreach (var enemy in enemies.ToList())
        {
            if (enemy.GetComponent<EnemyHealth>().isDead || enemy.GetComponent<EnemyMovement>().reachedEnd)
            {
                enemies.Remove(enemy);
            }
        }
    }



    void Shoot()
    {

        if (enemies.Count > 0 && pooledProjectile != null)
        {
            if (!isProjectileFired)
            {
                if (ballista)
                {
                    ballistaProjectilePos.SetActive(false);
                }
                pooledProjectile.SetActive(true);
                pooledProjectileRb.AddRelativeForce(Vector3.forward * projectileSpeed);

                isProjectileFired = true;
                pooledProjectile.transform.parent = objectPool.gameObject.transform;
            }
        }
    }


    void DeactivateProjectile()
    {
        if (isProjectileFired)
        {
            HandleProjectileFired();

            if (timerForProjectileLife >= projectileLife)
            {
                HandleProjectileHit();
                return;
            }
            else if (pooledProjectile.GetComponent<Projectile>().hit)
            {
                HandleProjectileHit();
                return;
            }
        }
    }

    void HandleProjectileFired()
    {
        hasProjectile = false;
        timerForShootingDelay = shootingDelay;
        timerForProjectileLife = 0;
        timerForProjectileLife += Time.deltaTime;
    }


    void HandleProjectileHit()
    {
        pooledProjectile.SetActive(false);
        isProjectileFired = false;
        pooledProjectileRb.velocity = Vector3.zero;
        pooledProjectileRb = null;
        pooledProjectile = null;
    }
}



