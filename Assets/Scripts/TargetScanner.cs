using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetScanner : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();

    Tower tower;

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();
    }

    private void Update()
    {
        SearchListToRemoveEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.AddEnemy(other.gameObject);
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            tower.RemoveEnemy(other.gameObject);
            
        }
    }



    void SearchListToRemoveEnemy()
    {
        if (enemies.Count == 0)
        {
            return;
        }
        foreach (var enemy in enemies.ToList())
        {
            if (enemy.GetComponent<EnemyHealth>().isDead || enemy.GetComponent<EnemyMovement>().reachedEnd)
            {
                tower.RemoveEnemy(enemy);
                enemies.Remove(enemy);
            }
        }
    }


}
