using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPercantageManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onDemolishCostReductionUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalTowerDamageUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalShootingDelayUpgrade;

    [HideInInspector] public float GlobalDemolishCostReductionPercentage = 0;
    [HideInInspector] public float GlobalTowerDamagePercentage = 0;
    [HideInInspector] public float GlobalShootingDelayPercentage = 0;

    [HideInInspector] public float GlobalAmmoRefillCostReductionPercentage = 0;
    [HideInInspector] public float GlobalAmmoRefillAmountPercentage = 0;


    public static GlobalPercantageManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        onDemolishCostReductionUpgrade.onEventRaised += ChangeDemolishCostReductionPercentage;
        onGlobalTowerDamageUpgrade.onEventRaised += ChangeTowerDamagePercentage;
        onGlobalShootingDelayUpgrade.onEventRaised += ChangeShootingDelayPercentage;
    }

    private void OnDisable()
    {
        onDemolishCostReductionUpgrade.onEventRaised -= ChangeDemolishCostReductionPercentage;
        onGlobalTowerDamageUpgrade.onEventRaised -= ChangeTowerDamagePercentage;
        onGlobalShootingDelayUpgrade.onEventRaised -= ChangeShootingDelayPercentage;
    }

    public void ChangeDemolishCostReductionPercentage(object _amount)
    {
        if (_amount is float fl)
        {
            GlobalDemolishCostReductionPercentage += fl;
        }
    }

    public void ChangeTowerDamagePercentage(object _amount)
    {
        if (_amount is float fl)
        {
            GlobalTowerDamagePercentage += fl;
        }
    }

    public void ChangeShootingDelayPercentage(object _amount)
    {
        if (_amount is float fl)
        {
            GlobalShootingDelayPercentage += fl;
        }
    }

}
