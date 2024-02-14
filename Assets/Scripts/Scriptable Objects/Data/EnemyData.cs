using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyData : GameData, IPoolable, IEnemy
{
    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }
    [field: SerializeReference] public int ObjectPoolsize { get; set; }
    [field: SerializeReference] public List<GameObject> objList { get; set; }
    [field: SerializeReference] public float DefaultMoveSpeed { get; set; }
    [field: SerializeReference] public float BaseMaxHealth { get; set; }
    [field: SerializeReference] public float MoneyDrop { get; set; }

    public GameObject GetObject()
    {
        foreach (var obj in objList)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;

            }
        }
        return null;
    }
}
