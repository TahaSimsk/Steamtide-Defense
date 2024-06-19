using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, ((CannonData)towerData).ExplosionRadius, ((CannonData)towerData).enemyLayer);

            foreach (var enemy in enemies)
            {
                if (enemy.GetComponent<EnemyHealth>().ReduceHealth(towerData.ProjectileDamage))
                {
                    xpManager.Anan();
                }
            }
            gameObject.SetActive(false);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ((CannonData)towerData).ExplosionRadius);
    }
}
