
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

    protected TowerData towerData;
    protected ProjectileData iProjectile;

    protected int counter = 0;
    protected int maxUpgradeCount;
    protected float upgradeCost;
    protected float nextUpgradeCost;
    public string upgradeName;
    Button button;

    
    protected virtual void OnEnable()
    {
        towerData = towerInfo.InstITower;
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
        if (counter < maxUpgradeCount)
        {
            if (moneyManager.IsPlaceable(upgradeCost))
            {
                moneyManager.DecreaseMoney(upgradeCost);
                DoUpgrade();
                counter++;
                SetButtonText();
            }
            else
            {
                HandleUnaffordableUpgrade();
            }
        }
        if (counter >= maxUpgradeCount)
        {
            button.interactable = false;
        }
    }

    protected virtual void DoUpgrade()
    {

    }

    protected virtual void HandleUnaffordableUpgrade()
    {
        Debug.Log("Can't buy " + upgradeName);
    }

    void SetButtonText()
    {
        if (counter == maxUpgradeCount)
        {
            upgradeButtonText.text = upgradeName;
            return;
        }
        upgradeButtonText.text = upgradeName + " $" + upgradeCost;
    }



}



