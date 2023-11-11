using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float timeBetweenEnemyWaves;

    [SerializeField] List<Waves> waves = new List<Waves>();

    [SerializeField] bool setEnemyHealthManually;
    [SerializeField] bool enemyHealthDifficulty;
    [SerializeField][Range(1, 3)] float enemyHealthDifficultyMultiplier;

    ObjectPool objectPool;

    int currentWave;

    float difficulty = 1f;
    float enemyHealth;


    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var wave in waves)
            {
                currentWave++;

                for (int i = 0; i < wave.enemyCount.Count; i++)
                {
                    int enemyCount = wave.enemyCount[i];
                    int enemyId = wave.enemies[i].GetComponent<EnemyHealth>().enemyId;

                    if (setEnemyHealthManually)
                    {
                        enemyHealth = wave.enemyHealth[i];
                    }

                    for (int x = 0; x < enemyCount; x++)
                    {
                        GameObject pooledEnemy = objectPool.GetEnemy(enemyId);

                        if (setEnemyHealthManually && enemyHealth != Mathf.Epsilon)
                        {
                            pooledEnemy.GetComponent<EnemyHealth>().maxHealth = enemyHealth;
                        }
                        else if (enemyHealthDifficulty)
                        {
                            pooledEnemy.GetComponent<EnemyHealth>().SetMaxHP(difficulty);
                        }

                        pooledEnemy.SetActive(true);


                        yield return new WaitForSeconds(timeBetweenEnemySpawns);
                    }

                }

                //next wave
                yield return new WaitForSeconds(timeBetweenEnemyWaves);

                if (enemyHealthDifficulty)
                {
                    difficulty *= enemyHealthDifficultyMultiplier;
                }
            }
            break;
        }
    }
}
