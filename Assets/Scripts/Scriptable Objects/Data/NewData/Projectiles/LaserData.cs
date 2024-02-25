using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Projectiles/Laser")]
public class LaserData : ProjectileData, IPoolable
{








    [field: Header("----------------------OBJECT POOLING-------------------------")]
    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }
    [field: SerializeReference] public int ObjectPoolsize { get; set; }
    [field: SerializeReference] public List<GameObject> objList { get; set; }


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
