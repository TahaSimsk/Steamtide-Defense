using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(DestroyWhenNoCollision());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().ReduceHealth(15f);

            //play sfx
            //play anim

            Destroy(gameObject);
        }
    }

    IEnumerator DestroyWhenNoCollision()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
