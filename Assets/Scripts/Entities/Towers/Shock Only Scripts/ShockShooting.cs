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
            lr.gameObject.SetActive(true);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        foreach (var item in shockLines)
        {
            if (item == null) continue;
            item.gameObject.SetActive(false);
        }
    }

    protected override void Shoot()
    {
        timer += Time.deltaTime;
        if (targetScanner.targetsInRange.Count == 0) return;


        if (timer < towerData.ShootingDelay) return;
        targetingSystem.GetTarget(targetScanner.targetsInRange);
        for (int i = 0; i < shockData.projectileCount; i++)
        {
            if (targetScanner.targetsInRange.Count < i + 1) break;

            GameObject target = targetingSystem.SortedTargetsByPoint[i];
            LineRenderer currentLR = shockLines[i];
            currentLR.gameObject.SetActive(true);
            currentLR.SetPosition(0, projectilePos.position);
            currentLR.SetPosition(1, target.transform.position);

            StartCoroutine(DisableLR(currentLR));

            if (target.GetComponent<EnemyHealth>().ReduceHealth(shockData.ProjectileDamage))
            {
                xpManager.GainXp();
            }

            target.GetComponent<EnemyMovement>().DecreaseMoveSpeedByPercentage(shockData.slowAmount, shockData.slowDuration);

        }
        timer = 0;
    }


    IEnumerator DisableLR(LineRenderer lr)
    {
        yield return new WaitForSeconds(shockData.ShootingDelay - 0.1f);
        lr.gameObject.SetActive(false);
    }
}
