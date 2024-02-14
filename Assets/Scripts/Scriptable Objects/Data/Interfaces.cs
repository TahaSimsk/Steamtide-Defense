using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    GameObject ObjectPrefab { get; set; }
    int ObjectPoolsize { get; set; }
    List<GameObject> objList { get; set; }

    GameObject GetObject();
}

public interface ITower
{
    GameObject TowerPrefab { get; set; }
    GameObject TowerHoverPrefab { get; set; }
    GameObject TowerNPHoverPrefab { get; set; }

    float TowerPlacementCost { get; set; }
    float ShootingDelay { get; set; }
    List<float> ShootingDelayUpgradeValues { get; set; }
    List<float> ShootingDelayUpgradeCosts { get; set; }
    float WeaponRotationSpeed { get; set; }
    float WeaponRange { get; set; }
    List<float> WeaponRangeUpgradeValues { get; set; }
    List<float> WeaponRangeUpgradeCosts { get; set; }
    float BaseMaxHealth { get; set; }
    List<float> MaxHealthUpgradeValues { get; set; }
    List<float> MaxHealthUpgradeCosts { get; set; }
}

public interface IProjectile
{
    float ProjectileSpeed { get; set; }
    float ProjectileDamage { get; set; }

    List<float> ProjectileDamageUpgradeValues { get; set; }
    List<float> ProjectileDamageUpgradeCosts { get; set; }

}

public interface IEnemy
{
    float DefaultMoveSpeed { get; set; }
    float BaseMaxHealth { get; set; }
    float MoneyDrop { get; set; }
}

public interface IUpgradeable
{
    void Upgrade();
}
