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
    [SerializeField] Transform basePathParent;

    List<GameObject> basePath = new List<GameObject>();
    Dictionary<GameObject, GameObject> pathEnemyPairs = new Dictionary<GameObject, GameObject>();
    [HideInInspector] public List<GameObject> EnemiesInBase = new List<GameObject>();
    private void Awake()
    {
        //for (int i = 0; i < paths.Count; i++)
        //{
        //    pathEnemyPairs.Add(paths[i], null);
        //}

        GetBasePathsFromParent();
    }

    void GetBasePathsFromParent()
    {
        foreach (Transform child in basePathParent)
        {
            pathEnemyPairs.Add(child.gameObject, null);
            basePath.Add(child.gameObject);
        }
    }

    private void OnEnable()
    {
        onEnemyReachEndOfPath.onEventRaised += PositionAndMoveEnemy;
        onEnemyDeath.onEventRaised += RemoveEnemy;
        onEnemyReachEndOfBasePath.onEventRaised += HandleEnemyReachEndOfBasePath;
    }
    private void OnDisable()
    {
        onEnemyReachEndOfPath.onEventRaised -= PositionAndMoveEnemy;
        onEnemyDeath.onEventRaised -= RemoveEnemy;
        onEnemyReachEndOfBasePath.onEventRaised -= HandleEnemyReachEndOfBasePath;
    }

    void PositionAndMoveEnemy(object go)
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



        List<GameObject> newPath = new List<GameObject>(basePath);

        for (int i = basePath.Count - 1; i >= 0; i--)
        {
            pathEnemyPairs.TryGetValue(basePath[i], out GameObject value);
            if (value == null)
            {
                StartCoroutine(enemyMovement.MoveAlongPath(newPath, true));
                pathEnemyPairs[basePath[i]] = enemy;
                if (enemy.activeInHierarchy)
                {
                    EnemiesInBase.Add(enemy);
                }
                break;

            }
            else
            {
                newPath.RemoveAt(i);
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
            return;
        }
    }

    void RemoveEnemy(object go)
    {
        if (go is GameObject enemy)
        {
            if (EnemiesInBase.Contains(enemy))
            {
                EnemiesInBase.Remove(enemy);
            }
            if (pathEnemyPairs.ContainsValue(enemy))
            {
                var keyOfValue = pathEnemyPairs.FirstOrDefault(x => x.Value == enemy).Key;
                pathEnemyPairs[keyOfValue] = null;
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
