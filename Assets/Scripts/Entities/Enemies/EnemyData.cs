using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Enemies")]
public class EnemyData : GameData
{


    [field: Header("------------------------------ENEMY ATTRIBUTES------------------------------")]
    public Immunity Immunity;
    public EnemyAttackType EnemyType;
    [field: SerializeReference] public float DefaultMoveSpeed { get; set; }
    [field: SerializeReference] public float BaseMaxHealth { get; set; }
    [field: SerializeReference] public float MoneyDrop { get; set; }

    public float Range;

    public float Damage;

    public float ShootingDelay;

    public float ProjectileSpeed;

    public float AimSpeed;

    [field: SerializeReference] public GameObject ObjectPrefab { get; set; }

    public GameObject ProjectilePrefab;


}
