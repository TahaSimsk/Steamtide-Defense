using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    [SerializeField] float bombRadius;
    [SerializeField] LayerMask enemyLayer;
    protected override void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, bombRadius, enemyLayer);
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyHealth>().ReduceHealth(projectileData.projectileDamage);
                hitEnemy = true;
            }
        }
    }
}
