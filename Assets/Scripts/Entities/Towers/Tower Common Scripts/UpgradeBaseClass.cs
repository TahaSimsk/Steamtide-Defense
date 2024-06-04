
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeBaseClass : MonoBehaviour, IPointerExitHandler, IPointerMoveHandler, IPointerEnterHandler,IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI upgradeButtonText;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] protected ObjectInfo towerInfo;

    protected TowerData towerData;

    protected int counter = 0;
    protected int maxUpgradeCount;
    protected float upgradeMoneyCost = 0;
    protected float upgradeWoodCost = 0;
    protected float upgradeRockCost = 0;
    public string upgradeName;
    Button button;
    string text;

    protected virtual void Awake()
    {
        
    }

    protected virtual void OnEnable()
    {
        towerData = towerInfo.InstTowerData;
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
            if (moneyManager.IsAffordable(upgradeMoneyCost, upgradeWoodCost, upgradeRockCost))
            {
                moneyManager.DecreaseMoney(upgradeMoneyCost);
                moneyManager.DecreaseWood(upgradeWoodCost);
                moneyManager.DecreaseRock(upgradeRockCost);
                DoUpgrade();
                counter++;
                SetButtonText();
                SetTooltipText();
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



        upgradeButtonText.text = $"{upgradeName}";
    }

    void SetTooltipText()
    {
        if (counter == maxUpgradeCount)
        {
            TooltipManager.Instance.DisableTip();
            return;
        }

        text = "Cost: \n";

        if (upgradeMoneyCost != 0)
        {
            text += $"${upgradeMoneyCost}\n";
        }
        if (upgradeWoodCost != 0)
        {
            text += $"{upgradeWoodCost} Wood\n";
        }
        if (upgradeRockCost != 0)
        {
            text += $"{upgradeRockCost} Rock\n";
        }

      TooltipManager.Instance.ShowTip(text, Input.mousePosition, moneyManager.IsAffordable(upgradeMoneyCost, upgradeWoodCost, upgradeRockCost));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltipText();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        SetTooltipText();
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.DisableTip();
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        SetTooltipText();
    }
}



