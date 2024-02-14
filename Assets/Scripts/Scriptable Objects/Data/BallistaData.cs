using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ballista")]
public class BallistaData : GameData, ITower
{
    //-----------------------------------------TOWER PREFABS-----------------------------------
    [field: SerializeReference] public GameObject TowerPrefab { get; set; }
    [field: SerializeReference] public GameObject TowerHoverPrefab { get; set; }
    [field: SerializeReference] public GameObject TowerNPHoverPrefab { get; set; }
    //-------------------------------------------------------------------------------------------


    //-------------------TOWER ATTRIBUTES & UPGRADE VALUES----------------------
    [field: SerializeReference] public float ShootingDelay { get; set; }
    [field: SerializeReference] public List<float> ShootingDelayUpgradeValues { get; set; }
    [field: SerializeReference] public List<float> ShootingDelayUpgradeCosts { get; set; }

    [field: SerializeReference] public float WeaponRange { get; set; }
    [field: SerializeReference] public List<float> WeaponRangeUpgradeValues { get; set; }
    [field: SerializeReference] public List<float> WeaponRangeUpgradeCosts { get; set; }

    [field: SerializeReference] public float BaseMaxHealth { get; set; }
    [field: SerializeReference] public List<float> MaxHealthUpgradeValues { get; set; }
    [field: SerializeReference] public List<float> MaxHealthUpgradeCosts { get; set; }

    [field: SerializeReference] public float WeaponRotationSpeed { get; set; }
    //-------------------------------------------------------------------------------------------

    [field: SerializeReference] public float TowerPlacementCost { get; set; }



}
