using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRefill : MonoBehaviour
{
    [SerializeField] AmmoManager ammoManager;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] TextMeshProUGUI ammoButtonText;
    Button ammoButton;
    float refillCost;
    int refillAmount;

    private void Start()
    {
        refillCost = towerInfo.DefTowerData.TowerAmmoRefillCost;
        refillAmount = towerInfo.DefTowerData.TowerAmmoRefillAmount;

        ammoButton = GetComponent<Button>();
        ammoButtonText.text = $"+{refillAmount} AMMO ({refillCost}$)";

        ammoButton.onClick.AddListener(RefillAmmo);
    }

    private void OnDestroy()
    {
        ammoButton.onClick.RemoveListener(RefillAmmo);
    }

    void RefillAmmo()
    {
        if (ammoManager.CurrentAmmoCount > towerInfo.InstTowerData.TowerAmmoCapacity - 1) return;

        if (moneyManager.IsPlaceable(refillCost))
        {
            ammoManager.AddAmmo(refillAmount);
            moneyManager.DecreaseMoney(refillCost);
        }
        else
        {
            //here goes logic when a refill is unaffordable
        }

    }
}
