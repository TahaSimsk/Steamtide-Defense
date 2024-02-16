using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileHitBehaviours/NormalHitBehaviour")]
public class NormalHitBehaviour : ProjectileHitBehaviours
{
    public override string NameOfType { get; set; } = "normalHitBehaviour";

    public override void Collide(Transform transform, Collider collider, ref float currentDamage, float baseDamage)
    {
        EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
        if (enemyHealth == null) return;
        enemyHealth.ReduceHealth(currentDamage);
        transform.gameObject.SetActive(false);
    }

    public override void WhenEnabled()
    {

    }
}
