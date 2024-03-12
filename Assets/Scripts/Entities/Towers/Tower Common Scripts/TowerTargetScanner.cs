using UnityEngine;

public class TowerTargetScanner : TargetScanner
{
    [SerializeField] GameEvent1ParamSO onEnemyReachedEnd;
    [SerializeField] RangeUpgrade rangeUpgrade;
    TowerData towerData;
    private void Start()
    {
        towerData = objectInfo.InstTowerData;
        ChangeRange();
        rangeUpgrade.OnRangeUpgraded += ChangeRange;
    }

    private void OnDestroy()
    {
        rangeUpgrade.OnRangeUpgraded -= ChangeRange;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onEnemyReachedEnd.onEventRaised += RemoveTarget;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onEnemyReachedEnd.onEventRaised -= RemoveTarget;

    }

    public void ChangeRange()
    {
        transform.localScale = new Vector3(towerData.TowerRange, 0.1f, towerData.TowerRange);
    }
}
