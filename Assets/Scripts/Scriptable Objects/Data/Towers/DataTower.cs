using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTower", menuName = "DataTower")]
public class DataTower : Data
{
    
    [Header("Weapon Prefabs")]
    public GameObject towerHoverPrefab;
    public GameObject towerNPHoverPrefab;

    [Header("Weapon Attributes")]
    public float shootingDelay;
    public float weaponRotationSpeed;
    public float weaponRange;

}
