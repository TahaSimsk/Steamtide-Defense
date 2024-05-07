using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUpgrade : MonoBehaviour
{
    [SerializeField] string upgradeName;
    [SerializeField] string upgradeDescription;
    [SerializeField] float upgradeAmount;
    [SerializeField] float upgradeCost;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] GameEvent1ParamSO upgradeEvent;
    [SerializeField] TextMeshProUGUI buttonText;
    
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ReduceAmmoRefillCost);
        UpdateText();
    }

    void ReduceAmmoRefillCost()
    {
        if (moneyManager.IsPlaceable(upgradeCost))
        {
            moneyManager.DecreaseMoney(upgradeCost);

            upgradeEvent.RaiseEvent(upgradeAmount);

            button.interactable = false;
        }
        else
        {
            //here goes the logic when the upgrade is unaffordable
        }
    }

    void UpdateText()
    {
        buttonText.text = upgradeDescription + upgradeAmount + $" ({upgradeCost}$)";
    }
}
