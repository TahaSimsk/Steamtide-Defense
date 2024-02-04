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
        FlagManager.onStateChanged += HandleButtonSelection;
        EventManager.onButtonPressed += HandleButtonSelection2;
        EventManager.onEnemyDeath += UpdateRemainingEnemiesText;
    }

    private void OnDisable()
    {
        FlagManager.onStateChanged -= HandleButtonSelection;
        EventManager.onButtonPressed -= HandleButtonSelection2;
    }


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

        HandleESCPressed();

        StartNextWaveCountdown();
    }


    void SetButtonCostTextOnStart()
    {
        //ballistaCostText.text = "Ballista: $" + moneySystem.ballistaCost;

        blasterCostText.text = "Blaster: $" + moneySystem.blasterCost;

        cannonCostText.text = "Cannon: $" + moneySystem.cannonCost;

        bombCostText.text = "Bomb: $" + moneySystem.bombCost;

        slowCostText.text = "Slow: $" + moneySystem.slowCost;
    }


    void ChangeButtonsColorBasedOnPlaceability()
    {
        //UpdatePlaceableButtonsColor(moneySystem.ballistaCost, ballistaCostText);

        UpdatePlaceableButtonsColor(moneySystem.blasterCost, blasterCostText);

        UpdatePlaceableButtonsColor(moneySystem.cannonCost, cannonCostText);

        UpdatePlaceableButtonsColor(moneySystem.bombCost, bombCostText);

        UpdatePlaceableButtonsColor(moneySystem.slowCost, slowCostText);
    }

    Data currentData;
    Button currentButton;
    void HandleButtonSelection2(Data data, Button button)
    {
        currentData = data;
        currentButton = button;
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

    void Baban()
    {
        foreach (var data in datas)
        {
            //UpdatePlaceableButtonsColor(data.cost,)
        }
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
        //switch (flagManager.currentMode)
        //{
        //    case FlagManager.CurrentMode.def:

        //        if (lastPressedButton != null)
        //            lastPressedButton.interactable = true;

        //        break;
        //    //case FlagManager.CurrentMode.ballista:
        //    //    ButtonSelectionLogic(ballistaButton);
        //    //    break;
        //    case FlagManager.CurrentMode.blaster:
        //        ButtonSelectionLogic(blasterButton);
        //        break;
        //    case FlagManager.CurrentMode.cannon:
        //        ButtonSelectionLogic(cannonButton);
        //        break;
        //    case FlagManager.CurrentMode.bomb:
        //        ButtonSelectionLogic(bombButton);
        //        break;
        //    case FlagManager.CurrentMode.slow:
        //        ButtonSelectionLogic(slowButton);
        //        break;
        //    case FlagManager.CurrentMode.upgrade:
        //        ButtonSelectionLogic(upgradeButton);
        //        break;
        //    case FlagManager.CurrentMode.demolish:
        //        ButtonSelectionLogic(demolishButton);
        //        break;

        //}
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
    public void UpdateRemainingEnemiesText(GameObject gameObject, Data data)
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
