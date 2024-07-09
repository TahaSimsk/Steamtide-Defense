using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    GameObject previousTarget;

    [HideInInspector] public GameObject CurrentTarget;

    [SerializeField] int closestTargetPoint = 1;

    [SerializeField] int selectedTargetPoint = 2;

    [SerializeField] int lowestHpTargetPoint = 5;
    [SerializeField] int highestHpTargetPoint = 3;

    [SerializeField] int fastestTargetPoint = 3;
    [SerializeField] int slowestTargetPoint = 3;

    [SerializeField] int clusterTargetPoint = 3;


    public List<float> values = new List<float>();
    public List<GameObject> objects = new List<GameObject>();
    [SerializeField] LayerMask enemyLayer;


    public void Anan(List<GameObject> targetsInRange)
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

        HandleSelectedTargetTargetSelection(enemyPointPairs);

        HandleClosestTargetSelection(enemyPointPairs);

        HandleTargetSelectionBasedOnHealth(enemyPointPairs);

        HandleTargetSelectionBasedOnMoveSpeed(enemyPointPairs);

        HandleTargetSelectionBasedOnCluster(enemyPointPairs);


        GameObject highestPointGameObject = RecalculateTotalPointsAndReturnEnemyWithHighestPoint(enemyPointPairs);

        FinilizeTargetSelection(enemyPointPairs, highestPointGameObject);
    }


    private void HandleSelectedTargetTargetSelection(Dictionary<GameObject, float> enemyPointPairs)
    {
        if (CurrentTarget != null)
        {
            enemyPointPairs[CurrentTarget] += selectedTargetPoint;
        }
    }

    private void HandleClosestTargetSelection(Dictionary<GameObject, float> enemyPointPairs)
    {
        //calculate shortest distance and store distances to dictionary
        float shortestDistance = Mathf.Infinity;
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();

        foreach (var pair in enemyPointPairs)
        {
            float currentEnemyDistance = (transform.position - pair.Key.transform.position).sqrMagnitude;

            enemyDistancePairs.Add(pair.Key, currentEnemyDistance);

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

    #region Test

    private void HandleClosestTargetSelection2(Dictionary<GameObject, float> enemyPointPairs)
    {
        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();
        float shortestDistance = Mathf.Infinity;

        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHP = Mathf.Infinity;
        float highestHP = 0;

        Dictionary<GameObject, float> enemyMovementSpeedPairs = new Dictionary<GameObject, float>();
        float lowestMoveSpeed = Mathf.Infinity;
        float highestMoveSpeed = 0;

        Dictionary<GameObject, float> enemyClusterPairs = new Dictionary<GameObject, float>();
        int enemyCountInRange = 0;

        foreach (var pair in enemyPointPairs)
        {
            //calculate shortest distance and store distances to dictionary
         
            shortestDistance = distance(pair.Key, enemyDistancePairs, shortestDistance);


        }

        //add points to enemies based off of distance
        foreach (var pair in enemyDistancePairs)
        {
            enemyPointPairs[pair.Key] += shortestDistance / pair.Value * closestTargetPoint;
        }
    }

    float distance(GameObject go, Dictionary<GameObject, float> enemyDistancePairs, float shortestDistance)
    {
        float currentEnemyDistance = (transform.position - go.transform.position).sqrMagnitude;

        enemyDistancePairs.Add(go, currentEnemyDistance);

        if (currentEnemyDistance < shortestDistance)
        {
            shortestDistance = currentEnemyDistance;
        }

        return shortestDistance;
    }

    float hp(GameObject go, Dictionary<GameObject, float> enemyHPPairs, float shortestDistance)
    {
        return 0;
    }

    #endregion

    private void HandleTargetSelectionBasedOnHealth(Dictionary<GameObject, float> enemyPointPairs)
    {
        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHP = Mathf.Infinity;
        float highestHP = 0;
        foreach (var pair in enemyPointPairs)
        {
            float currentEnemyHp = pair.Key.GetComponent<EnemyHealth>().CurrentHealth;

            enemyHPPairs.Add(pair.Key, currentEnemyHp);

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
            float pointFromLowHp = lowestEnemyHP / pair.Value * lowestHpTargetPoint;
            float pointFromHighHp = pair.Value / highestHP * highestHpTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowHp + pointFromHighHp;
        }
    }


    private void HandleTargetSelectionBasedOnMoveSpeed(Dictionary<GameObject, float> enemyPointPairs)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        float lowestMoveSpeed = Mathf.Infinity;
        float highestMoveSpeed = 0;

        foreach (var pair in enemyPointPairs)
        {
            float currentEnemyMoveSpeed = pair.Key.GetComponent<EnemyMovement>().CurrentMoveSpeed;

            enemyValuePairs.Add(pair.Key, currentEnemyMoveSpeed);

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
            float pointFromLowMoveSpeed = lowestMoveSpeed / pair.Value * slowestTargetPoint;
            float pointFromHighMoveSpeed = pair.Value / highestMoveSpeed * fastestTargetPoint;

            enemyPointPairs[pair.Key] += pointFromLowMoveSpeed + pointFromHighMoveSpeed;
        }
    }


    private void HandleTargetSelectionBasedOnCluster(Dictionary<GameObject, float> enemyPointPairs)
    {
        Dictionary<GameObject, float> enemyValuePairs = new Dictionary<GameObject, float>();
        int enemyCountInRange = 0;
        foreach (var pair in enemyPointPairs)
        {
            Collider[] enemies = Physics.OverlapSphere(pair.Key.transform.position, 10f, enemyLayer);
            int currentEnemyEnemyCountInRange;
            if (enemies != null)
            {
                currentEnemyEnemyCountInRange = enemies.Length;
            }
            else
            {
                currentEnemyEnemyCountInRange = 0;
            }

            enemyValuePairs.Add(pair.Key, currentEnemyEnemyCountInRange);

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


    private GameObject RecalculateTotalPointsAndReturnEnemyWithHighestPoint(Dictionary<GameObject, float> enemyPointPairs)
    {
        GameObject highestPointGameObject = null;
        float highestPoint = 0;
        values.Clear();
        objects.Clear();
        foreach (var pair in enemyPointPairs)
        {
            values.Add(pair.Value);
            objects.Add(pair.Key);
            if (pair.Value > highestPoint)
            {
                highestPoint = pair.Value;
                highestPointGameObject = pair.Key;
            }
        }

        return highestPointGameObject;
    }


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

}
