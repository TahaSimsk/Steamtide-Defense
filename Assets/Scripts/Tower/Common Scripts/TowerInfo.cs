using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    public GameData DefTowerGameData;
    public GameData DefProjectileGameData;

    public TowerData DefITower { get; set; }
    public ProjectileData DefIProjectile { get; set; }

    public GameData InstTowerGameData { get; set; }
    public GameData InstProjectileGameData { get; set; }

    public TowerData InstITower { get; set; }
    public ProjectileData InstIProjectile { get; set; }

    public ProjectileHitBehaviours hitBehaviour { get; set; }

    private void Awake()
    {
        InstTowerGameData = Instantiate(DefTowerGameData);
        InstProjectileGameData = Instantiate(DefProjectileGameData);

        InstITower = (TowerData)InstTowerGameData;
        InstIProjectile = (ProjectileData)InstProjectileGameData;

        DefITower = (TowerData)DefTowerGameData;
        DefIProjectile = (ProjectileData)DefProjectileGameData;
    }
}
