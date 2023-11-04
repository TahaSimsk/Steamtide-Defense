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



    ObjectPool objectPool;
    GameObject pooledArrow;
    Transform target;
    bool hasArrow;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        LookAtTarget();

    }

    void LookAtTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null || !enemies[i].activeInHierarchy) { return; }


            float distance = Vector3.Distance(enemies[i].transform.position, transform.position);


            if (distance < aimRange)
            {
                target = enemies[i].transform;
                transform.LookAt(enemies[i].transform);

            }

        }
        //transform.LookAt(enemies[i]);
        Shoot();
        
    }

    void Shoot()
    {
        if (target == null)
        {
            return;
        }
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < aimRange && !hasArrow)
        {
            pooledArrow = objectPool.GetWeaponBallistaArrow();
            hasArrow = true;
            pooledArrow.SetActive(true);
            pooledArrow.transform.parent = arrowParent.transform;
            pooledArrow.transform.SetPositionAndRotation(arrow.transform.position, arrow.transform.rotation);

            arrow.SetActive(false);
        }
        pooledArrow.transform.position = Vector3.MoveTowards(pooledArrow.transform.position, target.transform.position, projectileSpeed * Time.deltaTime);
    }
}
