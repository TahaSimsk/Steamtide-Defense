using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class EnemyEnterBaseSequence : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onEnemyReachEndOfPath;
    [SerializeField] GameEvent1ParamSO onEnemyReachEndOfBasePath;
    [SerializeField] GameEvent1ParamSO onEnemyDeath;
    [SerializeField] List<GameObject> paths = new List<GameObject>();

    public Dictionary<GameObject, GameObject> pathEnemyPairs = new Dictionary<GameObject, GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
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
        onEnemyReachEndOfBasePath.onEventRaised += HandleEnemyReachEndOfBasePath;
    }
    private void OnDisable()
    {
        onEnemyReachEndOfPath.onEventRaised -= PositionEnemy;
        onEnemyDeath.onEventRaised -= RemoveEnemy;
        onEnemyReachEndOfBasePath.onEventRaised -= HandleEnemyReachEndOfBasePath;
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



        List<GameObject> newPaths = new List<GameObject>(paths);

        for (int i = paths.Count - 1; i >= 0; i--)
        {
            pathEnemyPairs.TryGetValue(paths[i], out GameObject value);
            if (value == null)
            {
                StartCoroutine(enemyMovement.MoveAlongPath(newPaths, true));
                pathEnemyPairs[paths[i]] = enemy;
                if (enemy.activeInHierarchy)
                {
                    enemyList.Add(enemy);
                }
                Debug.Log("added");
                break;

            }
            else
            {
                newPaths.RemoveAt(i);
            }
        }
    }

    void HandleEnemyReachEndOfBasePath(object go)
    {
        if (go is GameObject enemy)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            StartCoroutine(enemyMovement.FaceWaypoint(transform.position));
            StartAttacking(enemy);
            Debug.Log("enemy reach end");
            return;
        }


    }

    void RemoveEnemy(object go)
    {
        if (go is GameObject enemy)
        {
            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
                Debug.Log("killed " + enemy.ToString());
            }
            if (pathEnemyPairs.ContainsValue(enemy))
            {
                var keyOfValue = pathEnemyPairs.FirstOrDefault(x => x.Value == enemy).Key;
                pathEnemyPairs[keyOfValue] = null;
                Debug.Log("removed");
            }

        }
    }

    void StartAttacking(GameObject enemy)
    {
        MeleeAttacking enemyMeleeAttack = enemy.GetComponent<MeleeAttacking>();
        if (enemyMeleeAttack != null)
        {
            StartCoroutine(enemyMeleeAttack.Attack(gameObject));
        }
    }
}
