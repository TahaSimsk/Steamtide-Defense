using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject highlightPrefab;
    [SerializeField] Slider healthBar;
    [SerializeField] float moneyDrop;
    [SerializeField] float baseMaxHealth;
    [HideInInspector] public float maxHealth;
    public int enemyId;

    float currentHealth;

    public bool isDead;

    MoneySystem moneySystem;
    UIManager uiManager;

    List<Projectile> projectiles = new List<Projectile>();

    Tower tower;

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
            moneySystem.AddMoney(moneyDrop);
            moneySystem.UpdateMoneyDisplay();
            uiManager.UpdateRemainingEnemiesText(false);
            if (tower != null)
            {
                tower.RemoveEnemy(gameObject);

                tower = null;
            }

            if (projectiles.Count > 0)
            {
                foreach (var projectile in projectiles)
                {
                    projectile.ResetTarget();
                }
                projectiles.Clear();
            }
            highlightPrefab.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void GetProjectile(Projectile projectile)
    {
        if (!projectiles.Contains(projectile))
        {
            projectiles.Add(projectile);
        }
    }

    public void RemoveProjectile(Projectile projectile)
    {
        if (projectiles.Contains(projectile))
        {
            projectiles.Remove(projectile);
        }
    }

    public void GetTower(Tower tower)
    {
        this.tower = tower;
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
