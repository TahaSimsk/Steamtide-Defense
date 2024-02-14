using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProjectile", menuName = "Data/DataProjectile/Arrow")]
public class DataArrow : DataProjectile
{
    [Header("Arrow Specific Attributes")]
    public int pierceLimit;
    public float pierceDamage;
    public bool canPierce;
    public float projectileLife;

    public GameObject poisonPool;
    public float chanceToDropPool;
    public float poisonPoolDuration;
}
