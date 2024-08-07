using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Towers/Ballista")]
public class BallistaData : TowerData
{

    [Header("------------------------PIERCE ATTRIBUTES------------------------")]
    public int pierceLimit;
    public bool canPierce;
    public List<float> pierceDamage;
    public float projectileLife;


    [Header("------------------------PIERCE UPGRADES-------------------------")]
    public List<float> pierce1DamageUpgrades;
    public List<float> pierce2DamageUpgrades;
    public List<float> pierce3DamageUpgrades;
    public List<float> pierce4DamageUpgrades;
    public List<float> pierceUpgradeMoneyCosts;
    public List<float> pierceUpgradeWoodCosts;
    public List<float> pierceUpgradeRockCosts;
    public List<int> pierceLimitUpgrades;


    [Header("------------------------POISON POOL ATTRIBUTES------------------------")]
    public GameObject poisonPool;
    public float poolDropChance;
    public float poolDuration;
    public float poolDamage;
    public float poisonDurationOnEnemies;
    public LayerMask poolLayer;


    [Header("------------------------POISON UPGRADES-------------------------")]
    [Header("Level 1 Upgrades")]
    public bool canPoison;
    [Header("Level 2 Upgrades")]
    public float poolDropChanceUpgradedValue;
    [Header("Level 3 Upgrades")]
    public float poolDurationUpgradedValue;
    [Header("Level 4 Upgrades")]
    public bool dropPoolOnFirstEnemy;
    public float poolDamageUpgradedValue;
    [Header("UpgradeCosts")]
    public List<float> poolUpgradeMoneyCosts;
    public List<float> poolUpgradeWoodCosts;
    public List<float> poolUpgradeRockCosts;

}
