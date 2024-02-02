using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetScanner : MonoBehaviour
{
    Tower2 tower;

    private void Awake()
    {
        tower = GetComponentInParent<Tower2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.AddEnemy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.RemoveEnemy(other.gameObject);

        }
    }

}
