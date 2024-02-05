using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Data data;

    TextMeshProUGUI buttonText;
    Button button;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponentInChildren<Button>();
        SetButtonCostText();
    }

    private void OnEnable()
    {
        EventManager.onMoneyIncreased += UpdatePlaceableButtonsColor;
        EventManager.onMoneyDecreased += UpdatePlaceableButtonsColor;
    }
    private void OnDisable()
    {
        EventManager.onMoneyIncreased -= UpdatePlaceableButtonsColor;
        EventManager.onMoneyDecreased -= UpdatePlaceableButtonsColor;
    }

    void SetButtonCostText()
    {
        buttonText.text = data.objectName + " $" + data.objectCost_MoneyDrop;
    }


    void UpdatePlaceableButtonsColor(float money)
    {

        if (money >= data.objectCost_MoneyDrop)
        {
            button.image.color = Color.green;
            Debug.Log("Button placeable");
        }
        else
        {
            button.image.color = Color.red;
            Debug.Log("Button not placeable");
        }

    }
}
