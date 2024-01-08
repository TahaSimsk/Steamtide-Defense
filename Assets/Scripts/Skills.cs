using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    bool canDamage;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.transform.position + Vector3.up * 2;
        }

        if (Input.GetMouseButtonDown(0))
        {
            canDamage = true;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && canDamage)
        {
            other.GetComponent<EnemyHealth>().ReduceHealth(15f);
            Destroy(gameObject);
        }
    }

    
}
