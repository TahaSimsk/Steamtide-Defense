using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameData/Towers/Blaster")]
public class BlasterData : TowerData
{

    [Header("---------------------------BURST ATTRIBUTES-----------------------")]
    public float TimeBetweenBursts;
    public int BurstCount;
    [Header("---------------------------BURST UPGRADES-----------------------")]
    [Header("Level 1 Upgrade")]
    public int UpgradedBurstCount_1;
    [Header("Level 2 Upgrade")]
    public int UpgradedBurstCount_2;
    [Header("Level 3 Upgrade")]
    public int UpradedAmmoCapacity;

    public float AmmoEfficiency;
}
