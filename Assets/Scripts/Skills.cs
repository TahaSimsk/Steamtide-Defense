using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    public float bombDamage;

    private void Start()
    {
        StartCoroutine(DestroyWhenNoCollision());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().ReduceHealth(bombDamage);

            //play sfx
            //play anim

            Destroy(gameObject);
        }
    }

    public void PassBombDamage(float value)
    {
        bombDamage = value;
    }

    IEnumerator DestroyWhenNoCollision()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
