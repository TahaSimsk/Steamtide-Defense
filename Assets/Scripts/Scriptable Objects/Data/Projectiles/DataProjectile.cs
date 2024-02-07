using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProjectile", menuName = "Data/DataProjectile")]
public class DataProjectile : Data
{

    

    [Header("Projectile Attributes")]
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLife;
}
