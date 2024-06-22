using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ObjectInfo objectInfo;
    [SerializeField] MoneyManager moneyManager;
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onEnemyDeath;
    [SerializeField] GameObject highlightPrefab;
    [SerializeField] Slider healthBar;

  [HideInInspector] public float CurrentHealth;


    private void OnEnable()
    {
        CurrentHealth = (objectInfo.DefObjectGameData as EnemyData).BaseMaxHealth;
        UpdateHPBar();
    }




    private bool CheckIfDiedAndHandleDeath()
    {
        if (CurrentHealth <= 0)
        {
            onEnemyDeath.RaiseEvent(gameObject);
            moneyManager.AddMoney((objectInfo.DefObjectGameData as EnemyData).MoneyDrop);
            highlightPrefab.SetActive(false);
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }



    public bool ReduceHealth(float damage)
    {
        CurrentHealth -= damage;
        UpdateHPBar();
        return CheckIfDiedAndHandleDeath();
    }


    void UpdateHPBar()
    {
        healthBar.value = CurrentHealth / (objectInfo.DefObjectGameData as EnemyData).BaseMaxHealth;
    }
}
