using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyHealth;

public static class EventManager
{
    public static event Action<Button> onButtonPressed;
    public static void OnButtonPressed(Button button) => onButtonPressed?.Invoke(button);


    public static event Action onESCPressed;
    public static void OnESCPressed() => onESCPressed?.Invoke();


    public static event Action onMoneyChanged;
    public static void OnMoneyChanged() => onMoneyChanged?.Invoke();


    public static event Action<float> onMoneyIncreased;
    public static void OnMoneyIncreased(float money) => onMoneyIncreased?.Invoke(money);


    public static event Action<float> onMoneyDecreased;
    public static void OnMoneyDecreased(float money) => onMoneyDecreased?.Invoke(money);


    public static event Action <GameObject> onEnemyDeath;
    public static void OnEnemyDeath(GameObject enemy) => onEnemyDeath?.Invoke(enemy);


    public static event Action<GameObject> onTowerPlaced;
    public static void OnTowerPlaced(GameObject tower) => onTowerPlaced?.Invoke(tower);

    //public static void Subscribe<T>(Action<T> handler)
    //{
    //    onEnemyDeath += obj => handler((T)obj);
    //}
    //public static void InvokeEvent<T>(T eventData)
    //{
    //    onEnemyDeath?.Invoke(eventData);
    //}

}
