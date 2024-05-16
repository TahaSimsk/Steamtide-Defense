using UnityEngine;

public class EnemyProjectile : Projectile
{
    EnemyData enemyData;

    protected override void OnEnable()
    {
        if (!initiated) return;
        onEnemyDeath.onEventRaised += CompareEnemy;
        damage = enemyData.Damage;
        projectileSpeed = enemyData.ProjectileSpeed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        IPlayerDamageable playerDamageable = other.GetComponent<IPlayerDamageable>();
        if (playerDamageable != null)
        {
            playerDamageable.GetDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public override void SetProjectile(GameData data)
    {
        enemyData = data as EnemyData;
    }
}