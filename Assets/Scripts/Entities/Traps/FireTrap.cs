using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float damage;
    public XPManager xpManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (HelperFunctions.CheckImmunity(other.gameObject, Immunity.AllTraps)) return;

            if (other.GetComponent<EnemyHealth>().ReduceHealth(damage))
            {
                xpManager.Anan();
            }
            
            Destroy(gameObject, 0.1f);
        }
    }
}

