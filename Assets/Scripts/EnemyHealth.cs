using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float baseMaxHealth;
    [HideInInspector] public float maxHealth;
    public int enemyId;

    float currentHealth;


    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void OnDisable()
    {
        maxHealth = baseMaxHealth;
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
