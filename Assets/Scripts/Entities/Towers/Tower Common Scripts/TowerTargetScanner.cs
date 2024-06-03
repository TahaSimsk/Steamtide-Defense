using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerTargetScanner : TargetScanner
{
    [SerializeField] GameEvent1ParamSO onEnemyReachedEnd;
    [SerializeField] GameEvent1ParamSO onGlobalRangeUpgrade;
    [SerializeField] RangeUpgrade rangeUpgrade;
    TowerData towerData;

    float rangePercentage;
    private void Start()
    {
        towerData = objectInfo.InstTowerData;
        ChangeRange(GlobalPercantageManager.Instance.GlobalRangePercentage);
    }

  
    protected override void OnEnable()
    {
        base.OnEnable();
        onEnemyReachedEnd.onEventRaised += RemoveTarget;
        onGlobalRangeUpgrade.onEventRaised += ChangeRange;
        rangeUpgrade.OnRangeUpgraded += ChangeRange;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onEnemyReachedEnd.onEventRaised -= RemoveTarget;
        onGlobalRangeUpgrade.onEventRaised -= ChangeRange;
        rangeUpgrade.OnRangeUpgraded -= ChangeRange;

    }

    public void ChangeRange(object _amount)
    {
        if (_amount is float fl)
        {
            rangePercentage += fl;
            towerData.TowerRange = HelperFunctions.CalculatePercentage(objectInfo.DefTowerData.TowerRange, rangePercentage);
            transform.localScale = new Vector3(towerData.TowerRange, 0.1f, towerData.TowerRange);
        }

    }
}
