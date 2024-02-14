using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Test")]
public class ArrowData : GameData, IPoolable, IProjectile
{
    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }
    [field: SerializeReference] public int ObjectPoolsize { get; set; }
    [field: SerializeReference] public List<GameObject> objList { get; set; }
    [field: SerializeReference] public float ProjectileSpeed { get; set; }
    [field: SerializeReference] public float ProjectileDamage { get; set; }
    [field: SerializeReference] public List<float> ProjectileDamageUpgradeValues { get; set; }
    [field: SerializeReference] public List<float> ProjectileDamageUpgradeCosts { get; set; }

    public int pierceLimit;
    public bool canPierce;
    public List<float> pierceDamage;

    public List<float> pierce1DamageUpgrades;
    public List<float> pierce2DamageUpgrades;
    public List<float> pierce3DamageUpgrades;
    public List<float> pierce4DamageUpgrades;
    public List<float> pierceUpgradeCosts;
    public List<int> pierceLimitUpgrades;

    public float projectileLife;

    public GameObject poisonPool;
    public float chanceToDropPool;
    public float poisonPoolDuration;

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
