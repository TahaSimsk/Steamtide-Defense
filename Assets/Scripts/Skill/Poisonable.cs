using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonable : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;

    float time;
    float damage;

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            float damagePersecond = damage * Time.deltaTime;
            enemyHealth.ReduceHealth(damagePersecond);
        }
    }

    public void GetPoisoned(float time, float damage)
    {
        this.time = time;
        this.damage = damage;
    }

    private void OnDisable()
    {
        time = 0;
    }
}
