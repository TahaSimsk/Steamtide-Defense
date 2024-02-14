
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class UpgradeBaseClass : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeButtonText;
    [SerializeField] MoneyManager moneyManager;

    [SerializeField] protected TowerInfo towerInfo;

    protected ITower iTower;
    protected IProjectile iProjectile;

    protected int counter = 0;
    protected int maxUpgrateCount;
    protected float upgradeCost;
    public string upgradeName;
    Button button;

    
    protected virtual void OnEnable()
    {
        iTower = towerInfo.InstITower;
        iProjectile = towerInfo.InstIProjectile;
    }



    protected virtual void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Upgrade);
        SetButtonText();
    }
    
    private void OnDestroy()
    {
        button.onClick.RemoveListener(Upgrade);
    }

    public void Upgrade()
    {
        if (counter < maxUpgrateCount)
        {
            if (moneyManager.IsPlaceable(upgradeCost))
            {
                DoUpgrade();
                moneyManager.DecreaseMoney(upgradeCost);
                counter++;
                SetButtonText();
            }
            else
            {
                Debug.Log("Can't buy " + upgradeName);
            }
        }
        if (counter >= maxUpgrateCount)
        {
            button.interactable = false;
        }
    }

    protected virtual void DoUpgrade()
    {

    }

    void SetButtonText()
    {
        if (counter == maxUpgrateCount)
        {
            upgradeButtonText.text = upgradeName;
            return;
        }
        upgradeButtonText.text = upgradeName + " $" + upgradeCost;
    }



}



