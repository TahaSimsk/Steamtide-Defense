using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Wave Related Text UI")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI remainingEnemiesText;
    [SerializeField] TextMeshProUGUI nextWaveCountdownText;


    EnemyWaveController waveController;


    //------------------ COUNTDOWN ------------------
    bool startCountdownForNextWave;
    float timerForNextWave;

  

    private void Awake()
    {
        waveController = FindObjectOfType<EnemyWaveController>();

    }


    void Update()
    {
        HandleESCPressed();

        StartNextWaveCountdown();
    }


    void HandleESCPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearMode();
        }
    }

    public void ClearMode()
    {
        EventManager.OnESCPressed();
    }



    void StartNextWaveCountdown()
    {
        if (startCountdownForNextWave)
        {
            nextWaveCountdownText.enabled = true;
            if (timerForNextWave > 1)
            {
                timerForNextWave -= Time.deltaTime;
                nextWaveCountdownText.text = "Next Wave Starts In: " + Mathf.FloorToInt(timerForNextWave);
            }
            else
            {
                startCountdownForNextWave = false;
            }
        }
        else
        {
            nextWaveCountdownText.enabled = false;
        }


    }




    public void UpdateWaveText(int currentWave)
    {
        waveText.text = "Wave: " + currentWave;
    }

    public void UpdateRemainingEnemiesText(bool onlyUpdateText)
    {
        if (!onlyUpdateText)
        {
            waveController.numOfTotalEnemies--;
        }

        remainingEnemiesText.text = "Enemies: " + waveController.numOfTotalEnemies;

    }


    public void GetNextWaveTimer(bool shouldCount, float timer)
    {
        startCountdownForNextWave = shouldCount;
        timerForNextWave = timer;
    }

}
