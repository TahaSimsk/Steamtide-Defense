using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Enemies")]
public class EnemyData : GameData, IPoolable
{


    [field: Header("------------------------------ENEMY ATTRIBUTES------------------------------")]
    [field: SerializeReference] public float DefaultMoveSpeed { get; set; }
    [field: SerializeReference] public float BaseMaxHealth { get; set; }
    [field: SerializeReference] public float MoneyDrop { get; set; }

    public float Range;

    public float Damage;

    public float ShootingDelay;

    public float ProjectileSpeed;

    [field: Header("------------------------------OBJECT POOLING------------------------------")]
    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }
    [field: SerializeReference] public int ObjectPoolsize { get; set; }
    public List<GameObject> objList { get; set; }

    public GameObject ProjectilePrefab;
    public int ProjectilePoolSize;
    [HideInInspector] public List<GameObject> ProjectileList;

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

    public GameObject GetEnemyProjectile()
    {
        foreach (var obj in ProjectileList)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;

            }
        }
        return null;
    }
}
