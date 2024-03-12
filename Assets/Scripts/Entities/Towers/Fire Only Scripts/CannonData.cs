using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName = "GameData/Towers/Cannon")]
public class CannonData : TowerData, IPoolable
{
    [Header("------------------------EXPLOSION ATTRIBUTES--------------------------")]
    public float ExplosionRadius;
    public LayerMask enemyLayer;

    [Header("------------------------EXPLOSION UPGRADES--------------------------")]
    public List<float> ExplosionRadiusUpgradeValues;
    public List<float> ExplosionRadiusUpgradeCosts;




    [Header("------------------------MORTAR ATTRIBUTES--------------------------")]


    public float MortarCooldown;
    public GameObject MortarTargetIndicator;
    public int numOfMissilesToLaunch;
    public float timeBetweenMissiles;

    public GameObject missilePrefab;
    public float initialTravelTimeOfMissile;
    public float missileMoveSpeed;
    public float timeToWaitBeforeAppearing;
    public float offsetToAppear;
    public float bombRadius;
    public float damage;

    [Header("------------------------MORTAR UPGRADES--------------------------")]
    [Header("Level 1 Upgrade")]
    public bool canMortar;
    [Header("Level 2 Upgrade")]
    public float upgradedBombRadius;
    [Header("Level 3 Upgrade")]
    public int upgradedNumOfMissiles;
    [Header("Level 4 Upgrade")]
    public float mortarUpgradedCooldown;
    [Header("Upgrade Costs")]
    public List<float> mortarUpgradeCosts;


    [field: Header("------------------------OBJECT POOLING--------------------------")]
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


