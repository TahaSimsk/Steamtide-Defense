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
    public GameObject RangeIndicator;
    [Header("Demolish State Variables")]
    public LayerMask demolishLayer;
    public GameObject resourceFloatingText;

    [HideInInspector] public Button pressedButton;

    BaseState currentState;

    public BuildState buildState = new BuildState();
    public DemolishState demolishState = new DemolishState();
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

    #endregion

}
