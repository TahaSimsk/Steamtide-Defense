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
    public GameEvent0ParamSO onMoneyChanged;

    public float startingBalance;

    [HideInInspector] public float money;

    private void OnEnable()
    {
        money = startingBalance;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        money = startingBalance;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }


    public bool IsAffordable(float towerCost)
    {
        if (money >= towerCost)
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
        money += _amount;
        onMoneyChanged.RaiseEvent();
    }



    public void DecreaseMoney(float amount)
    {
        money -= amount;
        onMoneyChanged.RaiseEvent();
    }

}
