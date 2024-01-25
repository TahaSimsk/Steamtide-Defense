using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillSlow : MonoBehaviour
{
    [HideInInspector]
    public float slowPercent;
    [HideInInspector]
    public float slowDuration;


    List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
    void Start()
    {
        StartCoroutine(StopSlowing());
    }

    IEnumerator StopSlowing()
    {

        yield return new WaitForSeconds(slowDuration);
        foreach (var item in enemyMovements)
        {
            item.ResetMoveSpeed();
        }
        Destroy(gameObject);


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovements.Add(enemyMovement);
            enemyMovement.ChangeMoveSpeed(slowPercent);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().ResetMoveSpeed();
        }
    }


}
