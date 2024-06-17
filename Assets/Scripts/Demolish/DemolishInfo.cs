using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemolishInfo : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onDemolishCostReductionUpgrade;
    public float DemolishCost;
    public float DurationToDemolish;
    public GameObject NormalTile;
    public Slider slider;

    [HideInInspector] public GatherState CurrentGatherState = GatherState.Gatherable;
    [HideInInspector] public Resource[] Resources;
    private void Awake()
    {
        Resources = GetComponents<Resource>();
    }

    private void OnEnable()
    {
        onDemolishCostReductionUpgrade.onEventRaised += HandleDemolishCostReductionUpgrade;
    }
    private void OnDisable()
    {
        onDemolishCostReductionUpgrade.onEventRaised -= HandleDemolishCostReductionUpgrade;
    }

    void HandleDemolishCostReductionUpgrade(object fl)
    {
        if (fl is float _amount)
        {
            DemolishCost = HelperFunctions.CalculatePercentage(DemolishCost, _amount);
        }
    }
}
