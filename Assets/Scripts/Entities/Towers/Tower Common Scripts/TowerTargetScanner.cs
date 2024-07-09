using System.Collections.Generic;
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
        ChangeRange(GlobalPercentageManager.Instance.GlobalRangePercentage);
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

    GameObject previousTarget;

    [HideInInspector] public GameObject CurrentTarget;

    [SerializeField] int closestTargetPoint = 1;
    //[SerializeField] int furthestTargetPoint = 5;

    [SerializeField] int selectedTargetPoint = 2;

    [SerializeField] int lowestHpTargetPoint = 5;
    [SerializeField] int highestHpTargetPoint = 3;

    [SerializeField] int fastTargetPoint = 3;
    [SerializeField] int slowTargetPoint = 3;

    [SerializeField] int clusterTargetPoint = 3;


    //float distance = Mathf.Infinity;


    public void Anan()
    {

        Dictionary<GameObject, float> enemyPointPairs = new Dictionary<GameObject, float>();

        foreach (var enemy in targetsInRange)
        {
            enemyPointPairs.Add(enemy, 0);
        }

        if (!targetsInRange.Contains(CurrentTarget))
        {
            CurrentTarget = null;
        }

        HandleClosestTargetSelection(closestTargetPoint, enemyPointPairs);

        HandleTargetSelectionBasedOnHealth(enemyPointPairs, lowestHpTargetPoint);

        HandleSelectedTargetTargetSelection(selectedTargetPoint, enemyPointPairs);

        HandleTargetSelectionBasedOnMoveSpeed(enemyPointPairs, highestHpTargetPoint);

        HandleTargetSelectionBasedOnCluster(enemyPointPairs, clusterTargetPoint);

        GameObject highestPointGameObject = RecalculateTotalPointsAndReturnEnemyWithHighestPoint(enemyPointPairs);
        
        FinilizeTargetSelection(enemyPointPairs, highestPointGameObject);

    }


    #region
    private void FinilizeTargetSelection(Dictionary<GameObject, float> enemyPointPairs, GameObject highestPointGameObject)
    {
        CurrentTarget = highestPointGameObject;

       

        enemyPointPairs[CurrentTarget] += selectedTargetPoint;

        if (CurrentTarget == previousTarget)
        {
            enemyPointPairs[CurrentTarget] -= selectedTargetPoint;
        }

        previousTarget = CurrentTarget;
    }

    private void HandleSelectedTargetTargetSelection(int selectedTargetPoint, Dictionary<GameObject, float> enemyPointPairs)
    {
        if (CurrentTarget != null)
        {
            enemyPointPairs[CurrentTarget] += selectedTargetPoint;
        }
    }

    private GameObject RecalculateTotalPointsAndReturnEnemyWithHighestPoint(Dictionary<GameObject, float> enemyPointPairs)
    {
        GameObject highestPointGameObject = null;
        float highestPoint = 0;
        values.Clear();
        objects.Clear();
        foreach (var item in enemyPointPairs)
        {
            values.Add(item.Value);
            objects.Add(item.Key);
            if (item.Value > highestPoint)
            {
                highestPoint = item.Value;
                highestPointGameObject = item.Key;
            }
        }

        return highestPointGameObject;
    }

    #endregion




    private void HandleClosestTargetSelection(int closestTargetPoint, Dictionary<GameObject, float> enemyPointPairs)
    {
        //calculate shortest distance and store distances to dictionary
        float shortestDistance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();

        foreach (var enemy in targetsInRange)
        {
            float currentEnemyDistance = (transform.position - enemy.transform.position).sqrMagnitude;

            enemyDistancePairs.Add(enemy, currentEnemyDistance);

            if (currentEnemyDistance < shortestDistance)
            {
                shortestDistance = currentEnemyDistance;
            }
        }

        //add points to enemies based off of distance
        foreach (var pair in enemyDistancePairs)
        {
            enemyPointPairs[pair.Key] += shortestDistance / pair.Value * closestTargetPoint;
        }
    }

    private void HandleTargetSelectionBasedOnHealth(Dictionary<GameObject, float> enemyPointPairs, int lowestHPTargetPoint)
    {
        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHP = Mathf.Infinity;
        float highestHP = 0;
        foreach (var pair in targetsInRange)
        {
            float currentEnemyHp = pair.GetComponent<EnemyHealth>().CurrentHealth;

            enemyHPPairs.Add(pair, currentEnemyHp);

            if (currentEnemyHp < lowestEnemyHP)
            {
                lowestEnemyHP = currentEnemyHp;
            }
            if (currentEnemyHp > highestHP)
            {
                highestHP = currentEnemyHp;
            }

        }

        foreach (var pair in enemyHPPairs)
        {
            float pointFromLowHp = lowestEnemyHP / pair.Value * lowestHPTargetPoint;
            float pointFromHighHp = pair.Value / highestHP * highestHpTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowHp + pointFromHighHp;
        }

    }
    public List<float> values = new List<float>();
    public List<GameObject> objects = new List<GameObject>();
    [SerializeField] LayerMask enemyLayer;

    private void HandleTargetSelectionBasedOnMoveSpeed(Dictionary<GameObject, float> enemyPointPairs, int pointMultiplier)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        float lowestMoveSpeed = Mathf.Infinity;
        float highestMoveSpeed = 0;
        foreach (var enemy in targetsInRange)
        {
            float currentEnemyMoveSpeed = enemy.GetComponent<EnemyMovement>().CurrentMoveSpeed;

            enemyValuePairs.Add(enemy, currentEnemyMoveSpeed);

            if (currentEnemyMoveSpeed < lowestMoveSpeed)
            {
                lowestMoveSpeed = currentEnemyMoveSpeed;
            }
            if (currentEnemyMoveSpeed > highestMoveSpeed)
            {
                highestMoveSpeed = currentEnemyMoveSpeed;
            }
        }

        foreach (var pair in enemyValuePairs)
        {
            float pointFromLowMoveSpeed = lowestMoveSpeed / pair.Value * slowTargetPoint;
            float pointFromHighMoveSpeed = pair.Value / highestMoveSpeed * fastTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowMoveSpeed + pointFromHighMoveSpeed;
        }


    }

    private void HandleTargetSelectionBasedOnCluster(Dictionary<GameObject, float> enemyPointPairs, int pointMultiplier)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        int enemyCountInRange = 0;
        foreach (var pair in targetsInRange)
        {
            Collider[] enemies = Physics.OverlapSphere(pair.transform.position, 10f, enemyLayer);
            int currentEnemyEnemyCountInRange;
            if (enemies != null)
            {
                currentEnemyEnemyCountInRange = enemies.Length;
            }
            else
            {
                currentEnemyEnemyCountInRange = 0;
            }

            enemyValuePairs.Add(pair, currentEnemyEnemyCountInRange);

            if (currentEnemyEnemyCountInRange > enemyCountInRange)
            {
                enemyCountInRange = currentEnemyEnemyCountInRange;
            }
        }

        foreach (var pair in enemyValuePairs)
        {
            float pointFromHighestCluster = pair.Value / enemyCountInRange * clusterTargetPoint;

            enemyPointPairs[pair.Key] += pointFromHighestCluster;
        }


    }

}
