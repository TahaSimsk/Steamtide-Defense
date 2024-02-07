using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Data data;
    [SerializeField] MoneyManager moneyManager;

    TextMeshProUGUI buttonText;
    Button button;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponentInChildren<Button>();

        
    }

    private void Start()
    {
        SetButtonCostText();
        UpdatePlaceableButtonsColor();
    }


    private void OnEnable()
    {
        EventManager.onMoneyChanged += UpdatePlaceableButtonsColor;
    }
    private void OnDisable()
    {
        EventManager.onMoneyChanged -= UpdatePlaceableButtonsColor;
    }

    void SetButtonCostText()
    {
        buttonText.text = data.objectName + " $" + data.objectCost_MoneyDrop;
    }


    void UpdatePlaceableButtonsColor()
    {
        if (data == null) return;

        
        if (moneyManager.IsPlaceable(data.objectCost_MoneyDrop))
        {
            button.image.color = Color.green;
        }
        else
        {
            button.image.color = Color.red;
        }

    }
}
