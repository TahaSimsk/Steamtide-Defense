using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : Projectile
{
    int pierceCount;

    float timer;
    ArrowData dataArrow;
    bool canPoison;

    protected override void OnEnable()
    {
        base.OnEnable();
        dataArrow = (ArrowData)projectileData;
        timer = 0;
        pierceCount = 0;
        if (initiated)
            canPoison = dataArrow.canPoison;
    }

    protected override void Update()
    {
        MoveToTarget();
    }

    protected override void MoveToTarget()
    {
        if (target != null)
            transform.LookAt(target.position + Vector3.up * 4);
        target = null;
        timer += Time.deltaTime;
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        if (timer >= dataArrow.projectileLife)
        {
            Debug.Log("deactivated bc timer" + timer);
            gameObject.SetActive(false);
        }

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHealth>().ReduceHealth(damage);


        PoisonBehaviour(other);



        PierceBehaviour();

    }

    private void PoisonBehaviour(Collider other)
    {
        if (canPoison)
        {
            if (Random.Range(0, 100) <= dataArrow.poolDropChance)
            {
                RaycastHit hit;
                if (Physics.SphereCast(other.transform.position + Vector3.up * 4f, 1f, Vector3.down, out hit, 6f, dataArrow.poolLayer))
                {
                    PoisonField pool = hit.transform.GetComponent<PoisonField>();

                    if (hit.transform.CompareTag("Path2") && pool == null)
                    {
                        GameObject poolObject = Instantiate(dataArrow.poisonPool, hit.transform.position + Vector3.up * 2, Quaternion.identity);
                        poolObject.GetComponent<PoisonField>().SetDurationsAndDamage(dataArrow.poolDuration, dataArrow.poisonDurationOnEnemies, dataArrow.poolDamage);
                    }
                    else if (pool != null)
                    {
                        pool.SetDurationsAndDamage(dataArrow.poolDuration, dataArrow.poisonDurationOnEnemies, dataArrow.poolDamage);
                    }

                }

            }
            canPoison = !dataArrow.dropPoolOnFirstEnemy;
        }
    }

    private void PierceBehaviour()
    {
        if (dataArrow.canPierce)
        {
            pierceCount++;

            Debug.Log("pierce count: " + pierceCount);
            if (pierceCount > dataArrow.pierceLimit)
            {

                Debug.Log("deactivated ");
                gameObject.SetActive(false);
                return;
            }
            damage = dataArrow.ProjectileDamage - (dataArrow.ProjectileDamage * dataArrow.pierceDamage[pierceCount - 1] * 0.01f);
        }
        else
        {
            Debug.Log("deactivated ");
            gameObject.SetActive(false);
        }
    }
}
