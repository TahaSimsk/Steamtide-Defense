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

    [HideInInspector]
    public List<GameObject> targetsInRange = new List<GameObject>();



    private void Start()
    {
        ChangeRange(towerInfo.InstITower.TowerRange);
    }

    private void OnEnable()
    {
        onTargetDeath.onEventRaised += RemoveTarget;
        onEnemyReachedEnd.onEventRaised += RemoveTarget;
    }
    private void OnDisable()
    {
        onTargetDeath.onEventRaised -= RemoveTarget;
        onEnemyReachedEnd.onEventRaised -= RemoveTarget;
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

    public void ChangeRange(float range)
    {
        transform.localScale = new Vector3(range, 0.1f, range);
    }


    void RemoveTarget(object target)
    {
        if (target is GameObject && targetsInRange.Contains((GameObject)target))
        {
            targetsInRange.Remove((GameObject)target);
        }
    }

}
