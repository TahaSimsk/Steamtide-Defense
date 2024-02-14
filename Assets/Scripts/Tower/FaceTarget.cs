using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceTarget : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] TargetScanner targetScanner;

    private void Update()
    {
        LookAtTarget();
    }


    void LookAtTarget()
    {

        if (targetScanner.targetsInRange.Count == 0) return;

        Vector3 dir = targetScanner.targetsInRange[0].transform.position - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * towerInfo.InstITower.WeaponRotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(rotation);

    }

}
