using System;
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
    [HideInInspector] public bool hoverMode;

    public delegate void OnStateChanged();
    public static event OnStateChanged onStateChanged;


    public void ChangeCurrentMode(int anan)
    {
        currentMode = (CurrentMode)anan;

        onStateChanged?.Invoke();

    }
    Button button;
    public void GetButton(Button button)
    {
        this.button = button;
    }
    Data data;
    public void ChangeAnan(Data data)
    {

        this.data = data;
    }

    public void ShareDataAndButton()
    {
        //onButtonPressed?.Invoke(data, button);
        EventManager.OnButtonPressed(data, button);
    }







}
