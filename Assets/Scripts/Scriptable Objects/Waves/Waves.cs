using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave 1", menuName = "Waves")]
public class Waves : ScriptableObject
{
    public List<GameObject> enemyTypes;

    public List<int> enemyCount;

    public List<Vector2> timeBetweenEnemySpawns;
}
