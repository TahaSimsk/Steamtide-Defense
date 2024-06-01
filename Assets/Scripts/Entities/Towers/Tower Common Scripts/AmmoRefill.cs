using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefill : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onAmmoRefillCostReductionUpgrade;
    [SerializeField] GameEvent1ParamSO onAmmoRefillAmountUpgrade;
    [SerializeField] AmmoManager ammoManager;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] TextMeshProUGUI ammoButtonText;

    Button ammoButton;
    float refillCost;
    int refillAmount;

    private void Start()
    {
        ammoButton = GetComponent<Button>();

        refillCost = towerInfo.DefTowerData.TowerAmmoRefillCost;
        refillAmount = towerInfo.DefTowerData.TowerAmmoRefillAmount;

        UpdateText();

        ammoButton.onClick.AddListener(RefillAmmo);
        onAmmoRefillCostReductionUpgrade.onEventRaised += HandleAmmoRefillCostReductionUpgrade;
        onAmmoRefillAmountUpgrade.onEventRaised += HandleAmmoRefillAmountUpgrade;
    }

    private void OnDestroy()
    {
        ammoButton.onClick.RemoveListener(RefillAmmo);
        onAmmoRefillCostReductionUpgrade.onEventRaised -= HandleAmmoRefillCostReductionUpgrade;
        onAmmoRefillAmountUpgrade.onEventRaised -= HandleAmmoRefillAmountUpgrade;
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

        //if (_amount is float fl)
        //{
        //    ammoRefillCostReductionPercentage += fl;

        //    refillCost += towerInfo.DefTowerData.TowerAmmoRefillCost * ammoRefillCostReductionPercentage * 0.01f;

        //}



        refillCost = HelperFunctions.CalculatePercentage(refillCost, (float)_amount);
            UpdateText();
    }
    void HandleAmmoRefillAmountUpgrade(object _amount)
    {

        //if (_amount is float fl)
        //{
        //    ammoRefillAmountPercentage += fl;

        //    refillAmount += (int)(towerInfo.DefTowerData.TowerAmmoRefillAmount * ammoRefillCostReductionPercentage * 0.01f);

        //}

        refillAmount = (int)HelperFunctions.CalculatePercentage((float)refillAmount, (float)_amount);
            UpdateText();
    }


    void UpdateText()
    {
        ammoButtonText.text = $"+{refillAmount} AMMO ({refillCost}$)";
    }
}
