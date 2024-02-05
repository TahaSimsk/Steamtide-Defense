using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyManager", menuName = "MoneyManager")]
public class MoneyManager : ScriptableObject
{
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
        EventManager.onEnemyDeath += AddMoney;
        EventManager.onTowerPlaced += DecreaseMoney;
    }
    private void OnDisable()
    {
        EventManager.onEnemyDeath -= AddMoney;
        EventManager.onTowerPlaced -= DecreaseMoney;
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

    public void AddMoney(float amount)
    {
        //money += amount;
        //EventManager.OnMoneyChanged();
    }

    //this is used for enemy death, gets the data from the died enemy and gets the cost, ignore gameobject
    public void AddMoney(GameObject enemy)
    {
        money += enemy.GetComponent<EnemyHealth>().enemyData.objectCost_MoneyDrop;
        EventManager.OnMoneyChanged();
        EventManager.OnMoneyIncreased(money);
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;
        EventManager.OnMoneyChanged();
        EventManager.OnMoneyDecreased(money);
    }

    //this is used for tower placement, gets the data from last placed tower and gets the cost
    void DecreaseMoney(GameObject gameObject)
    {
        money -= gameObject.GetComponent<Tower>().towerData.objectCost_MoneyDrop;
        EventManager.OnMoneyChanged();
        EventManager.OnMoneyDecreased(this.money);
    }




}
