using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyManager", menuName = "Managers/MoneyManager")]
public class MoneyManager : ScriptableObject
{
    [Header("Events")]
    public GameEvent2ParamSO onTowerPlaced;
    public GameEvent1ParamSO onEnemyDeath;
    public GameEvent0ParamSO onMoneyChanged;

    [Header("Costs to Place Towers")]
    public float ballistaCost;
    public float blasterCost;
    public float cannonCost;

    [Header("Costs for Skills")]
    public float bombCost;
    public float slowCost;

    [Header("Costs for Demolish")]
    public float oneTreeDemolishCost;
    public float doubleTreeDemolishCost;
    public float quadTreeDemolishCost;


    public float startingBalance;

    [HideInInspector] public float money;

    private void OnEnable()
    {
        money = startingBalance;
        //EventManager.onEnemyDeath += AddMoney;
        onEnemyDeath.onEventRaised += AddMoney;
        onTowerPlaced.onEventRaised += DecreaseMoney;
    }


    private void OnDisable()
    {
        //EventManager.onEnemyDeath -= AddMoney;
        onEnemyDeath.onEventRaised += AddMoney;
        onTowerPlaced.onEventRaised -= DecreaseMoney;
    }


    public bool IsPlaceable(float towerCost)
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


    public void AddMoney(object enemy)
    {
        if (enemy is GameObject g)
        {
            EnemyData enemyData = g.GetComponent<ObjectInfo>().DefObjectGameData as EnemyData;
            money += enemyData.MoneyDrop;
            onMoneyChanged.RaiseEvent();
        }
    }



    public void DecreaseMoney(float amount)
    {
        money -= amount;
        onMoneyChanged.RaiseEvent();
    }

    public void DecreaseMoney(object gameObject, object amount)
    {
        if (amount is float)
        {
            money -= (float)amount;
            onMoneyChanged.RaiseEvent();
        }

    }




}
