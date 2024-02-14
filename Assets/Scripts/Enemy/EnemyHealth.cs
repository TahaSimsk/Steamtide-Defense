using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onEnemyDeath;

    public GameData enemyData;
    [SerializeField] GameObject highlightPrefab;
    [SerializeField] Slider healthBar;



    float currentHealth;

    [HideInInspector] public float maxHealth;

    private void Awake()
    {
        maxHealth =((IEnemy)enemyData).BaseMaxHealth;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
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

    public void SetMaxHP(float amount)
    {
        maxHealth *= amount;
    }


    void UpdateHPBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
