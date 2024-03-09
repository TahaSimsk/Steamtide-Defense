using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetScanner : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] GameEvent1ParamSO onTargetDeath;
    [SerializeField] GameEvent1ParamSO onEnemyReachedEnd;
    [SerializeField] RangeUpgrade rangeUpgrade;
    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();

    TowerData towerData;

    private void Start()
    {
        towerData = towerInfo.InstTowerData;
        ChangeRange();
    }

    private void OnEnable()
    {
        onTargetDeath.onEventRaised += RemoveTarget;
        onEnemyReachedEnd.onEventRaised += RemoveTarget;
        rangeUpgrade.OnRangeUpgraded += ChangeRange;
    }
    private void OnDisable()
    {
        onTargetDeath.onEventRaised -= RemoveTarget;
        onEnemyReachedEnd.onEventRaised -= RemoveTarget;
        rangeUpgrade.OnRangeUpgraded -= ChangeRange;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            targetsInRange.Remove(other.gameObject);

        }
    }

    public void ChangeRange()
    {
        transform.localScale = new Vector3(towerData.TowerRange, 0.1f, towerData.TowerRange);
    }


    void RemoveTarget(object target)
    {
        if (target is GameObject && targetsInRange.Contains((GameObject)target))
        {
            targetsInRange.Remove((GameObject)target);
        }
    }

}
