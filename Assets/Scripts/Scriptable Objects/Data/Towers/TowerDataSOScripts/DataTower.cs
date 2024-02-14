using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTower", menuName = "Data/DataTower")]
public class DataTower : Data
{
    [Header("Equipped Projectile")]
    public DataProjectile equippedProjectile;

    [Header("Weapon Prefabs")]
    public GameObject towerHoverPrefab;
    public GameObject towerNPHoverPrefab;

    [Header("Weapon Attributes")]
    public float shootingDelay;
    public float weaponRotationSpeed;
    public float weaponRange;
    public float baseMaxHealth;

    [Header("Upgrade Attributes")]
    public float upgradedShootingDelay;
    public float upgradedWeaponRotationSpeed;
    public float upgradedWeaponRange;
    public float upgradeCost;


}
