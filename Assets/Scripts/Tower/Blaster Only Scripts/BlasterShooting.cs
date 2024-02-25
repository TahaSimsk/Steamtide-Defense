using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShooting : Shooting
{
    BlasterData blasterData;

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

            StartCoroutine(BurstShooting());
            timer = 0;

        }
    }


    IEnumerator BurstShooting()
    {

        for (int i = 0; i < blasterData.BurstCount; i++)
        {
            if (ammoConsumption >= .99f)
            {
                ammoConsumption = 0;
                ammoManager.ReduceAmmoAndCheckHasAmmo();
            }

            GetProjectileFromPoolAndActivate();
            ammoConsumption += blasterData.AmmoEfficiency;
            yield return new WaitForSeconds(blasterData.TimeBetweenBursts);
        }



    }
}
