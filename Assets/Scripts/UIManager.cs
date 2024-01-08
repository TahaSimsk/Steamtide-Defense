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

    [Header("Upgrade Related Text UI")]
    [SerializeField] Button upgradeModeButton;
    [SerializeField] Button cancelUpgradeButton;

    [SerializeField] GameObject bombPrefab;

    #region Flags

    //--------------- WEAPON TYPE FLAGS ---------------
    [HideInInspector] public bool ballista;
    [HideInInspector] public bool blaster;
    [HideInInspector] public bool cannon;
    [HideInInspector] public bool upgradeMode;

    #endregion


    EnemyWaveController waveController;
    SkillManager skillManager;

    //------------------ COUNTDOWN ------------------
    bool startCountdownForNextWave;
    float timerForNextWave;

    private void Awake()
    {
        waveController = FindObjectOfType<EnemyWaveController>();
        skillManager = FindObjectOfType<SkillManager>();
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





    public void Ballista(bool yes)
    {
        this.ballista = yes;
    }
    public void Blaster(bool yes)
    {
        this.blaster = yes;
    }
    public void Cannon(bool yes)
    {
        this.cannon = yes;
    }


    public void UpgradeMode(bool on)
    {
        if (on)
        {
            upgradeModeButton.gameObject.SetActive(false);
            cancelUpgradeButton.gameObject.SetActive(true);
            upgradeMode = true;
        }
        else
        {
            upgradeModeButton.gameObject.SetActive(true);
            cancelUpgradeButton.gameObject.SetActive(false);
            upgradeMode = false;
        }
    }


    public List<bool> ReturnFlags()
    {
        List<bool> flags = new List<bool>();
        flags.Add(this.ballista);
        flags.Add(this.blaster);
        flags.Add(this.cannon);
        return flags;
    }

    public void SetFlags(bool ballista, bool blaster, bool cannon)
    {
        this.ballista = ballista;
        this.blaster = blaster;
        this.cannon = cannon;
    }

    public void CreateBomb()
    {
        Instantiate(bombPrefab, Input.mousePosition, Quaternion.identity);
    }

    public void ClearFlags()
    {
        ballista = false;
        blaster = false;
        cannon = false;
        skillManager.DestroyBomb();
        //upgradeMode = false;
        //upgradeModeButton.gameObject.SetActive(true);
        //cancelUpgradeButton.gameObject.SetActive(false);
    }
}
