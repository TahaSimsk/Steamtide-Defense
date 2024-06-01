using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public string NameOfResource;
    public float DropAmount { get; set; }
    public Vector2Int MinMaxDropAmount;
    public MoneyManager MoneyManager;
    public GameEvent1ParamSO onDropAmountUpgrade;


    protected virtual void Awake()
    {
        DropAmount = Random.Range(MinMaxDropAmount.x, MinMaxDropAmount.y);
    }

    protected virtual void OnEnable()
    {
        onDropAmountUpgrade.onEventRaised += UpgradeDropAmount;
    }

    protected virtual void OnDisable()
    {
        onDropAmountUpgrade.onEventRaised -= UpgradeDropAmount;
    }


    public virtual void Drop()
    {

    }

    protected virtual void UpgradeDropAmount(object fl)
    {
        if (fl is float _amount)
        {
            DropAmount = HelperFunctions.CalculatePercentage(DropAmount, _amount);
        }
    }

}