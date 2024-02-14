using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    public GameData DefTowerGameData;
    public GameData DefProjectileGameData;

    public ITower DefITower { get; set; }
    public IProjectile DefIProjectile { get; set; }

    public GameData InstTowerGameData { get; set; }
    public GameData InstProjectileGameData { get; set; }

    public ITower InstITower { get; set; }
    public IProjectile InstIProjectile { get; set; }


    private void Awake()
    {
        InstTowerGameData = Instantiate(DefTowerGameData);
        InstProjectileGameData = Instantiate(DefProjectileGameData);

        InstITower = (ITower)InstTowerGameData;
        InstIProjectile = (IProjectile)InstProjectileGameData;

        DefITower = (ITower)DefTowerGameData;
        DefIProjectile = (IProjectile)DefProjectileGameData;
    }
}
