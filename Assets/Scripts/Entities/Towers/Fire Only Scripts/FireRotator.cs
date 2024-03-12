using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRotator : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] Transform partToRotate;
    FireData fireData;
    float rotateSpeed;
    void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
    }

    void Update()
    {
        RotateTower();
    }

    void RotateTower()
    {
        rotateSpeed += Time.deltaTime * fireData.TowerRotationSpeed;
        partToRotate.rotation = Quaternion.Euler(0, rotateSpeed, 0);
    }
}
