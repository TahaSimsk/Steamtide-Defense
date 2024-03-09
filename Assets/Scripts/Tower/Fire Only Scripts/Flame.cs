using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] RangeUpgrade rangeUpgrade;
    FireData fireData;

    
    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        SetFlameScale();

        rangeUpgrade.OnRangeUpgraded += SetFlameScale;
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
}
