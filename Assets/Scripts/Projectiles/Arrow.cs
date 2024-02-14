using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    int pierceCount;
    float timer;
    ArrowData dataArrow;

    private void Start()
    {
        dataArrow = (ArrowData)projectileData;
    }

    protected override void Update()
    {
        MoveToTarget();
    }

    protected override void MoveToTarget()
    {
        timer += Time.deltaTime;
        transform.position += transform.forward * dataArrow.ProjectileSpeed * Time.deltaTime;
        if (timer >= dataArrow.projectileLife)
        {
            DeactivateProjectile();
        }

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHealth>().ReduceHealth(dataArrow.ProjectileDamage);




        //if (Random.Range(0, 100) <= dataArrow.chanceToDropPool)
        //{
        //    RaycastHit hit;
        //    if (Physics.SphereCast(transform.position, 1f, Vector3.down, out hit, 5f))
        //    {
        //        if (hit.transform.CompareTag("Path2"))
        //        {
        //            Instantiate(dataArrow.poisonPool, hit.transform.position, Quaternion.identity);

        //        }

        //    }
        //}
        if (dataArrow.canPierce)
        {
            pierceCount++;
            if (pierceCount > dataArrow.pierceLimit)
            {
                DeactivateProjectile();
            }
        }
        else
        {
            DeactivateProjectile();
        }


    }

    void DeactivateProjectile()
    {
        timer = 0;
        pierceCount = 0;
        gameObject.SetActive(false);
    }
}
