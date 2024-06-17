using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIManager : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onESCPressed;
    [SerializeField] GameObject upgradeUI;

    private void OnEnable()
    {
        onESCPressed.onEventRaised += DeactivateUI;
    }
    private void OnDisable()
    {
        onESCPressed.onEventRaised -= DeactivateUI;
    }

    private void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        ActivateUI();
    }

    void ActivateUI()
    {
        upgradeUI.SetActive(true);
    }
    void DeactivateUI()
    {
        upgradeUI.SetActive(false);
    }
}
