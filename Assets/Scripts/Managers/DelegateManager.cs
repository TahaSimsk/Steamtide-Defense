using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateManager : MonoBehaviour
{
    public delegate void OnESCPressed();
    public static OnESCPressed onESCPressed;

    public delegate void OnMouseOverDemolish();
    public static OnMouseOverDemolish onMouseOverDemolish;

    public delegate void OnMouseOverUpgrade();
    public static OnESCPressed onMouseOverUpgrade;
}
