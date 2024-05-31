using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MoneyManager", menuName = "Managers/MoneyManager")]
public class MoneyManager : ScriptableObject
{
    [Header("Events")]
    [SerializeField] GameEvent0ParamSO onMoneyChanged;
    [SerializeField] GameEvent0ParamSO onWoodAmountChanged;
    [SerializeField] GameEvent0ParamSO onRockAmountChanged;

    public float startingBalance;

    [HideInInspector] public float CurrentMoneyAmount;
    [HideInInspector] public float CurrentWoodAmount;
    [HideInInspector] public float CurrentRockAmount;



    void OnEnable()
    {
        ResetResources();
        SceneManager.activeSceneChanged += ResetResourcesOnSceneChange;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= ResetResourcesOnSceneChange;
    }

    void ResetResourcesOnSceneChange(Scene arg0, Scene arg1)
    {
        ResetResources();
    }

    void ResetResources()
    {
        CurrentMoneyAmount = startingBalance;
        CurrentWoodAmount = 0;
        CurrentRockAmount = 0;
    }




    public bool IsAffordable(float towerCost)
    {
        if (CurrentMoneyAmount >= towerCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsAffordable(float _moneyCost, float _woodCost, float _rockCost)
    {
        if (CurrentMoneyAmount >= _moneyCost && CurrentWoodAmount >= _woodCost && CurrentRockAmount >= _rockCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void AddMoney(float _amount)
    {
        CurrentMoneyAmount += _amount;
        onMoneyChanged.RaiseEvent();
    }

    public void DecreaseMoney(float amount)
    {
        CurrentMoneyAmount -= amount;
        onMoneyChanged.RaiseEvent();
    }

    public void AddWood(float _amount)
    {
        CurrentWoodAmount += _amount;
        onWoodAmountChanged.RaiseEvent();
    }

    public void DecreaseWood(float _amount)
    {
        CurrentWoodAmount -= _amount;
        onWoodAmountChanged.RaiseEvent();
    }

    public void AddRock(float _amount)
    {
        CurrentRockAmount += _amount;
        onRockAmountChanged.RaiseEvent();
    }
    public void DecreaseRock(float _amount)
    {
        CurrentRockAmount -= _amount;
        onRockAmountChanged.RaiseEvent();
    }

}
