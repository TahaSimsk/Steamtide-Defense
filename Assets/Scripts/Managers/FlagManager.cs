using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
    public enum CurrentMode
    {
        def,
        ballista,
        blaster,
        cannon,
        bomb,
        slow,
        upgrade,
        demolish,
        hover
    };
    public CurrentMode currentMode;


    public delegate void OnStateChanged();
    public static event OnStateChanged onStateChanged;


    public void ChangeCurrentMode(int anan)
    {
        currentMode = (CurrentMode)anan;

        onStateChanged?.Invoke();

    }



    //[HideInInspector] public bool ballistaMode;
    //[HideInInspector] public bool blasterMode;
    //[HideInInspector] public bool cannonMode;
    //[HideInInspector] public bool bombMode;
    //[HideInInspector] public bool slowMode;
    //[HideInInspector] public bool upgradeMode;
    //[HideInInspector] public bool demolishMode;
    [HideInInspector] public bool hoverMode;


    //public void SetBallistaMode(bool value)
    //{
    //    ballistaMode = value;
    //}

    //public void SetBlasterMode(bool value)
    //{
    //    blasterMode = value;
    //}

    //public void SetCannonMode(bool value)
    //{
    //    cannonMode = value;
    //}

    //public void SetBombMode(bool value)
    //{
    //    bombMode = value;
    //}

    //public void SetSlowMode(bool value)
    //{
    //    slowMode = value;
    //}

    //public void SetUpgradeMode(bool value)
    //{
    //    upgradeMode = value;
    //}

    //public void SetDemolishMode(bool value)
    //{
    //    demolishMode = value;
    //}

    //public void ClearMode()
    //{
    //    ballistaMode = false;
    //    blasterMode = false;
    //    cannonMode = false;
    //    bombMode = false;
    //    slowMode = false;
    //    upgradeMode = false;
    //    demolishMode = false;
    //}
}
