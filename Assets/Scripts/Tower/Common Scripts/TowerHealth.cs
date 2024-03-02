using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onTowerDeath;
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] Slider healthBar;



    float currentHealth;
    float baseMaxHealth;
    float maxHealth;

    private void Awake()
    {
        baseMaxHealth = towerInfo.DefTowerData.BaseMaxHealth;
        maxHealth = baseMaxHealth;
    }

    private void Start()
    {
        
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
            onTowerDeath.RaiseEvent(gameObject);

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
        maxHealth = (baseMaxHealth * amount * 0.01f) + baseMaxHealth;
        //currentHealth = maxHealth;
        Debug.Log(maxHealth.ToString());
        UpdateHPBar();
    }


    void UpdateHPBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
