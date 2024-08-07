using UnityEngine;
using UnityEngine.EventSystems;

public class TowerRangeVisual : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onEscPressed;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] GameObject UI_2D;

    MeshRenderer towerRangeMesh;

    private void Awake()
    {
        if (targetScanner != null)
            towerRangeMesh = targetScanner.GetComponent<MeshRenderer>();
    }


    private void OnEnable()
    {
        onEscPressed.onEventRaised += DeactivateUpgradeUI;
    }

    private void OnDisable()
    {
        onEscPressed.onEventRaised += DeactivateUpgradeUI;
    }

    
    private void OnMouseExit()
    {
        HideRangeMesh();
    }

    private void HideRangeMesh()
    {
        if (towerRangeMesh == null || towerRangeMesh.enabled == false) return;
        towerRangeMesh.enabled = false;
    }

    private void OnMouseOver()
    {
        ShowRangeMesh();
    }

    public void ShowRangeMesh()
    {
        if (EventSystem.current.IsPointerOverGameObject() || towerRangeMesh == null || towerRangeMesh.enabled == true) return;
        towerRangeMesh.enabled = true;
    }

    private void OnMouseDown()
    {
        if (UI_2D != null && !EventSystem.current.IsPointerOverGameObject())
        {
            UI_2D.SetActive(true);
            if (towerRangeMesh != null)
                towerRangeMesh.enabled = false;
        }
    }



    void DeactivateUpgradeUI()
    {
        if (towerRangeMesh != null)
            towerRangeMesh.enabled = false;
        if (UI_2D == null) return;
        UI_2D.SetActive(false);

    }
}
