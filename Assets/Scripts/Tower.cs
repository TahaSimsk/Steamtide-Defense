using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowParent;


    [SerializeField] float aimRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float shootingSpeed;
    [SerializeField] float shootingDelay;

    Rigidbody rb;

    ObjectPool objectPool;
    GameObject pooledArrow;
    Transform target;

    public bool canShoot;

    GameObject[] enemies;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        GetArrow();
    }

    void Update()
    {
        LookAtTarget();

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
                canShoot = true;

                target = enemies[i].transform;
                transform.LookAt(enemies[i].transform);
                //shoot target
                rb.AddForce(Vector3.forward * 100 * Time.deltaTime, ForceMode.Impulse);
            }

        }

    }

    void GetArrow()
    {

        target = null;
        canShoot = false;
        pooledArrow = null;
        
        pooledArrow = objectPool.GetWeaponBallistaArrow();
        rb = pooledArrow.GetComponent<Rigidbody>();
        pooledArrow.SetActive(true);
        pooledArrow.transform.position = transform.position + Vector3.up * 2;



    }

}



