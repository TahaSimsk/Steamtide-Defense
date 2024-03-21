using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ObjectInfo objectInfo;
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




    private void CheckIfDiedAndHandleDeath()
    {
        if (currentHealth <= 0)
        {
            onEnemyDeath.RaiseEvent(gameObject);

            highlightPrefab.SetActive(false);
            gameObject.SetActive(false);
        }
    }



    public void ReduceHealth(float damage)
    {
        currentHealth -= damage;
        UpdateHPBar();
        CheckIfDiedAndHandleDeath();
    }


    void UpdateHPBar()
    {
        healthBar.value = currentHealth / (objectInfo.DefObjectGameData as EnemyData).BaseMaxHealth;
    }
}
