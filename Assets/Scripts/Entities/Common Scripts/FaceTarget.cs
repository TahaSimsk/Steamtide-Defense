using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceTarget : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] Transform partToMove;

    private void Update()
    {
        LookAtTarget();
    }


    void LookAtTarget()
    {

        if (targetScanner.targetsInRange.Count == 0) return;

        Vector3 dir = targetScanner.targetsInRange[0].transform.position - partToMove.position;
        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToMove.rotation, lookRotation, Time.deltaTime * towerInfo.InstTowerData.TowerRotationSpeed).eulerAngles;

        partToMove.rotation = Quaternion.Euler(rotation);

    }

}
