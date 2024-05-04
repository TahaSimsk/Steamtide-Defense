using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthRefill : MonoBehaviour
{
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
        healthButtonText.text = $"HP++ ({refillCost}$)";

        healthButton.onClick.AddListener(RefillHP);
    }

    private void OnDestroy()
    {
        healthButton.onClick.RemoveListener(RefillHP);
    }

    void RefillHP()
    {
        if (towerHealth.CurrentHealth > towerHealth.MaxHealth - 1) return;

        if (moneyManager.IsPlaceable(refillCost))
        {
            towerHealth.ResetHP();
            moneyManager.DecreaseMoney(refillCost);
        }
        else
        {
            //here goes logic when a refill is unaffordable
        }

    }


}
