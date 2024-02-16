using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected IProjectile projectileData;
    public GameEvent1ParamSO onEnemyDeath;
    public Transform target;
    bool initiated = false;

    protected Vector3 targetPos;

    public List<ProjectileHitBehaviours> hitBehaviours;

    protected virtual void OnEnable()
    {
        if (!initiated) return;
        onEnemyDeath.onEventRaised += CompareEnemy;
    }

    private void OnDisable()
    {
        onEnemyDeath.onEventRaised -= CompareEnemy;
        initiated = true;
    }

    protected virtual void Update()
    {
        MoveToTarget();
    }

    protected virtual void MoveToTarget()
    {
        if (target != null)
        {
            targetPos = target.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileData.ProjectileSpeed * Time.deltaTime);
        if ((gameObject.transform.position - targetPos).sqrMagnitude < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }


    /*
     * if projectile hits an enemy, damage it and set hitEnemy to true to stop movement of projectile, terminate while loop and therefore deactivate projectile.
     */

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().ReduceHealth(projectileData.ProjectileDamage);
            gameObject.SetActive(false);
        }

        
    }

    /*
     * this method checks whether any dead and deactivated enemy is our current target.
     * this is done with EnemyHealth script that is attached to enemies and invokes OnEnemyDeath delegate when it dies.
     * if the dead enemy is our current target we are setting the target to null because we dont want this projectile to follow it to strange places
     */
    void CompareEnemy(object enemy)
    {
        if (enemy is GameObject)
        {
            if (target == null) return;

            if ((GameObject)enemy == target.gameObject)
            {
                target = null;
            }
        }

    }

    public void SetProjectile(IProjectile projectile)
    {
        projectileData = projectile;
    }

}

