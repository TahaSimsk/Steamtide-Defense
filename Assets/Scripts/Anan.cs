using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Anan : ScriptableObject
{
    [SerializeField] GameObject weapon;


    [SerializeField] float aimRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float projectileLife;
    [SerializeField] float shootingDelay;


    ObjectPool objectPool;
    GameObject pooledWeapon;


    bool canShoot, hasWeapon;

    float timer, timer2;

    GameObject[] enemies;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        LookAtTarget();
        GetArrow();
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
        if (canShoot && pooledWeapon != null)
        {
            timer2 += Time.deltaTime;

            pooledWeapon.transform.localPosition += Vector3.forward * projectileSpeed * Time.deltaTime;

            if (timer2 >= projectileLife)
            {
                timer2 = 0;
                DeactivateProjectile();
                return;
            }
            else if (pooledWeapon.GetComponent<Projectile>().hit)
            {
                DeactivateProjectile();
                return;
            }
        }
    }


    void DeactivateProjectile()
    {
        pooledWeapon.SetActive(false);
        hasWeapon = false;
        timer = shootingDelay;
        timer2 = 0;
        pooledWeapon = null;
        canShoot = false;
    }

    void GetArrow()
    {
        timer -= Time.deltaTime;

        if (!hasWeapon && timer <= 0)
        {
            pooledWeapon = objectPool.GetWeaponBallistaArrow();
            pooledWeapon.SetActive(true);

            pooledWeapon.transform.parent = weapon.transform.parent;
            pooledWeapon.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);

            hasWeapon = true;
        }
    }
}



