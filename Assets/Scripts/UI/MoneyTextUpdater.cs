using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextUpdater : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onMoneyChanged;
    [SerializeField] MoneyManager moneyManager;
    TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        onMoneyChanged.onEventRaised += UpdateMoneyText;
    }
    private void OnDisable()
    {
        onMoneyChanged.onEventRaised -= UpdateMoneyText;
    }

    void UpdateMoneyText()
    {
        moneyText.text = "$" + moneyManager.money;
    }
}
