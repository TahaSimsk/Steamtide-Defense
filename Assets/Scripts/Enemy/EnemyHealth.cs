using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public DataEnemies enemyData;
    [SerializeField] GameObject highlightPrefab;
    [SerializeField] Slider healthBar;



    float currentHealth;

    [HideInInspector] public float maxHealth;

    private void Awake()
    {
        maxHealth = enemyData.baseMaxHealth;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateHPBar();
    }



    void Update()
    {
        HandleDeath();
    }

    private void HandleDeath()
    {
        if (currentHealth <= 0)
        {
            EventManager.OnEnemyDeath(this.gameObject);

            highlightPrefab.SetActive(false);
            gameObject.SetActive(false);
        }
    }



    public void ReduceHealth(float damage)
    {
        currentHealth -= damage;
        UpdateHPBar();

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
