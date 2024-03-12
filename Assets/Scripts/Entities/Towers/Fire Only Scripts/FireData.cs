using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Towers/Fire")]
public class FireData : TowerData
{
    [Header("Rotation Speed")]
    public List<float> RotateSpeedUpgradeValues;
    public List<float> RotateSpeedUpgradeCosts;

    public List<float> flameUpgradeCosts;

    [Header("---------------------------FIRE TRAP ATTRIBUTES-----------------------")]
    public bool canThrowTrap;
    public GameObject fireTrap;
    public float trapRandomPosOffset;
    public float trapDamage;
    public float trapCooldown;
    public LayerMask pathLayer;

    [Header("---------------------------FIRE TRAP UPGRADES-----------------------")]
    public float UpgradedTrapCooldown1;
    public float UpgradedTrapCooldown2;
    public float UpgradedTrapDamage;
    public List<float> FireTrapUpgradeCosts;
}
