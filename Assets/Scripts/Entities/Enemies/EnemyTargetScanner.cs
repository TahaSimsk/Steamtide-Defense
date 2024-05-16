using UnityEngine;

public class EnemyTargetScanner : TargetScanner
{

    private void Awake()
    {
        EnemyData enemyData = objectInfo.DefObjectGameData as EnemyData;
        float range = enemyData.Range;
        transform.localScale = new Vector3(range, 0.1f, range);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        targetsInRange.Clear();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        IPlayerDamageable playerDamageable = other.GetComponent<IPlayerDamageable>();
        if (playerDamageable != null)
        {
            targetsInRange.Add(other.gameObject);

        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        IPlayerDamageable playerDamageable = other.GetComponent<IPlayerDamageable>();
        if (playerDamageable != null)
        {
            targetsInRange.Remove(other.gameObject);
        }
    }

}
