using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTextUpdater : MonoBehaviour
{
    [SerializeField] MoneyManager moneyManager;

    [Header("Events")]
    [SerializeField] GameEvent0ParamSO onMoneyChanged;
    [SerializeField] GameEvent0ParamSO onWoodAmountChanged;
    [SerializeField] GameEvent0ParamSO onRockAmountChanged;

    [Header("Resource Texts")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI rockText;


    private void Start()
    {
        UpdateMoneyText();
        UpdateWoodText();
        UpdateRockText();
    }

    private void OnEnable()
    {
        onMoneyChanged.onEventRaised += UpdateMoneyText;
        onWoodAmountChanged.onEventRaised += UpdateWoodText;
        onRockAmountChanged.onEventRaised += UpdateRockText;
    }

    private void OnDisable()
    {
        onMoneyChanged.onEventRaised -= UpdateMoneyText;
        onWoodAmountChanged.onEventRaised -= UpdateWoodText;
        onRockAmountChanged.onEventRaised -= UpdateRockText;
    }

    void UpdateMoneyText()
    {
        moneyText.text = $"$ {moneyManager.CurrentMoneyAmount}"; ;
    }

    void UpdateWoodText()
    {
        woodText.text=$"Wood: {moneyManager.CurrentWoodAmount}";
    }

    void UpdateRockText()
    {
        rockText.text = $"Rock: {moneyManager.CurrentRockAmount}";
    }
}
