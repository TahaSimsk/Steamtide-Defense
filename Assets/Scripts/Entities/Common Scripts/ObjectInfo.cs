using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public GameData DefObjectGameData;

    public TowerData DefTowerData { get; set; }

    public GameData InstTowerGameData { get; set; }

    public TowerData InstTowerData { get; set; }

    private void Awake()
    {
        if (DefObjectGameData is TowerData tower)
        {
            InstTowerGameData = Instantiate(DefObjectGameData);
            InstTowerData = (TowerData)InstTowerGameData;
            DefTowerData = tower;
        }
        
    }
}
