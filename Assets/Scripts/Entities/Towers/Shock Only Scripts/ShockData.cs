using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Towers/Shock")]
public class ShockData : TowerData
{
    [Header("---------------------------SHOCK ATTRIBUTES-----------------------")]
    public GameObject Projectile;
    public int projectileCount;
    public float slowAmount;
    public float slowDuration;

    [Header("---------------------------SHOCK UPGRADES-----------------------")]
    [Header("Level 1 Upgrades")]
    public int upgradedProjectileCount1;
    [Header("Level 2 Upgrades")]
    public float upgradedSlowAmount;
    [Header("Level 3 Upgrades")]
    public float upgradedSlowDuration;
    [Header("Level 4 Upgrades")]
    public int upgradedProjectileCount2;
    [Header("Shock Upgrade Costs")]
    public List<float> shockUpgradeMoneyCosts;
    public List<float> shockUpgradeWoodCosts;
    public List<float> shockUpgradeRockCosts;

    [Header("---------------------------EMP ATTRIBUTES-----------------------")]
    public float empRadius;
    public float empCooldown;
    public float freezeDuration;
    public LayerMask enemyLayer;
    public bool canFreezeBosses;


    [Header("---------------------------EMP UPGRADES-----------------------")]
    public float upgradedFreezeDuration;
    public float upgradedEmpCooldown;
    [Header("Shock Upgrade Costs")]
    public List<float> empUpgradeMoneyCosts;
    public List<float> empUpgradeWoodCosts;
    public List<float> empUpgradeRockCosts;
}
