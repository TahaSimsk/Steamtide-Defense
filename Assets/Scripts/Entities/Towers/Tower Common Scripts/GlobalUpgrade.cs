using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GlobalUpgrade : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] string upgradeName;
    [SerializeField] string upgradeDescription;
    [SerializeField] float upgradeAmount;
    [SerializeField] float upgradeMoneyCost;
    [SerializeField] float upgradeWoodCost;
    [SerializeField] float upgradeRockCost;
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
        if (moneyManager.IsAffordable(upgradeMoneyCost, upgradeWoodCost, upgradeRockCost))
        {
            moneyManager.DecreaseMoney(upgradeMoneyCost);
            moneyManager.DecreaseWood(upgradeWoodCost);
            moneyManager.DecreaseRock(upgradeRockCost);

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
        //buttonText.text = upgradeDescription + upgradeAmount;
        buttonText.text = upgradeName;
    }

    private string anan()
    {
        string text = $"{upgradeDescription}{upgradeAmount}%\nUpgrade Cost: \n";

        if (upgradeMoneyCost != 0)
        {
            text += $"${upgradeMoneyCost}\n";
        }
        if (upgradeWoodCost != 0)
        {
            text += $"{upgradeWoodCost} Wood\n";
        }
        if (upgradeRockCost != 0)
        {
            text += $"{upgradeRockCost} Rock\n";
        }
        return text;

    }

    public void OnPointerMove(PointerEventData eventData)
    {
        TooltipManager.Instance.ShowTip(anan(), Input.mousePosition, moneyManager.IsAffordable(upgradeMoneyCost, upgradeWoodCost, upgradeRockCost));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.DisableTip();
    }
}
