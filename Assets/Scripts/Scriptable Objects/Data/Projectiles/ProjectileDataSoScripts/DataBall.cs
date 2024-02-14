using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProjectile", menuName = "Data/DataProjectile/Ball")]
public class DataBall : DataProjectile
{
    [Header("Ball Specific Attributes")]
    public float bombRadius;
    public LayerMask enemyLayer;
}
