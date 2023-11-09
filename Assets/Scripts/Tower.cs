using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject arrow;


    [SerializeField] float aimRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float projectileLife;
    [SerializeField] float shootingDelay;


    ObjectPool objectPool;
    GameObject pooledArrow;

    Rigidbody rb;


    bool canShoot, hasArrow;

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
        if (canShoot && pooledArrow != null)
        {
            timer2 += Time.deltaTime;



            rb.AddRelativeForce(Vector3.forward * projectileSpeed * 10);
            //pooledArrow.transform.parent = transform.parent;
            



            //pooledArrow.transform.localPosition += Vector3.forward * projectileSpeed * Time.deltaTime;

            if (timer2 >= projectileLife)
            {
                timer2 = 0;
                DeactivateProjectile();
                return;
            }
            else if (pooledArrow.GetComponent<Projectile>().hit)
            {
                DeactivateProjectile();
                return;
            }
        }
    }


    void DeactivateProjectile()
    {
        pooledArrow.SetActive(false);
        hasArrow = false;
        timer = shootingDelay;
        timer2 = 0;
        rb.velocity = Vector3.zero;
        rb = null;
        pooledArrow = null;
        canShoot = false;
    }

    void GetArrow()
    {
        timer -= Time.deltaTime;

        if (!hasArrow && timer <= 0)
        {
            pooledArrow = objectPool.GetWeaponBallistaArrow();
            pooledArrow.SetActive(true);
            rb = pooledArrow.GetComponent<Rigidbody>();

            pooledArrow.transform.parent = arrow.transform.parent;
            pooledArrow.transform.SetPositionAndRotation(arrow.transform.position, arrow.transform.rotation);

            hasArrow = true;
        }
    }
}



