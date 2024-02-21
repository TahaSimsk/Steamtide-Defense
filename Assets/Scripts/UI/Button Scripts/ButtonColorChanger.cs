using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onMoneyChanged;
    //public DataTower data;
    [SerializeField] MoneyManager moneyManager;

    public TowerData towerGameData;

    TextMeshProUGUI buttonText;
    Button button;

    //[HideInInspector]
    //public ITower towerGameData;

    private void Awake()
    {
        //towerGameData = (ITower)towerGameData;
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
        onMoneyChanged.onEventRaised += UpdatePlaceableButtonsColor;
    }
    private void OnDisable()
    {
        onMoneyChanged.onEventRaised -= UpdatePlaceableButtonsColor;
    }

    void SetButtonCostText()
    {
        //buttonText.text = dataTower.objectName + " $" + dataTower.objectCost_MoneyDrop;
        buttonText.text = towerGameData.objectName + " $" + towerGameData.TowerPlacementCost;
    }


    void UpdatePlaceableButtonsColor()
    {
        //if (data == null) return;


        //if (moneyManager.IsPlaceable(data.objectCost_MoneyDrop))
        //{
        //    button.image.color = Color.green;
        //}
        //else
        //{
        //    button.image.color = Color.red;
        //}
        if (towerGameData == null) return;


        if (moneyManager.IsPlaceable(towerGameData.TowerPlacementCost))
        {
            button.image.color = Color.green;
        }
        else
        {
            button.image.color = Color.red;
        }

    }
}
