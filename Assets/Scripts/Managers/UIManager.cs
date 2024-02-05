using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Data> datas = new List<Data>();

    [SerializeField] MoneySystem moneySystem;

    [Header("Tower Cost Text UI")]
    [SerializeField] TextMeshProUGUI ballistaCostText;
    [SerializeField] TextMeshProUGUI blasterCostText;
    [SerializeField] TextMeshProUGUI cannonCostText;

    [Header("Skill Cost Text UI")]
    [SerializeField] TextMeshProUGUI bombCostText;
    [SerializeField] TextMeshProUGUI slowCostText;

    [Header("Wave Related Text UI")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI remainingEnemiesText;
    [SerializeField] TextMeshProUGUI nextWaveCountdownText;

    [Header("Buttons")]
    [SerializeField] Button ballistaButton;
    [SerializeField] Button blasterButton;
    [SerializeField] Button cannonButton;
    [SerializeField] Button bombButton;
    [SerializeField] Button slowButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button demolishButton;

    EnemyWaveController waveController;
    FlagManager flagManager;

    Button lastPressedButton;

    //------------------ COUNTDOWN ------------------
    bool startCountdownForNextWave;
    float timerForNextWave;

    private void OnEnable()
    {
        EventManager.onButtonPressed += HandleButtonSelection2;
        EventManager.onEnemyDeath += UpdateRemainingEnemiesText;
    }

    private void OnDisable()
    {
        EventManager.onButtonPressed -= HandleButtonSelection2;
        EventManager.onEnemyDeath -= UpdateRemainingEnemiesText;
    }


    private void Awake()
    {
        waveController = FindObjectOfType<EnemyWaveController>();

        flagManager = FindObjectOfType<FlagManager>();
    }




    void Update()
    {


        HandleESCPressed();

        StartNextWaveCountdown();
    }


    Data currentData;
    Button currentButton;
    void HandleButtonSelection2(Button button)
    {
        //currentData = data;
        //currentButton = button;
        ButtonSelectionLogic(button);

        //UpdatePlaceableButtonsColor(data.cost, button.GetComponentInChildren<TextMeshProUGUI>());
        //button.GetComponentInChildren<TextMeshProUGUI>().text = data.objectName + ": $" + data.cost;
    }

    void ButtonSelectionLogic(Button button)
    {
        if (lastPressedButton != button && lastPressedButton != null)
        {
            lastPressedButton.interactable = true;
        }
        lastPressedButton = button;
        //button.gameObject.SetActive(false);
        button.interactable = false;
        cancelButton.gameObject.SetActive(true);

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
        flagManager.ChangeCurrentMode((int)FlagManager.CurrentMode.def);
        if (lastPressedButton != null)
            lastPressedButton.interactable = true;
        EventManager.OnESCPressed();
        cancelButton.gameObject.SetActive(false);
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


    //this method is for when an enemy dies, ignore gameobject and data
    public void UpdateRemainingEnemiesText(GameObject enemy)
    {
        waveController.numOfTotalEnemies--;
        remainingEnemiesText.text = "Enemies: " + waveController.numOfTotalEnemies;
    }

    public void GetNextWaveTimer(bool shouldCount, float timer)
    {
        startCountdownForNextWave = shouldCount;
        timerForNextWave = timer;
    }

}
