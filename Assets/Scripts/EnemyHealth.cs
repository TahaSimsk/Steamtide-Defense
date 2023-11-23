using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float moneyDrop;
    [SerializeField] float baseMaxHealth;
    [HideInInspector] public float maxHealth;
    public int enemyId;

    float currentHealth;

    public bool isDead;

    MoneySystem scoreSystem;
    UIManager uiManager;

    private void Start()
    {
        uiManager=FindObjectOfType<UIManager>();
        scoreSystem = FindObjectOfType<MoneySystem>();
    }


    private void OnEnable()
    {
        currentHealth = maxHealth;
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
            scoreSystem.AddMoney(moneyDrop);
            scoreSystem.UpdateMoneyDisplay();
            uiManager.UpdateRemainingEnemiesText(false);
        }
    }

    public void ReduceHealth(float damage)
    {
        currentHealth -= damage;
    }

    public void SetMaxHP(float amount)
    {
        maxHealth *= amount;
    }
}
