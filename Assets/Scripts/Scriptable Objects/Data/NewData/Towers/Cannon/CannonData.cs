using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName ="GameData/Towers/Cannon")]
public class CannonData : TowerData
{
    [Header("------------------------MORTAR ATTRIBUTES--------------------------")]
    public bool canMortar;
    public float mortarUpgradeCost;

    public float MortarRadius;
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



}


