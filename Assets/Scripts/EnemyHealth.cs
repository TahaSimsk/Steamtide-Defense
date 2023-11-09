using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;

    float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
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
}
