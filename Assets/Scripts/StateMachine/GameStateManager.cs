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

    [HideInInspector] public bool isHoveringUI;
    [HideInInspector] public Button pressedButton;

    [Header("Required Components")]
    public MoneyManager moneyManager;
    public CursorManager cursorManager;
    public TooltipManager tooltipManager;

    [Header("Build State Variables")]
    public LayerMask placeableLayer;
    public LayerMask ignoreLayers;
    public Vector3 offsetForTowerPlacement;

    [Header("Demolish State Variables")]
    public GameObject tile;
    public LayerMask demolishLayer;

    [Header("Upgrade State Variables")]
    public LayerMask upgradeLayer;

    BaseState currentState;

    public BuildState buildState = new BuildState();
    public DemolishState demolishState = new DemolishState();
    public UpgradeState upgradeState = new UpgradeState();
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
    public void EnterUpgradeState(Button button)
    {
        pressedButton = button;
        SwitchState(upgradeState);
    }


    public void EnterSkillState(Button button)
    {
        pressedButton = button;
        SwitchState(skillState);
    }

    #endregion

}
