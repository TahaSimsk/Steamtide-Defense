using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DemolishInfo : MonoBehaviour
{
    enum ObjectType { oneTree, doubleTree, quadTree }

    [SerializeField] ObjectType objectType;

    //[SerializeField] bool oneTree;
    //[SerializeField] bool doubleTree;
    //[SerializeField] bool quadTree;

    public float moneyCost;


    private void Awake()
    {
        MoneySystem moneySystem = FindObjectOfType<MoneySystem>();

        switch (objectType)
        {
            case ObjectType.oneTree:
                moneyCost = moneySystem.oneTreeDemolishCost;
                break;
            case ObjectType.doubleTree:
                moneyCost = moneySystem.doubleTreeDemolishCost;
                break;
            case ObjectType.quadTree:
                moneyCost = moneySystem.quadTreeDemolishCost;
                break;
        }

        //if (oneTree)
        //{
        //    moneyCost = moneySystem.oneTreeDemolishCost;
        //}
        //else if (doubleTree)
        //{
        //    moneyCost = moneySystem.doubleTreeDemolishCost;
        //}
        //else if (quadTree)
        //{
        //    moneyCost = moneySystem.quadTreeDemolishCost;
        //}


    }
}
