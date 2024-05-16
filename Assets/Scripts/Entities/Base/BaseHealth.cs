using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour, IPlayerDamageable
{
    [SerializeField] BaseData baseData;
    [SerializeField] Slider healthBar;
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onBaseDeath;


    float currentHealth;
    float baseMaxHealth;
    float maxHealth;

    void Awake()
    {
        baseMaxHealth = baseData.BaseHP;
        maxHealth = baseMaxHealth;
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }
    
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHPBar();
        CheckIfDiedAndHandleDeath();
    }

    void UpdateHPBar()
    {
        healthBar.value = currentHealth / baseData.BaseHP;
    }

    void CheckIfDiedAndHandleDeath()
    {
        if (currentHealth <= 0)
        {
            onBaseDeath.RaiseEvent(gameObject);

            gameObject.SetActive(false);
        }
    }

   
}
