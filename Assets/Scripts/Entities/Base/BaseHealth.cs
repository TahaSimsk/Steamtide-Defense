using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] BaseData baseData;
    [SerializeField] Slider healthBar;
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onBaseDeath;
    [SerializeField] GameEvent1ParamSO onEnemyReachBase;


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
        onEnemyReachBase.onEventRaised += ReduceHealth;
    }
    void OnDisable()
    {
        onEnemyReachBase.onEventRaised -= ReduceHealth;
    }

    public void ReduceHealth(object enemy)
    {
        if (enemy is GameObject g)
        {
            ObjectInfo enemyInfo = g.GetComponent<ObjectInfo>();
            EnemyData enemyData = enemyInfo.DefObjectGameData as EnemyData;
            currentHealth -= enemyData.Damage;
            UpdateHPBar();
            CheckIfDiedAndHandleDeath();
        }
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
