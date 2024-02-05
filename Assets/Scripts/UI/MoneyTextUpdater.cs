using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextUpdater : MonoBehaviour
{
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
        EventManager.onMoneyChanged += UpdateMoneyText;
    }
    private void OnDisable()
    {
        EventManager.onMoneyChanged -= UpdateMoneyText;
    }

    void UpdateMoneyText()
    {
        moneyText.text = "$" + moneyManager.money;
    }
}
