using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] RangeUpgrade rangeUpgrade;
    [SerializeField] DamageUpgrade damageUpgrade;
    [SerializeField] GameEvent1ParamSO onTowerDamageUpgrade;
    FireData fireData;

    float percentage;
    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        SetFlameScale();

        rangeUpgrade.OnRangeUpgraded += SetFlameScale;
        HandleDamageUpgrade(GlobalPercantageManager.Instance.GlobalTowerDamagePercentage);
    }

    private void OnDestroy()
    {
        rangeUpgrade.OnRangeUpgraded -= SetFlameScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHealth>().ReduceHealth(fireData.ProjectileDamage);
    }

    void SetFlameScale()
    {
        Vector3 parentScale = transform.parent.localScale;
        transform.parent.localScale = new Vector3(parentScale.x, parentScale.y, fireData.TowerRange);
    }
    private void OnEnable()
    {
        damageUpgrade.OnDamageUpgraded += HandleDamageUpgrade;
        onTowerDamageUpgrade.onEventRaised += HandleDamageUpgrade;
    }
    private void OnDisable()
    {
        damageUpgrade.OnDamageUpgraded -= HandleDamageUpgrade;
        onTowerDamageUpgrade.onEventRaised -= HandleDamageUpgrade;
    }
    void HandleDamageUpgrade(object _amount)
    {
        if (_amount is float fl)
        {
            percentage += fl;
            fireData.ProjectileDamage = towerInfo.DefTowerData.ProjectileDamage + towerInfo.DefTowerData.ProjectileDamage * percentage * 0.01f;
        }
    }
}
