using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onButtonPressed;
    [SerializeField] GameEvent0ParamSO onESCPressed;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        DisableButton();
    }


    private void OnEnable()
    {
        button.onClick.AddListener(InvokeCancelEvent);
        onButtonPressed.onEventRaised += ShowButton;
        onESCPressed.onEventRaised += DisableButton;
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeCancelEvent);
        onButtonPressed.onEventRaised -= ShowButton;
        onESCPressed.onEventRaised -= DisableButton;
    }
    void ShowButton(object button)
    {
        if (button is Button)
        {

            if (this.button.interactable == false)
            {
                this.button.interactable = true;
                this.button.image.enabled = true;

                foreach (Transform child in transform)
                    child.gameObject.SetActive(true);

            }
        }
    }

    void InvokeCancelEvent()
    {
        onESCPressed.RaiseEvent();
    }

    void DisableButton()
    {
        if (button.interactable == true)
        {
            button.interactable = false;
            button.image.enabled = false;

            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }
}
