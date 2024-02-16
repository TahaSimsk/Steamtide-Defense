using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : Projectile
{
    int pierceCount;
    float timer;
    ArrowData dataArrow;
    float damage;
    bool init;


    protected override void OnEnable()
    {
        base.OnEnable();
        dataArrow = (ArrowData)projectileData;
        timer = 0;
        pierceCount = 0;
        if (init)
        {
            damage = dataArrow.ProjectileDamage;
        }
        else
        {
            init = true;
        }
        foreach (var behaviour in hitBehaviours)
        {
            behaviour.WhenEnabled();
        }
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
            gameObject.SetActive(false);
        }

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHealth>().ReduceHealth(damage);
        if (hitBehaviours.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        //PoisonBehaviour();


        foreach (var behaviour in hitBehaviours)
        {
            behaviour.Collide(transform, other, ref damage, dataArrow.ProjectileDamage);
        }


        //PierceBehaviour();

    }

    private void PoisonBehaviour()
    {
        if (dataArrow.canPoison)
        {
            if (Random.Range(0, 100) <= dataArrow.chanceToDropPool)
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 1f, Vector3.down, out hit, 5f, dataArrow.poolLayer))
                {
                    PoisonField pool = hit.transform.GetComponent<PoisonField>();

                    if (hit.transform.CompareTag("Path2") && pool == null)
                    {
                        GameObject poolObject = Instantiate(dataArrow.poisonPool, hit.transform.position + Vector3.up * 2, Quaternion.identity);
                        poolObject.GetComponent<PoisonField>().SetDurationsAndDamage(dataArrow.poisonPoolDuration, dataArrow.poisonDurationOnEnemies, dataArrow.poisonDamage);
                    }
                    else if (pool != null)
                    {
                        pool.SetDurationsAndDamage(dataArrow.poisonPoolDuration, dataArrow.poisonDurationOnEnemies, dataArrow.poisonDamage);
                    }
                }
            }
        }
    }

    private void PierceBehaviour()
    {
        if (dataArrow.canPierce)
        {
            pierceCount++;
            if (pierceCount > dataArrow.pierceLimit)
            {
                gameObject.SetActive(false);
                return;
            }
            damage = dataArrow.ProjectileDamage - (dataArrow.ProjectileDamage * dataArrow.pierceDamage[pierceCount - 1] * 0.01f);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
