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

    float currentHealth;


    private void OnEnable()
    {
        currentHealth = (objectInfo.DefObjectGameData as EnemyData).BaseMaxHealth;
        UpdateHPBar();
    }




    private bool CheckIfDiedAndHandleDeath()
    {
        if (currentHealth <= 0)
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
        currentHealth -= damage;
        UpdateHPBar();
        return CheckIfDiedAndHandleDeath();
    }


    void UpdateHPBar()
    {
        healthBar.value = currentHealth / (objectInfo.DefObjectGameData as EnemyData).BaseMaxHealth;
    }
}
