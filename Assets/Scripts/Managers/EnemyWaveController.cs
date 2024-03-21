using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onWaveStart;
    [SerializeField] GameEvent1ParamSO onWaveEnd;

    [Header("Required Components")]
    [SerializeField] TextMeshProUGUI waveText;

    [Header("Attributes")]
    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float timeBetweenEnemyWaves;

    [Header("Waves")]
    [SerializeField] List<Waves> waves = new List<Waves>();

    List<GameObject> enemiesInWave = new List<GameObject>();

    public int numOfTotalEnemies;


    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            //start the wave
            foreach (var wave in waves)
            {

                GetTotalEnemiesInCurrentWave(wave.enemyCount);
                onWaveStart.RaiseEvent(numOfTotalEnemies);
                numOfTotalEnemies = 0;
                //loop through enemy types in the wave
                for (int i = 0; i < wave.enemyCount.Count; i++)
                {
                    int enemyCount = wave.enemyCount[i];
                    IPoolable enemyData = wave.enemies[i].GetComponent<ObjectInfo>().DefObjectGameData as IPoolable;


                    //spawn enemies by their count which is set in the "Wave SO"
                    for (int x = 0; x < enemyCount; x++)
                    {
                        //GameObject pooledEnemy = objectPool.GetObjectFromPool(hashCode);
                        GameObject pooledEnemy = null;

                        pooledEnemy = enemyData.GetObject();

                        enemiesInWave.Add(pooledEnemy);

                        pooledEnemy.SetActive(true);


                        yield return new WaitForSeconds(timeBetweenEnemySpawns);
                    }

                }
                //end of the wave

                //wait for enemies to die before starting countdown to go to the next wave
                foreach (var enemy in enemiesInWave)
                {
                    yield return new WaitUntil(() => enemy.activeInHierarchy == false);
                }
                enemiesInWave.Clear();

                onWaveEnd.RaiseEvent(timeBetweenEnemyWaves);


                yield return new WaitForSeconds(timeBetweenEnemyWaves);
            }
            //end of total waves
            break;
        }
    }



    void GetTotalEnemiesInCurrentWave(List<int> enemyCount)
    {
        foreach (var item in enemyCount)
        {
            numOfTotalEnemies += item;
        }
    }



}
