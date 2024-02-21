using System.Collections.Generic;
using UnityEngine;

public class TowerData : GameData
{
    [field: Header("---------------------------TOWER PREFABS-----------------------")]
    public GameObject TowerPrefab;
    public GameObject TowerHoverPrefab;
    public GameObject TowerNPHoverPrefab;


    [field: Header("---------------------------TOWER ATTRIBUTES-----------------------")]
    public float ShootingDelay;
    public float TowerRange;
    public float BaseMaxHealth;
    public float TowerRotationSpeed;
    public float TowerPlacementCost;


    [field: Header("---------------------------TOWER UPGRADES-----------------------")]
    public List<float> ShootingDelayUpgradeValues;
    public List<float> ShootingDelayUpgradeCosts;
    public List<float> RangeUpgradeValues;
    public List<float> RangeUpgradeCosts;
    public List<float> MaxHealthUpgradeValues;
    public List<float> MaxHealthUpgradeCosts;
}


