using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public bool hit;
    [HideInInspector]
    public float projetileDmg;

    private void OnEnable()
    {
        hit = false;

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
