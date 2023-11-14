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
    public List<GameObject> enemies = new List<GameObject>();
    Rigidbody pooledProjectileRb;



    bool hasProjectile, isProjectileFired;

    float timerForShootingDelay, timerForProjectileLife;



    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

    }

    void Update()
    {
        if (ballista)
            GetProjectile(objectPool.GetWeaponBallistaArrow(), ballistaProjectilePos);
        if (blaster)
            GetProjectile(objectPool.GetWeaponBlasterLaser(), blasterProjectilePos);
        if (cannon)
            GetProjectile(objectPool.GetWeaponCannonBall(), cannonProjectilePos);


        LookAtEnemy();
        Shoot();
        SearchListToRemoveEnemy();
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
            pooledProjectile.GetComponent<Projectile>().currentTower = this.transform;
            //projectile.projetileDmg = this.projectileDmg;
            //projectile.currentTower = this.transform;


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
                isProjectileFired = true;
                if (ballista)
                {
                    ballistaProjectilePos.SetActive(false);
                }
                pooledProjectile.SetActive(true);
                pooledProjectile.GetComponent<Projectile>().GetInfo(enemies[0].transform, projectileSpeed, projectileDmg, true);

                pooledProjectile.transform.parent = objectPool.gameObject.transform;
            }
        }
    }


    void DeactivateProjectile()
    {
        if (isProjectileFired)
        {


            timerForProjectileLife += Time.deltaTime;
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




    void HandleProjectileHit()
    {
        hasProjectile = false;
        timerForShootingDelay = shootingDelay;
        timerForProjectileLife = 0;

        pooledProjectileRb.velocity = Vector3.zero;
        pooledProjectile.SetActive(false);
        pooledProjectileRb = null;
        pooledProjectile = null;

        isProjectileFired = false;
    }
}



