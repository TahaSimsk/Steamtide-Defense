using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, ((BallData)projectileData).ExplosionRadius, ((BallData)projectileData).enemyLayer);
            
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyHealth>().ReduceHealth(projectileData.ProjectileDamage);
            }
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ((BallData)projectileData).ExplosionRadius);
    }
}
