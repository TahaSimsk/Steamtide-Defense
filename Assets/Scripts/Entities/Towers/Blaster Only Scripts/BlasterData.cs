using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Towers/Blaster")]
public class BlasterData : TowerData, IPoolable
{

    [Header("---------------------------BURST ATTRIBUTES-----------------------")]
    public float TimeBetweenBursts;
    public int BurstCount;
    public float AmmoEfficiency;

    [Header("---------------------------BURST UPGRADES-----------------------")]

    public List<int> BurstUpgradedValues;
    public bool canDoubleBarrel;
    public List<float> BurstUpgradeMoneyCosts;
    public List<float> BurstUpgradeWoodCosts;
    public List<float> BurstUpgradeRockCosts;

    [Header("---------------------------AMMO UPGRADES-----------------------")]

    public List<float> AmmoEfficiencyUpgradedValues;
    public int UpgradedAmmoCapacity;
    public List<float> AmmoUpgradeMoneyCosts;
    public List<float> AmmoUpgradeWoodCosts;
    public List<float> AmmoUpgradeRockCosts;

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
