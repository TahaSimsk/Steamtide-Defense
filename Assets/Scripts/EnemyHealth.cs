using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float baseMaxHealth;
    [HideInInspector] public float maxHealth;
    public int enemyId;

    float currentHealth;

    public bool isDead;


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
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            gameObject.SetActive(false);
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
