using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [HideInInspector] public bool isHoveringUI;
    [HideInInspector] public Button pressedButton;
    public MoneyManager moneyManager;
    public MoneySystem moneySystem;

    [Header("Build State Variables")]
    public LayerMask placeableLayer;
    public Vector3 offsetForTowerPlacement;

    [Header("Demolish State Variables")]
    public GameObject tile;
    public LayerMask demolishLayer;
    public CursorManager cursorManager;
    public TooltipManager tooltipManager;

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
        EventManager.onESCPressed += () => SwitchState(emptyState);
    }
    private void OnDisable()
    {
        EventManager.onESCPressed -= () => SwitchState(emptyState);

    }

    private void Start()
    {
        currentState = emptyState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);

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

    void ButtonSelection(Button button)
    {
        //if (pressedButton != null)
        //{
        //    pressedButton.interactable = true;
        //}

        //pressedButton = button;
        //pressedButton.interactable = false;
    }

    #endregion

}
