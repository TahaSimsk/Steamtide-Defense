using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    public GameData DefTowerGameData;

    public TowerData DefTowerData { get; set; }

    public GameData InstTowerGameData { get; set; }

    public TowerData InstTowerData { get; set; }

    private void Awake()
    {
        InstTowerGameData = Instantiate(DefTowerGameData);
        InstTowerData = (TowerData)InstTowerGameData;
        DefTowerData = (TowerData)DefTowerGameData;

    }
}
