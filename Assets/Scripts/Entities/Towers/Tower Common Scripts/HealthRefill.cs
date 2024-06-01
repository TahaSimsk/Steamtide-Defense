using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthRefill : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onHPRefillCostReductionUpgrade;
    [SerializeField] TowerHealth towerHealth;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] TextMeshProUGUI healthButtonText;
    Button healthButton;
    float refillCost;
    private void Start()
    {
        refillCost = towerInfo.DefTowerData.TowerHPRefillCost;
        healthButton = GetComponent<Button>();
        UpdateText();
        healthButton.onClick.AddListener(RefillHP);
        onHPRefillCostReductionUpgrade.onEventRaised += HandleHPRefillCostReductionUpgrade;
    }

    private void OnDestroy()
    {
        healthButton.onClick.RemoveListener(RefillHP);
        onHPRefillCostReductionUpgrade.onEventRaised -= HandleHPRefillCostReductionUpgrade;
    }
    
    void RefillHP()
    {
        if (towerHealth.CurrentHealth > towerHealth.MaxHealth - 1) return;

        if (moneyManager.IsAffordable(refillCost))
        {
            towerHealth.ResetHP();
            moneyManager.DecreaseMoney(refillCost);
        }
        else
        {
            //here goes logic when a refill is unaffordable
        }

    }

    void UpdateText()
    {
        healthButtonText.text = $"HP++ ({refillCost}$)";
    }

    void HandleHPRefillCostReductionUpgrade(object _amount)
    {
        refillCost = HelperFunctions.CalculatePercentage(refillCost, (float)_amount);
        UpdateText();
    }

}
