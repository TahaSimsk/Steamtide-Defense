using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] RangeUpgrade rangeUpgrade;
    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] GameEvent1ParamSO onTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalTowerRangeUpgrade;
    FireData fireData;

    float damagePercentage;
    float rangePercentage;
    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        SetFlameScale(GlobalPercentageManager.Instance.GlobalRangePercentage);

        HandleDamageUpgrade(GlobalPercentageManager.Instance.GlobalTowerDamagePercentage);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHealth>().ReduceHealth(fireData.ProjectileDamage);
    }

    void SetFlameScale(object _amount)
    {
        if (_amount is float fl)
        {
            rangePercentage += fl;
            Vector3 parentScale = transform.parent.localScale;
            fireData.TowerRange = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.TowerRange, rangePercentage);
            transform.parent.localScale = new Vector3(parentScale.x, parentScale.y, fireData.TowerRange);
        }
     
    }
    private void OnEnable()
    {
        damageUpgrade.OnDamageUpgraded += HandleDamageUpgrade;
        onTowerDamageUpgrade.onEventRaised += HandleDamageUpgrade;
        rangeUpgrade.OnRangeUpgraded += SetFlameScale;
        onGlobalTowerRangeUpgrade.onEventRaised += SetFlameScale;

    }
    private void OnDisable()
    {
        damageUpgrade.OnDamageUpgraded -= HandleDamageUpgrade;
        onTowerDamageUpgrade.onEventRaised -= HandleDamageUpgrade;
        rangeUpgrade.OnRangeUpgraded -= SetFlameScale;
        onGlobalTowerRangeUpgrade.onEventRaised -= SetFlameScale;
    }
    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            damagePercentage += fl;
            fireData.ProjectileDamage = towerInfo.DefTowerData.ProjectileDamage + towerInfo.DefTowerData.ProjectileDamage * damagePercentage * 0.01f;
        }
    }
}
