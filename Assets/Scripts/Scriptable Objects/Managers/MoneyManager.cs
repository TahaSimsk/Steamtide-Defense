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
    }
    private void OnDisable()
    {
        EventManager.onEnemyDeath -= AddMoney;
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
        EventManager.OnMoneyDecreased(this.money);
    }




}
