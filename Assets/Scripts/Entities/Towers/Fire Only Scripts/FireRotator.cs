using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRotator : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] Transform partToRotate;
    [SerializeField] ShootingDelayUpgrade shootingDelayUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;

    FireData fireData;
    float rotateSpeed;
    float combinedShootingDelayPercentages;
    void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        HandleShootingDelayUpgrade(GlobalPercentageManager.Instance.GlobalShootingDelayPercentage);

    }

    private void OnEnable()
    {
        shootingDelayUpgrade.OnShootingDelayUpgraded += HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised += HandleShootingDelayUpgrade;
    }
    private void OnDisable()
    {
        shootingDelayUpgrade.OnShootingDelayUpgraded -= HandleShootingDelayUpgrade;
        onGlobalShootingDelayUpgrade.onEventRaised -= HandleShootingDelayUpgrade;
    }

    void Update()
    {
        RotateTower();
    }

    void RotateTower()
    {
        rotateSpeed += Time.deltaTime * fireData.ShootingDelay;
        partToRotate.rotation = Quaternion.Euler(0, rotateSpeed, 0);
    }


    void HandleShootingDelayUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            combinedShootingDelayPercentages += Mathf.Abs(fl);
            fireData.ShootingDelay = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.ShootingDelay, combinedShootingDelayPercentages);

            Debug.Log(fireData.ShootingDelay);
            Debug.Log(combinedShootingDelayPercentages);
        }
    }
}
