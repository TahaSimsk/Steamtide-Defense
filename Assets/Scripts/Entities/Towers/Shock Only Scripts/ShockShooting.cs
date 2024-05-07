using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockShooting : Shooting
{
    List<LineRenderer> shockLines = new List<LineRenderer>();
    ShockData shockData;
    protected override void Start()
    {
        base.Start();
        shockData = (ShockData)towerData;

        for (int i = 0; i < 3; i++)
        {
            LineRenderer lr = Instantiate(shockData.Projectile).GetComponent<LineRenderer>();
            shockLines.Add(lr);
        }
    }

    private void OnDisable()
    {
        foreach (var item in shockLines)
        {
            item.gameObject.SetActive(false);
        }
    }

    protected override void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count == 0) return;

        HelperFunctions.LookAtTarget(targetScanner.Target(towerData.TargetPriority).position, partToRotate, towerData.TowerRotationSpeed);

        if (timer < towerData.ShootingDelay) return;

        for (int i = 0; i < shockData.projectileCount; i++)
        {
            if (targetScanner.targetsInRange.Count < i + 1) break;
            GameObject target = targetScanner.targetsInRange[i];
            LineRenderer currentLR = shockLines[i];
            currentLR.enabled = true;
            currentLR.SetPosition(0, projectilePos.position);
            currentLR.SetPosition(1, target.transform.position);
            StartCoroutine(DisableLR(currentLR));
            target.GetComponent<EnemyHealth>().ReduceHealth(shockData.ProjectileDamage);
            target.GetComponent<EnemyMovement>().DecreaseMoveSpeedByPercentage(shockData.slowAmount, shockData.slowDuration);

        }
        timer = 0;


    }

    IEnumerator DisableLR(LineRenderer lr)
    {
        yield return new WaitForSeconds(0.5f);
        lr.enabled = false;
    }
}
