using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonField : MonoBehaviour
{

    float poolDuration = 0.1f;
    float poisonDuration;
    float poisonDamage;



    private void Update()
    {
        poolDuration -= Time.deltaTime;
        if (poolDuration <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Poisonable poisonable = other.GetComponent<Poisonable>();
        if (poisonable == null) return;
        poisonable.GetPoisoned(poisonDuration, poisonDamage);
    }

    public void SetDurationsAndDamage(float poolDuration, float poisonDuration, float poisonDamage)
    {
        this.poolDuration = poolDuration;
        this.poisonDuration = poisonDuration;
        this.poisonDamage = poisonDamage;
    }


}
