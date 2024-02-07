using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingEnemiesText : MonoBehaviour
{
    TextMeshProUGUI myText;

    int numOfTotalEnemies;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        EventManager.onEnemyDeath += UpdateRemainingEnemiesText;
        EventManager.onWaveStart += GetTotalEnemies;

    }

    private void OnDisable()
    {
        EventManager.onEnemyDeath -= UpdateRemainingEnemiesText;
        EventManager.onWaveStart += GetTotalEnemies;
    }

    void UpdateRemainingEnemiesText(GameObject gameObject)
    {
        numOfTotalEnemies--;
        myText.text = "Enemies: " + numOfTotalEnemies;
    }

    void GetTotalEnemies(int enemies)
    {
        numOfTotalEnemies = enemies;
        myText.text = "Enemies: " + numOfTotalEnemies;
    }
}
