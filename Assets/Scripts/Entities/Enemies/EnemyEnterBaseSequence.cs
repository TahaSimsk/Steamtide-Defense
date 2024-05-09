using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class EnemyEnterBaseSequence : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onEnemyReachEndOfPath;
    [SerializeField] GameEvent1ParamSO onEnemyDeath;
    [SerializeField] List<GameObject> paths = new List<GameObject>();

    Dictionary<GameObject, GameObject> pathEnemyPairs = new Dictionary<GameObject, GameObject>();
    List<GameObject> enemyList = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            pathEnemyPairs.Add(paths[i], null);
        }
    }

    private void OnEnable()
    {
        onEnemyReachEndOfPath.onEventRaised += PositionEnemy;
        onEnemyDeath.onEventRaised += RemoveEnemy;
    }
    private void OnDisable()
    {
        onEnemyReachEndOfPath.onEventRaised -= PositionEnemy;
        onEnemyDeath.onEventRaised -= RemoveEnemy;
    }

    void PositionEnemy(object go)
    {
        GameObject enemy;
        EnemyMovement enemyMovement;
        if (go is GameObject _enemy)
        {
            enemyMovement = _enemy.GetComponent<EnemyMovement>();
            enemy = _enemy;
        }
        else
        {
            return;
        }

        if (pathEnemyPairs.ContainsValue(enemy))
        {
            StartCoroutine(enemyMovement.FaceWaypoint(transform.position));
            StartAttacking();
            return;
        }

        List<GameObject> newPaths = new List<GameObject>(paths);

        for (int i = paths.Count - 1; i >= 0; i--)
        {
            pathEnemyPairs.TryGetValue(paths[i], out GameObject value);
            if (value == null)
            {
                StartCoroutine(enemyMovement.MoveAlongPath(newPaths));
                pathEnemyPairs[paths[i]] = enemy;
                enemyList.Add(enemy);
                break;

            }
            else
            {
                newPaths.RemoveAt(i);
            }
        }
    }

    void RemoveEnemy(object go)
    {
        if (go is GameObject enemy)
        {
            if (pathEnemyPairs.ContainsValue(enemy))
            {
                var keyOfValue = pathEnemyPairs.FirstOrDefault(x => x.Value == enemy).Key;
                pathEnemyPairs[keyOfValue] = null;
            }
            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
            }
        }
    }

    void StartAttacking()
    {
        Debug.Log("Attacking");
    }
}
