using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    //------------------ COUNTDOWN ------------------
    bool startCountdownForNextWave;
    float timerForNextWave;


    private void Awake()
    {
        waveController = FindObjectOfType<EnemyWaveController>();

        flagManager = FindObjectOfType<FlagManager>();
    }


    void Start()
    {
        SetButtonCostTextOnStart();

    }


    void Update()
    {
        ChangeButtonsColorBasedOnPlaceability();

        HandleButtonSelection();

        StartNextWaveCountdown();
    }


    void SetButtonCostTextOnStart()
    {
        ballistaCostText.text = "Ballista: $" + moneySystem.ballistaCost;

        blasterCostText.text = "Blaster: $" + moneySystem.blasterCost;

        cannonCostText.text = "Cannon: $" + moneySystem.cannonCost;

        bombCostText.text = "Bomb: $" + moneySystem.bombCost;

        slowCostText.text = "Slow: $" + moneySystem.slowCost;
    }


    void ChangeButtonsColorBasedOnPlaceability()
    {
        UpdatePlaceableButtonsColor(moneySystem.ballistaCost, ballistaCostText);

        UpdatePlaceableButtonsColor(moneySystem.blasterCost, blasterCostText);

        UpdatePlaceableButtonsColor(moneySystem.cannonCost, cannonCostText);

        UpdatePlaceableButtonsColor(moneySystem.bombCost, bombCostText);

        UpdatePlaceableButtonsColor(moneySystem.slowCost, slowCostText);
    }


    void UpdatePlaceableButtonsColor(float weaponCost, TextMeshProUGUI text)
    {
        if (moneySystem.IsPlaceable(weaponCost))
        {
            text.transform.parent.GetComponent<Button>().image.color = Color.green;
        }
        else
        {
            text.transform.parent.GetComponent<Button>().image.color = Color.red;
        }
    }


    void HandleButtonSelection()
    {
        ButtonSelectionLogic(ballistaButton, flagManager.ballistaMode);

        ButtonSelectionLogic(blasterButton, flagManager.blasterMode);

        ButtonSelectionLogic(cannonButton, flagManager.cannonMode);

        ButtonSelectionLogic(bombButton, flagManager.bombMode);

        ButtonSelectionLogic(upgradeButton, flagManager.upgradeMode);

        ButtonSelectionLogic(slowButton, flagManager.slowMode);

        ButtonSelectionLogic(demolishButton, flagManager.demolishMode);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cancelButton.gameObject.SetActive(false);
            DelegateManager.onESCPressed?.Invoke();
           
            flagManager.ClearMode();
        }
    }


    void ButtonSelectionLogic(Button button, bool mode)
    {
        if (mode)
        {
            //button.gameObject.SetActive(false);
            button.interactable = false;
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            //button.gameObject.SetActive(true);
            button.interactable = true;
        }
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
