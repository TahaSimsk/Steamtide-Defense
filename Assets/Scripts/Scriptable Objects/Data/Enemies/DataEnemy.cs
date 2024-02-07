using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEnemy", menuName = "Data/DataEnemy")]
public class DataEnemies : Data
{

    [Header("Enemy Attributes")]
    public float defaultMoveSpeed;
    public float baseMaxHealth;

    //[HideInInspector]
    //new private float cost1=5;

}
