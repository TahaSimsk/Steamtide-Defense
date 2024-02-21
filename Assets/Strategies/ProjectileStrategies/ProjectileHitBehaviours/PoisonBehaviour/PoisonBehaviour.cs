using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileHitBehaviours/PoisonBehaviour")]
public class PoisonBehaviour : ProjectileHitBehaviours
{
    [field: SerializeReference] public override float UpgradeCost { get; set; }
    public bool canPoison;
    public bool poisonFirstEnemy;
    public float chanceToDropPool;
    public LayerMask poolLayer;
    public GameObject poisonPool;
    public float poisonPoolDuration;
    public float poisonDurationOnEnemies;
    public float poisonDamage;
    public override string NameOfType { get; set; } = "poisonHitBehaviour";

    

    public override void Collide(Transform transform, Collider collider, ref float currentDamage, float baseDamage)
    {
        if (canPoison)
        {
            if (Random.Range(0, 100) <= chanceToDropPool)
            {
                RaycastHit hit;
                if (Physics.SphereCast(collider.transform.position + Vector3.up * 4, 1f, Vector3.down, out hit, 6f, poolLayer))
                {
                    PoisonField pool = hit.transform.GetComponent<PoisonField>();

                    if (hit.transform.CompareTag("Path2") && pool == null)
                    {
                        GameObject poolObject = Instantiate(poisonPool, hit.transform.position + Vector3.up * 2, Quaternion.identity);
                        poolObject.GetComponent<PoisonField>().SetDurationsAndDamage(poisonPoolDuration, poisonDurationOnEnemies, poisonDamage);
                    }
                    else if (pool != null)
                    {
                        pool.SetDurationsAndDamage(poisonPoolDuration, poisonDurationOnEnemies, poisonDamage);
                    }
                    canPoison = !poisonFirstEnemy;
                }
            }
        }
    }

    public override void WhenEnabled()
    {
        canPoison = true;
    }
}