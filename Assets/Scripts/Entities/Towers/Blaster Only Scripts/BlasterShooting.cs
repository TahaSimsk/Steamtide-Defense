using System.Collections;
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
        if (targetScanner.targetsInRange.Count == 0) return;

        if (targetScanner.targetsInRange.Contains(targetingSystem.CurrentTarget))
        {
            HelperFunctions.LookAtTarget(targetingSystem.CurrentTarget.transform.position, partToRotate, towerData.TowerRotationSpeed);
        }

        if (timer < towerData.ShootingDelay) return;
        targetingSystem.GetTarget(targetScanner.targetsInRange);
        StartCoroutine(BurstShooting(projectilePos));

        if (blasterData.canDoubleBarrel)
        {
            StartCoroutine(BurstShooting(laserPos2));
        }
        timer = 0;


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
