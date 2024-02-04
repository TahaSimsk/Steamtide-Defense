using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyHealth;

public static class EventManager
{
    public static event Action<Data, Button> onButtonPressed;
    public static void OnButtonPressed(Data data, Button button) => onButtonPressed?.Invoke(data, button);


    public static event Action onESCPressed;
    public static void OnESCPressed() => onESCPressed?.Invoke();


    public static event Action onMoneyChanged;
    public static void OnMoneyChanged() => onMoneyChanged?.Invoke();


    public static event Action<GameObject, Data> onEnemyDeath;
    public static void OnEnemyDeath(GameObject enemy, Data data) => onEnemyDeath?.Invoke(enemy, data);


    public static event Action<Data> onTowerPlaced;
    public static void OnTowerPlaced(Data data) => onTowerPlaced?.Invoke(data);

}
