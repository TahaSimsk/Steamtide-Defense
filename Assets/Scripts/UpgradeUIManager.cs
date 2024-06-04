using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onESCPressed;
    [SerializeField] GameObject upgradeUI;
    // Start is called before the first frame update
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
