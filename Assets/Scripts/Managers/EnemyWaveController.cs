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
                for (int i = 0; i < wave.enemyTypes.Count; i++)
                {
                    //store the amount of enemies in the selected type
                    int enemyCount = wave.enemyCount[i];

                    //store the enemy data for purposes of spawning the enemy
                    EnemyData enemyData = wave.enemyTypes[i].GetComponent<ObjectInfo>().DefObjectGameData as EnemyData;
                    //store the min and max value to randomize enemy spawn countdown
                    Vector2 _timeBetweenEnemySpawns = wave.timeBetweenEnemySpawns[i];

                    //spawn enemies by their count which is set in the "Wave SO"
                    for (int x = 0; x < enemyCount; x++)
                    {
                        GameObject pooledEnemy = null;

                        //store the next enemy to be spawned
                        pooledEnemy = ObjectPool.Instance.GetObject(enemyData.ObjectPrefab.GetHashCode(), enemyData.ObjectPrefab);
                        //add it to the list for purposes of checking if the enemy is alive 
                        enemiesInWave.Add(pooledEnemy);

                        //spawn the enemy
                        pooledEnemy.SetActive(true);

                        //get a random spawn timer
                        float randomizedSpawnTimer = Random.Range(_timeBetweenEnemySpawns.x, _timeBetweenEnemySpawns.y);

                        //wait till the countdown to finish to spawn another enemy
                        yield return new WaitForSeconds(randomizedSpawnTimer);
                    }
                    //---------------END OF THE ENEMY TYPE--------------------


                }
                //---------------END OF THE WAVE--------------------

                //wait for enemies to die before starting countdown to go to the next wave
                foreach (var enemy in enemiesInWave)
                {
                    yield return new WaitUntil(() => enemy.activeInHierarchy == false);
                }
                enemiesInWave.Clear();

                //raise a wave end event and pass the timer
                onWaveEnd.RaiseEvent(timeBetweenEnemyWaves);

                //start the countdown for the next wave
                yield return new WaitForSeconds(timeBetweenEnemyWaves);
            }
            //---------------END OF TOTAL WAVES--------------------
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

    void SkipTheType(int num)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            num++;
        }
    }

    void SkipTheTimer(float time)
    {
        time = 0;
    }


}
