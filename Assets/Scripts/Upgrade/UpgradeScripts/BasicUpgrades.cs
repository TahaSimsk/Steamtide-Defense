using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgrades : MonoBehaviour
{
    [SerializeField] TowerHealth towerHealth;
    [SerializeField] Shooting shooting;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] Canvas canvas;

    GameData originalTowerData;
    GameData originalProjectileData;

    GameData instantiatedTowerData;
    GameData instantiatedProjectileData;

    ITower instantiatedITowerData;
    IProjectile InstantiatedIProjectileData;

    ITower originalITowerData;
    IProjectile originalIProjectileData;

    private void Start()
    {
        //originalTowerData = shooting.TowerData;
        //originalProjectileData = shooting.ProjectileData;

        //instantiatedTowerData = shooting.InstantiatedTowerData;
        //instantiatedProjectileData = shooting.InstantiatedProjectileData;

        //instantiatedITowerData = (ITower)instantiatedTowerData;
        //InstantiatedIProjectileData= (IProjectile)instantiatedProjectileData;

        //originalITowerData = (ITower)originalTowerData;
        //originalIProjectileData = (IProjectile)originalProjectileData;
                        
    }

    public void UpgradeRange(float range)
    {
        instantiatedITowerData.WeaponRange = (originalITowerData.WeaponRange * range * 0.01f) + instantiatedITowerData.WeaponRange;
        Debug.Log(instantiatedITowerData.WeaponRange);
        targetScanner.ChangeRange(instantiatedITowerData.WeaponRange);
    }

    public void UpgradeShootingDelay(float delay)
    {
        instantiatedITowerData.ShootingDelay = instantiatedITowerData.ShootingDelay - (originalITowerData.ShootingDelay * delay * 0.01f);
        Debug.Log(instantiatedITowerData.ShootingDelay);
    }

    public void UpgradeDamage(float damage)
    {
        InstantiatedIProjectileData.ProjectileDamage = (originalIProjectileData.ProjectileDamage * damage * 0.01f) + InstantiatedIProjectileData.ProjectileDamage;
        Debug.Log(InstantiatedIProjectileData.ProjectileDamage);
    }

    public void UpgradeHealth(float health)
    {
        //iTowerData.BaseMaxHealth = health;
        towerHealth.SetMaxHP(health);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        canvas.gameObject.SetActive(true);
    }

}
