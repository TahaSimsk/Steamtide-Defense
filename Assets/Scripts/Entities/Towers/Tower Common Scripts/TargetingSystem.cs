using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField][Range(0, 10)] public int closestTargetPoint = 1;
    [SerializeField][Range(0, 10)] public int selectedTargetPoint = 2;
    [SerializeField][Range(0, 10)] public int lowestHpTargetPoint = 5;
    [SerializeField][Range(0, 10)] public int highestHpTargetPoint = 3;
    [SerializeField][Range(0, 10)] public int fastestTargetPoint = 3;
    [SerializeField][Range(0, 10)] public int slowestTargetPoint = 3;
    [SerializeField][Range(0, 10)] public int clusterTargetPoint = 3;

    [SerializeField] LayerMask enemyLayer;

    GameObject previousTarget;

    [HideInInspector] public GameObject CurrentTarget;

    [HideInInspector] public List<GameObject> SortedTargetsByPoint = new List<GameObject>();


    // Main method to get the target from a list of enemies in range
    public void GetTarget(List<GameObject> targetsInRange)
    {
        // Dictionary to hold enemy objects and their calculated points
        Dictionary<GameObject, float> enemyPointPairs = new Dictionary<GameObject, float>();

        // Initialize points for each enemy
        foreach (var enemy in targetsInRange)
        {
            enemyPointPairs.Add(enemy, 0);
        }

        // If the current target is not in range, set it to null
        if (!targetsInRange.Contains(CurrentTarget))
        {
            CurrentTarget = null;
        }

        // Add points to the currently selected target
        AddSelectedTargetPoint(enemyPointPairs);

        // Calculate points for all targets based on different criteria
        CalculateTargetPoints(enemyPointPairs);

        // Get the target with the highest points
        GameObject highestPointGameObject = GetHighestPointTarget(enemyPointPairs);

        // Finalize the target selection process
        FinilizeTargetSelection(enemyPointPairs, highestPointGameObject);
    }


    // Adds points to the currently selected target
    private void AddSelectedTargetPoint(Dictionary<GameObject, float> enemyPointPairs)
    {
        if (CurrentTarget != null)
        {
            enemyPointPairs[CurrentTarget] += selectedTargetPoint;
        }
    }


    // Calculates points for all targets based on various criteria
    private void CalculateTargetPoints(Dictionary<GameObject, float> enemyPointPairs)
    {
        #region Dictionaries and variables for different criteria

        Dictionary<GameObject, float> enemyDistancePairs = new Dictionary<GameObject, float>();
        float shortestDistance = Mathf.Infinity;

        Dictionary<GameObject, float> enemyHPPairs = new Dictionary<GameObject, float>();
        float lowestEnemyHp = Mathf.Infinity;
        float highestEnemyHp = 0;

        Dictionary<GameObject, float> enemyMovementSpeedPairs = new Dictionary<GameObject, float>();
        float lowestMoveSpeed = Mathf.Infinity;
        float highestMoveSpeed = 0;

        Dictionary<GameObject, float> enemyClusterPairs = new Dictionary<GameObject, float>();
        int highestEnemyCount = 0;

        #endregion

        // Store values for each criterion for all targets
        foreach (var pair in enemyPointPairs)
        {
            StoreDistanceValues(pair.Key, enemyDistancePairs, ref shortestDistance);
            StoreHealthValues(pair.Key, enemyHPPairs, ref lowestEnemyHp, ref highestEnemyHp);
            StoreMovementSpeedValues(pair.Key, enemyMovementSpeedPairs, ref lowestMoveSpeed, ref highestMoveSpeed);
            StoreClusterValues(pair.Key, enemyClusterPairs, ref highestEnemyCount);
        }

        // Temporary dictionary to avoid modifying the original during iteration
        Dictionary<GameObject, float> tempDictionary = new Dictionary<GameObject, float>(enemyPointPairs);

        // Update points for each target based on the stored values
        foreach (var pair in tempDictionary)
        {
            CalculatePoint(enemyPointPairs, enemyDistancePairs, pair.Key, shortestDistance, closestTargetPoint, true);

            CalculatePoint(enemyPointPairs, enemyHPPairs, pair.Key, lowestEnemyHp, lowestHpTargetPoint, true);
            CalculatePoint(enemyPointPairs, enemyHPPairs, pair.Key, highestEnemyHp, highestHpTargetPoint, false);

            CalculatePoint(enemyPointPairs, enemyMovementSpeedPairs, pair.Key, lowestMoveSpeed, slowestTargetPoint, true);
            CalculatePoint(enemyPointPairs, enemyMovementSpeedPairs, pair.Key, highestMoveSpeed, fastestTargetPoint, false);

            CalculatePoint(enemyPointPairs, enemyClusterPairs, pair.Key, highestEnemyCount, clusterTargetPoint, false);
        }
    }


    // Stores the distance value for each enemy and updates the shortest distance
    void StoreDistanceValues(GameObject enemy, Dictionary<GameObject, float> dictionary, ref float shortestValue)
    {
        if (closestTargetPoint == 0) return;

        float currentValue = (transform.position - enemy.transform.position).sqrMagnitude;

        dictionary.Add(enemy, currentValue);

        if (currentValue < shortestValue)
        {
            shortestValue = currentValue;
        }
    }


    // Stores the health value for each enemy and updates the lowest and highest health values
    void StoreHealthValues(GameObject enemy, Dictionary<GameObject, float> dictionary,
        ref float lowestValue, ref float highestValue)
    {
        if (highestHpTargetPoint == 0 && lowestHpTargetPoint == 0) return;

        float currentValue = enemy.GetComponent<EnemyHealth>().CurrentHealth;

        dictionary.Add(enemy, currentValue);

        if (currentValue < lowestValue)
        {
            lowestValue = currentValue;
        }
        if (currentValue > highestValue)
        {
            highestValue = currentValue;
        }
    }


    // Stores the movement speed value for each enemy and updates the lowest and highest speeds
    void StoreMovementSpeedValues(GameObject enemy, Dictionary<GameObject, float> dictionary,
        ref float lowestValue, ref float highestValue)
    {
        if (fastestTargetPoint == 0 && slowestTargetPoint == 0) return;

        float currentValue = enemy.GetComponent<EnemyMovement>().CurrentMoveSpeed;

        dictionary.Add(enemy, currentValue);

        if (currentValue < lowestValue)
        {
            lowestValue = currentValue;
        }
        if (currentValue > highestValue)
        {
            highestValue = currentValue;
        }
    }


    // Stores the cluster value for each enemy and updates the highest enemy count
    void StoreClusterValues(GameObject enemy, Dictionary<GameObject, float> dictionary, ref int highestEnemyCount)
    {
        if (clusterTargetPoint == 0) return;

        Collider[] enemies = Physics.OverlapSphere(enemy.transform.position, 10f, enemyLayer);

        int numOfTotalEnemiesAroundEnemy;

        if (enemies != null)
        {
            numOfTotalEnemiesAroundEnemy = enemies.Length;
        }
        else
        {
            numOfTotalEnemiesAroundEnemy = 0;
        }

        dictionary.Add(enemy, numOfTotalEnemiesAroundEnemy);

        if (numOfTotalEnemiesAroundEnemy > highestEnemyCount)
        {
            highestEnemyCount = numOfTotalEnemiesAroundEnemy;
        }
    }


    // Calculates points for each criterion and updates the point dictionary
    void CalculatePoint(Dictionary<GameObject, float> pointDictionary, Dictionary<GameObject,
        float> valueDictionary, GameObject enemy, float referenceValue, int point, bool isLowest)
    {
        if (point == 0) return;

        if (isLowest)
        {
            float finalPoint = referenceValue / valueDictionary[enemy] * point;
            pointDictionary[enemy] += finalPoint;

        }
        else
        {
            float finalPoint = valueDictionary[enemy] / referenceValue * point;
            pointDictionary[enemy] += finalPoint;
        }
    }


    // Recalculates total points and returns the enemy with the highest points
    private GameObject GetHighestPointTarget(Dictionary<GameObject, float> enemyPointPairs)
    {
        SortedTargetsByPoint.Clear();


        var sortedTargetPairs = enemyPointPairs.OrderByDescending(x => x.Value).ToList();

        foreach (var sortedTarget in sortedTargetPairs)
        {
            SortedTargetsByPoint.Add(sortedTarget.Key);
        }

        return sortedTargetPairs[0].Key;
    }


    // Finalizes the target selection process
    private void FinilizeTargetSelection(Dictionary<GameObject, float> enemyPointPairs,
        GameObject highestPointGameObject)
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
