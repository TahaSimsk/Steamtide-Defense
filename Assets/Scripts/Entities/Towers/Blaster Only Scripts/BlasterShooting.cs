using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShooting : Shooting
{
    BlasterData blasterData;
    [SerializeField] Transform laserPos2;
    float ammoConsumption;

    protected override void Start()
    {
        base.Start();
        blasterData = (BlasterData)towerData;
    }

    protected override void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count > 0 && timer >= towerData.ShootingDelay)
        {
            HelperFunctions.LookAtTarget(targetScanner.Target(towerData.TargetPriority).position, partToRotate, towerData.TowerRotationSpeed);

            StartCoroutine(BurstShooting(projectilePos));

            if (blasterData.canDoubleBarrel)
            {
                StartCoroutine(BurstShooting(laserPos2));
            }
            timer = 0;

        }
    }


    IEnumerator BurstShooting(Transform projectileSpawnPoint)
    {

        for (int i = 0; i < blasterData.BurstCount; i++)
        {
            if (ammoConsumption >= .99f)
            {
                ammoConsumption -= 1;
                ammoManager.ReduceAmmoAndCheckHasAmmo();
            }

            GetProjectileFromPoolAndActivate(projectileSpawnPoint);
            ammoConsumption += blasterData.AmmoEfficiency;
            yield return new WaitForSeconds(blasterData.TimeBetweenBursts);
        }



    }
}
