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
    [SerializeField] float aimRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float projectileLife;
    [SerializeField] float shootingDelay;

    [Header("Projectile Positions")]
    [SerializeField] GameObject ballistaProjectile;
    [SerializeField] GameObject blasterProjectile;
    [SerializeField] GameObject cannonProjectile;


    ObjectPool objectPool;
    GameObject pooledProjectile;

    Rigidbody rb;


    bool canShoot, hasProjectile, spedUp;

    float timer, timer2;

    GameObject[] enemies;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        LookAtTarget();
        if (ballista)
            GetProjectile(objectPool.GetWeaponBallistaArrow(), ballistaProjectile);
        if (blaster)
            GetProjectile(objectPool.GetWeaponBlasterLaser(), blasterProjectile);
        if (cannon)
            GetProjectile(objectPool.GetWeaponCannonBall(), cannonProjectile);
        Shoot();
    }

    void LookAtTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null || !enemies[i].activeInHierarchy) { return; }

            float distance = Vector3.Distance(enemies[i].transform.position, transform.position);

            if (distance < aimRange)
            {
                transform.LookAt(enemies[i].transform);
                canShoot = true;
            }
        }
    }

    void Shoot()
    {
        if (canShoot && pooledProjectile != null)
        {
            timer2 += Time.deltaTime;

            if (!spedUp)
            {
                if (ballista)
                {
                    ballistaProjectile.SetActive(false);
                }
                pooledProjectile.SetActive(true);
                rb.AddRelativeForce(Vector3.forward * projectileSpeed);
                transform.parent = objectPool.gameObject.transform;
                spedUp = true;
            }

            //rb.AddRelativeForce(Vector3.forward * projectileSpeed * 10);
            //pooledArrow.transform.parent = transform.parent;




            //pooledArrow.transform.localPosition += Vector3.forward * projectileSpeed * Time.deltaTime;

            if (timer2 >= projectileLife)
            {
                timer2 = 0;
                DeactivateProjectile();
                return;
            }
            else if (pooledProjectile.GetComponent<Projectile>().hit)
            {
                DeactivateProjectile();
                return;
            }
        }
    }


    void DeactivateProjectile()
    {
        pooledProjectile.SetActive(false);
        if (ballista)
        {
            ballistaProjectile.SetActive(true);
        }
        hasProjectile = false;
        spedUp = false;
        timer = shootingDelay;
        timer2 = 0;
        rb.velocity = Vector3.zero;
        rb = null;
        pooledProjectile = null;
        canShoot = false;
    }

    void GetProjectile(GameObject projectileFromPool, GameObject gameObjectToSetProjectilePosition)
    {
        timer -= Time.deltaTime;

        if (!hasProjectile && timer <= 0)
        {
            pooledProjectile = projectileFromPool;
            rb = pooledProjectile.GetComponent<Rigidbody>();
            pooledProjectile.GetComponent<Projectile>().projetileDmg = this.projectileDmg;


            pooledProjectile.transform.parent = gameObjectToSetProjectilePosition.transform.parent;
            pooledProjectile.transform.SetPositionAndRotation(gameObjectToSetProjectilePosition.transform.position, gameObjectToSetProjectilePosition.transform.rotation);

            hasProjectile = true;
        }
    }
}



