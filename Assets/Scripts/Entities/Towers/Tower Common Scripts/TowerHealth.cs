using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour, IPlayerDamageable
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onTowerDeath;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] Slider healthBar;



    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    float baseMaxHealth;


    private void Awake()
    {
        baseMaxHealth = towerInfo.DefTowerData.BaseMaxHealth;
        MaxHealth = baseMaxHealth;
    }

    private void OnEnable()
    {
        ResetHP();
        UpdateHPBar();
    }


    private void CheckIfDiedAndHandleDeath()
    {
        if (CurrentHealth <= 0)
        {
            onTowerDeath.RaiseEvent(gameObject);

            gameObject.SetActive(false);
        }
    }



    public void ReduceHealth(float damage)
    {
        CurrentHealth -= damage;
        UpdateHPBar();
        CheckIfDiedAndHandleDeath();
    }

    public void SetMaxHP(float amount)
    {
        MaxHealth = (baseMaxHealth * amount * 0.01f) + baseMaxHealth;
        UpdateHPBar();
    }

    public void ResetHP()
    {
        CurrentHealth = MaxHealth;
        UpdateHPBar();
    }


    void UpdateHPBar()
    {
        healthBar.value = CurrentHealth / MaxHealth;
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateHPBar();
        CheckIfDiedAndHandleDeath();
    }
}
