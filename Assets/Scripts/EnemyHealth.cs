using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] float moneyDrop;
    [SerializeField] float baseMaxHealth;
    [HideInInspector] public float maxHealth;
    public int enemyId;

    float currentHealth;

    public bool isDead;

    MoneySystem moneySystem;
    UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        moneySystem = FindObjectOfType<MoneySystem>();
    }


    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateHPBar();
        isDead = false;
    }

    private void OnDisable()
    {
        maxHealth = baseMaxHealth;
        isDead = true;
    }


    void Update()
    {
        HandleDeath();
    }

    private void HandleDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            gameObject.SetActive(false);
            moneySystem.AddMoney(moneyDrop);
            moneySystem.UpdateMoneyDisplay();
            uiManager.UpdateRemainingEnemiesText(false);
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
