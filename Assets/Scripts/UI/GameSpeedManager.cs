using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManager : MonoBehaviour
{
    [SerializeField] Button decreaseButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button increaseButton;

    float previousValue = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 0;
            SavePreviousValue();
            HandleButtonState();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1.0f;
            SavePreviousValue();
            HandleButtonState();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2f;
            SavePreviousValue();
            HandleButtonState();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 3f;
            SavePreviousValue();
            HandleButtonState();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 4;
            SavePreviousValue();
            HandleButtonState();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseUnpauseGame();
        }
    }

    public void DecreaseGameSpeed()
    {

        if (previousValue - 1 < 0)
        {
            Time.timeScale = 0;
            HandleButtonState();
            return;
        }
        Time.timeScale = previousValue - 1;
        SavePreviousValue();
        HandleButtonState();
    }

    public void IncreaseGameSpeed()
    {
        if (previousValue + 1 > 4)
        {
            Time.timeScale = 4f;
            HandleButtonState();
            return;
        }
        Time.timeScale = previousValue + 1;

        SavePreviousValue();
        HandleButtonState();
    }

    public void PauseUnpauseGame()
    {
        if (Time.timeScale == 0)
        {
            if (previousValue == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = previousValue;
            }
            HandleButtonState();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    void SavePreviousValue()
    {
        previousValue = Time.timeScale;
    }

    void HandleButtonState()
    {
        switch (Time.timeScale)
        {
            case 0:
                CheckButtonState();
                decreaseButton.interactable = false;
                break;

            case 1:
                CheckButtonState();
                break;

            case 2:
                CheckButtonState();
                break;

            case 3:
                CheckButtonState();
                break;

            case 4:
                CheckButtonState();
                increaseButton.interactable = false;
                break;
        }
    }

    void CheckButtonState()
    {
        if (decreaseButton.interactable == false)
        {
            decreaseButton.interactable = true;
        }
        if (increaseButton.interactable == false)
        {
            increaseButton.interactable = true;
        }
    }

}
