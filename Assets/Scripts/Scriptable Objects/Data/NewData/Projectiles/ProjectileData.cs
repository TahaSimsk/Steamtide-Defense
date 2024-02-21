using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : GameData
{
    [Header("------------------------BASE PROJECTILE ATTRIBUTES--------------------------")]
    public float ProjectileSpeed;
    public float ProjectileDamage;

    [Header("------------------------BASE PROJECTILE UPGRADES--------------------------")]
    public List<float> ProjectileDamageUpgradeValues;
    public List<float> ProjectileDamageUpgradeCosts;
}


