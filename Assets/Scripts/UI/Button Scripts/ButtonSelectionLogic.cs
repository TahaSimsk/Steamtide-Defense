using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectionLogic : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();

    }
    private void OnEnable()
    {
        button.onClick.AddListener(InvokeButtonClickEvent);
        EventManager.onButtonPressed += CheckWhichButtonPressed;
        EventManager.onESCPressed += ClearButtonSelection;
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeButtonClickEvent);
        EventManager.onButtonPressed -= CheckWhichButtonPressed;
        EventManager.onESCPressed -= ClearButtonSelection;
    }

    void InvokeButtonClickEvent()
    {
        EventManager.OnButtonPressed(button);
    }

    void CheckWhichButtonPressed(Button button)
    {
        if (button == this.button)
        {
            this.button.interactable = false;
        }
        else if (button != this.button && this.button.interactable == false)
        {
            this.button.interactable = true;
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
