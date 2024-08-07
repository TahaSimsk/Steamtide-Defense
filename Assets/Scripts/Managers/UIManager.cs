using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject winCanvas;

    [Header("Events")]
    [SerializeField] GameEvent0ParamSO onESCPressed;
    [SerializeField] GameEvent1ParamSO onBaseDeath;
    [SerializeField] GameEvent0ParamSO onLevelComplete;

    void Update()
    {
        HandleESCPressed();

        if (Input.GetKeyDown(KeyCode.V))
        {
            HandleGameOver(null);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HandleWin();
        }
    }

    private void OnEnable()
    {
        onBaseDeath.onEventRaised += HandleGameOver;
        onLevelComplete.onEventRaised += HandleWin;

    }
    private void OnDisable()
    {
        onBaseDeath.onEventRaised -= HandleGameOver;
        onLevelComplete.onEventRaised -= HandleWin;
    }

    void HandleGameOver(object sender)
    {
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    void HandleWin()
    {
        inGameCanvas.SetActive(false);
        winCanvas.SetActive(true);
        Time.timeScale = 0f;
    }



    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    void HandleESCPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onESCPressed.RaiseEvent();
        }
    }

}
