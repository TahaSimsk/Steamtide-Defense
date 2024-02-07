using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        DisableButton();
    }


    private void OnEnable()
    {
        button.onClick.AddListener(InvokeCancelEvent);
        EventManager.onButtonPressed += ShowButton;
        EventManager.onESCPressed += DisableButton;
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeCancelEvent);
        EventManager.onButtonPressed -= ShowButton;
        EventManager.onESCPressed -= DisableButton;
    }
    void ShowButton(Button button)
    {
        if (this.button.interactable == false)
        {
            this.button.interactable = true;
           this.button.image.enabled = true;

            foreach (Transform child in transform)
                child.gameObject.SetActive(true);

        }
    }

    void InvokeCancelEvent()
    {
        EventManager.OnESCPressed();
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
