using System.Collections.Generic;
using UnityEngine;

public class TowerData : GameData
{
    [Header("---------------------------TOWER PREFABS-----------------------")]
    public GameObject TowerPrefab;
    public GameObject TowerHoverPrefab;
    public GameObject TowerNPHoverPrefab;


    [Header("---------------------------TOWER ATTRIBUTES-----------------------")]
    public float ShootingDelay;
    public float TowerRange;
    public float BaseMaxHealth;
    public float TowerRotationSpeed;
    public float TowerPlacementCost;
    public int TowerAmmoCapacity;
    public float ProjectileSpeed;
    public float ProjectileDamage;

    [Header("---------------------------TOWER UPGRADES-----------------------")]
    [Header("Shooting Delay")]
    public List<float> ShootingDelayUpgradeValues;
    public List<float> ShootingDelayUpgradeCosts;

    [Header("Range")]
    public List<float> RangeUpgradeValues;
    public List<float> RangeUpgradeCosts;

    [Header("HP")]
    public List<float> MaxHealthUpgradeValues;
    public List<float> MaxHealthUpgradeCosts;

    [Header("Damage")]
    public List<float> ProjectileDamageUpgradeValues;
    public List<float> ProjectileDamageUpgradeCosts;

  
   
}


