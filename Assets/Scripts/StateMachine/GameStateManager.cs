using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Header("Events")]
    public GameEvent2ParamSO onTowerPlaced;
    public GameEvent1ParamSO onDemolished;
    [SerializeField] GameEvent0ParamSO onESCPressed;



    [Header("Required Components")]
    public MoneyManager moneyManager;
    public CursorManager cursorManager;
    public ObjectPool objectPool;
    public Camera MainCamera;

    [Header("Build State Variables")]
    public LayerMask placeableLayer;
    public LayerMask ignoreLayers;
    public Vector3 offsetForTowerPlacement;

    [Header("Demolish State Variables")]
    public LayerMask demolishLayer;
    public ObjectToPool resourceFloatingText;

    [HideInInspector] public bool isHoveringUI;
    [HideInInspector] public Button pressedButton;

    BaseState currentState;

    public BuildState buildState = new BuildState();
    public DemolishState demolishState = new DemolishState();
    public SkillState skillState = new SkillState();
    public EmptyState emptyState = new EmptyState();

    private void OnEnable()
    {
        onESCPressed.onEventRaised += () => SwitchState(emptyState);
    }
    private void OnDisable()
    {
        onESCPressed.onEventRaised -= () => SwitchState(emptyState);

    }

    private void Start()
    {
        currentState = emptyState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 3f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 4;
        }
    }


    public void SwitchState(BaseState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState(this);
    }




    #region Methods to switch state for buttons
    public void EnterBuildState(Button button)
    {
        pressedButton = button;
        SwitchState(buildState);
    }

    public void EnterDemolishState(Button button)
    {
        pressedButton = button;
        SwitchState(demolishState);
    }


    public void EnterSkillState(Button button)
    {
        pressedButton = button;
        SwitchState(skillState);
    }

    #endregion

}
