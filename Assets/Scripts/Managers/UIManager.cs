using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] GameEvent0ParamSO onESCPressed;
   
    void Update()
    {
        HandleESCPressed();
    }


    void HandleESCPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onESCPressed.RaiseEvent();
        }
    }

}
