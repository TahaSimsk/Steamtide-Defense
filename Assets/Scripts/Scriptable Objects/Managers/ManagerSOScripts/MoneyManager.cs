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
    [HideInInspector] public int CurrentWoodAmount;
    [HideInInspector] public int CurrentRockAmount;


   
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

    public void AddWood(int _amount)
    {
        CurrentWoodAmount += _amount;
        onWoodAmountChanged.RaiseEvent();
    }

    public void RemoveWood(int _amount)
    {
        CurrentWoodAmount -= _amount;
        onWoodAmountChanged.RaiseEvent();
    }

    public void AddRock(int _amount)
    {
        CurrentRockAmount += _amount;
        onRockAmountChanged.RaiseEvent();
    }
    public void RemoveRock(int _amount)
    {
        CurrentRockAmount -= _amount;
        onRockAmountChanged.RaiseEvent();
    }

}
