using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefill : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onGlobalAmmoRefillCostReductionUpgrade;
    [SerializeField] GameEvent1ParamSO onGlobalAmmoRefillAmountUpgrade;
    [SerializeField] AmmoManager ammoManager;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] TextMeshProUGUI ammoButtonText;

    Button ammoButton;
    float refillCost;
    int refillAmount;

    float ammoRefillCostReductionPercentage;
    float ammoRefillAmountPercentage;

    private void Start()
    {
        ammoButton = GetComponent<Button>();

        refillCost = towerInfo.DefTowerData.TowerAmmoRefillCost;
        refillAmount = towerInfo.DefTowerData.TowerAmmoRefillAmount;

        HandleAmmoRefillAmountUpgrade(GlobalPercentageManager.Instance.GlobalAmmoRefillAmountPercentage);
        HandleAmmoRefillCostReductionUpgrade(GlobalPercentageManager.Instance.GlobalAmmoRefillCostReductionPercentage);

        ammoButton.onClick.AddListener(RefillAmmo);
        onGlobalAmmoRefillCostReductionUpgrade.onEventRaised += HandleAmmoRefillCostReductionUpgrade;
        onGlobalAmmoRefillAmountUpgrade.onEventRaised += HandleAmmoRefillAmountUpgrade;
    }

    private void OnDestroy()
    {
        ammoButton.onClick.RemoveListener(RefillAmmo);
        onGlobalAmmoRefillCostReductionUpgrade.onEventRaised -= HandleAmmoRefillCostReductionUpgrade;
        onGlobalAmmoRefillAmountUpgrade.onEventRaised -= HandleAmmoRefillAmountUpgrade;
    }

    void RefillAmmo()
    {
        if (ammoManager.CurrentAmmoCount > towerInfo.InstTowerData.TowerAmmoCapacity - 1) return;

        if (moneyManager.IsAffordable(refillCost))
        {
            ammoManager.AddAmmo(refillAmount);
            moneyManager.DecreaseMoney(refillCost);
        }
        else
        {
            //here goes logic when a refill is unaffordable
        }

    }


    void HandleAmmoRefillCostReductionUpgrade(object _amount)
    {

        if (_amount is float fl)
        {
            ammoRefillCostReductionPercentage += fl;

            refillCost = HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.TowerAmmoRefillCost, ammoRefillCostReductionPercentage);

            UpdateText();
        }

    }
    void HandleAmmoRefillAmountUpgrade(object _amount)
    {

        if (_amount is float fl)
        {
            ammoRefillAmountPercentage += fl;

            refillAmount = (int)HelperFunctions.CalculatePercentage(towerInfo.DefTowerData.TowerAmmoRefillAmount, ammoRefillAmountPercentage);

            UpdateText();
        }

    }


    void UpdateText()
    {
        ammoButtonText.text = $"+{refillAmount} AMMO ({refillCost}$)";
    }
}
