using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected DataProjectile projectileData;


    public Transform target;
    bool initiated = false;

   protected bool hitEnemy;

    private void OnEnable()
    {
        if (!initiated) return;

        EventManager.onEnemyDeath += CompareEnemy;
        StartCoroutine(MoveToTargetAndHandleCollision());
    }

    private void OnDisable()
    {
        EventManager.onEnemyDeath -= CompareEnemy;
        initiated = true;
        hitEnemy = false;
    }

    IEnumerator MoveToTargetAndHandleCollision()
    {
        /*
         * when this projectile gets activated it stores the position of the target's once
         */
        Vector3 targetPosition = target.position;

        while (true)
        {
            /*
             * then projectile moves to target's position not its transform
             */
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, projectileData.projectileSpeed * Time.deltaTime);


            /*
             * projectile sets its rotation to face target
             */
            transform.LookAt(targetPosition);

            /*
             * checks to see if target is not null, because while traveling to the target, it may get deactivated by another projectile. 
             * If it happens we dont want projectile to travel to its current position, we want projectile to travel to it's last known position.
             */
            if (target != null)
            {
                targetPosition = target.position;
            }

            yield return null;

            /*
             * Distance calculation to check whether projectile touches target, if it does while loop gets terminated
             */
            if ((transform.position - targetPosition).sqrMagnitude < 1f)
            {
                //projectile hit the target
                break;
            }

            /*
             * if projectile hits an enemy, stop moving the projectile and terminate while loop
             */
            if (hitEnemy)
            {
                break;
            }
        }

        /*
         * this line execudes after the while loop so it means we hit the target and while loop is terminated
         */


        transform.gameObject.SetActive(false);
    }

    /*
     * if projectile hits an enemy, damage it and set hitEnemy to true to stop movement of projectile, terminate while loop and therefore deactivate projectile.
     */

    protected virtual void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ReduceHealth(projectileData.projectileDamage);
            hitEnemy = true;
        }
    }

    /*
     * this method checks whether any dead and deactivated enemy is our current target.
     * this is done with EnemyHealth script that is attached to enemies and invokes OnEnemyDeath delegate when it dies.
     * if the dead enemy is our current target we are setting the target to null because we dont want this projectile to follow it to strange places
     */
    void CompareEnemy(GameObject enemy)
    {
        Debug.Log("Compared enemy");
        if (target == null) return;

        if (enemy == target.gameObject)
        {
            target = null;
            Debug.Log("Target is the same, removed");
        }
    }
}

