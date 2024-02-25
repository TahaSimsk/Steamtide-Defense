using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName = "GameData/Towers/Cannon")]
public class CannonData : TowerData
{
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
    public LayerMask enemyLayer;
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

}


