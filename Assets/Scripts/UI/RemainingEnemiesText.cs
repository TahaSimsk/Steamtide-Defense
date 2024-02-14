using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingEnemiesText : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onEnemyDeath;
    [SerializeField] GameEvent1ParamSO onWaveStart;
    [SerializeField] GameEvent1ParamSO onEnemyReachedEndOfPath;

    TextMeshProUGUI myText;

    int numOfTotalEnemies;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        onEnemyDeath.onEventRaised += DecreaseEnemiesByOneAndUpdateText;
        onWaveStart.onEventRaised += GetTotalEnemiesAndUpdateText;
        onEnemyReachedEndOfPath.onEventRaised += DecreaseEnemiesByOneAndUpdateText;
    }

    private void OnDisable()
    {
        onEnemyDeath.onEventRaised -= DecreaseEnemiesByOneAndUpdateText;
        onWaveStart.onEventRaised -= GetTotalEnemiesAndUpdateText;
        onEnemyReachedEndOfPath.onEventRaised -= DecreaseEnemiesByOneAndUpdateText;
    }

    void DecreaseEnemiesByOneAndUpdateText(object gameObject)
    {
        if (gameObject is GameObject)
        {
            numOfTotalEnemies--;
            myText.text = "Enemies: " + numOfTotalEnemies;
        }
    }

    void GetTotalEnemiesAndUpdateText(object amount)
    {
        if (amount is int)
        {
            numOfTotalEnemies = (int)amount;
            myText.text = "Enemies: " + numOfTotalEnemies;
        }
    }
}
