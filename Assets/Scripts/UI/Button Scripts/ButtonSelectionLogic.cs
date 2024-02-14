using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectionLogic : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onButtonPressed;
    [SerializeField] GameEvent0ParamSO onESCPressed;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();

    }
    private void OnEnable()
    {
        button.onClick.AddListener(InvokeButtonClickEvent);
        onButtonPressed.onEventRaised += CheckWhichButtonPressed;
        onESCPressed.onEventRaised += ClearButtonSelection;
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeButtonClickEvent);
        onButtonPressed.onEventRaised -= CheckWhichButtonPressed;
        onESCPressed.onEventRaised -= ClearButtonSelection;
    }

    void InvokeButtonClickEvent()
    {
        onButtonPressed.RaiseEvent(button);
    }

    void CheckWhichButtonPressed(object button)
    {
        if (button is Button)
        {
            if ((Button)button == this.button)
            {
                this.button.interactable = false;
            }
            else if ((Button)button != this.button && this.button.interactable == false)
            {
                this.button.interactable = true;
            } 
        }
    }

    void ClearButtonSelection()
    {
        if (button.interactable == false)
        {
            button.interactable = true;
        }
    }
}
