using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public bool hit;
    [HideInInspector]
    public float projetileDmg;
    Rigidbody rb;

    public Transform currentTower;

    Transform target;
    Vector3 targetPos;
    float projectileSpeed;
    bool canShoot;

    private void OnEnable()
    {
        hit = false;

    }

    //private void Update()
    //{
    //    if (rb.velocity.magnitude>0)
    //    {

    //    }
    //}

    private void OnDisable()
    {
        target = null;

        canShoot = false;
        currentTower = null;

    }

    private void Update()
    {
        if (canShoot)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                hit = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed * Time.deltaTime);
            transform.LookAt(target.position);

        }
        
        
    }

    public void GetInfo(Transform target, float projectileSpeed, float projectileDmg, bool canShoot)
    {
        this.target = target;
        this.projectileSpeed = projectileSpeed;
        this.projetileDmg = projectileDmg;
        this.canShoot = canShoot;

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hit = true;

            other.GetComponent<EnemyHealth>().ReduceHealth(projetileDmg);

        }
    }
}
