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

    [Header("Wave Related Text UI")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI remainingEnemiesText;
    [SerializeField] TextMeshProUGUI nextWaveCountdownText;

    [Header("Buttons")]
    [SerializeField] Button ballistaButton;
    [SerializeField] Button blasterButton;
    [SerializeField] Button cannonButton;
    [SerializeField] Button bomb2Button;
    [SerializeField] Button upgradeButton;
    [SerializeField] Button xButton;


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

        ballistaCostText.text = "Ballista: $" + moneySystem.ballistaCost.ToString();
        blasterCostText.text = "Blaster: $" + moneySystem.blasterCost;
        cannonCostText.text = "Cannon: $" + moneySystem.cannonCost;
    }

    void Update()
    {
        UpdatePlaceableButtonsColor(moneySystem.ballistaCost, ballistaCostText);
        UpdatePlaceableButtonsColor(moneySystem.blasterCost, blasterCostText);
        UpdatePlaceableButtonsColor(moneySystem.cannonCost, cannonCostText);

        StartNextWaveCountdown();

        Anan();
    }



    void Anan()
    {
        Anan2(ballistaButton, flagManager.ballistaMode);
        Anan2(blasterButton, flagManager.blasterMode);
        Anan2(cannonButton, flagManager.cannonMode);
        Anan2(bomb2Button, flagManager.bombMode);
        Anan2(upgradeButton, flagManager.upgradeMode);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            xButton.gameObject.SetActive(false);
            flagManager.ClearMode();
        }
    }

    void Anan2(Button button, bool mode)
    {
        if (mode)
        {
            button.gameObject.SetActive(false);
            xButton.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(true);
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

    public void GetNextWaveTimer(bool shouldCount, float timer)
    {
        startCountdownForNextWave = shouldCount;
        timerForNextWave = timer;
    }

}
