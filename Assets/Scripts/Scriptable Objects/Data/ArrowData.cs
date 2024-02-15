using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Projectiles/Arrow")]
public class ArrowData : GameData, IPoolable, IProjectile
{

    [field: Header("----------------------OBJECT POOLING-------------------------")]
    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }
    [field: SerializeReference] public int ObjectPoolsize { get; set; }
    [field: SerializeReference] public List<GameObject> objList { get; set; }


    [field:Header("------------------------ARROW ATTRIBUTES--------------------------")]
    [field: SerializeReference] public float ProjectileSpeed { get; set; }
    [field: SerializeReference] public float ProjectileDamage { get; set; }
    public float projectileLife;


    [field: Header("----------------------ARROW UPGRADES-------------------------")]
    [field: SerializeReference] public List<float> ProjectileDamageUpgradeValues { get; set; }
    [field: SerializeReference] public List<float> ProjectileDamageUpgradeCosts { get; set; }


    [Header("------------------------PIERCE ATTRIBUTES------------------------")]
    public int pierceLimit;
    public bool canPierce;
    public List<float> pierceDamage;


    [Header("------------------------PIERCE UPGRADES-------------------------")]
    public List<float> pierce1DamageUpgrades;
    public List<float> pierce2DamageUpgrades;
    public List<float> pierce3DamageUpgrades;
    public List<float> pierce4DamageUpgrades;
    public List<float> pierceUpgradeCosts;
    public List<int> pierceLimitUpgrades;


    [Header("------------------------POISON POOL ATTRIBUTES------------------------")]
    public GameObject poisonPool;
    public float chanceToDropPool;
    public float poisonPoolDuration;
    public float poisonDurationOnEnemies;
    public float poisonDamage;
    public bool canPoison;
    public LayerMask poolLayer;



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
