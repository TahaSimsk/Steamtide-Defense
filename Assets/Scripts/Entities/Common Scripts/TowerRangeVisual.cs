using UnityEngine;
using UnityEngine.EventSystems;

public class TowerRangeVisual : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onEscPressed;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] GameObject upgradeUI;

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
        if (towerRangeMesh == null || towerRangeMesh.enabled == false) return;
        towerRangeMesh.enabled = false;
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject() || towerRangeMesh == null || towerRangeMesh.enabled == true) return;
        towerRangeMesh.enabled = true;
    }

    private void OnMouseDown()
    {
        if (upgradeUI != null && !EventSystem.current.IsPointerOverGameObject())
        {
            upgradeUI.SetActive(true);
            if (towerRangeMesh != null)
                towerRangeMesh.enabled = false;
        }
    }



    void DeactivateUpgradeUI()
    {
        if (towerRangeMesh != null)
            towerRangeMesh.enabled = false;
        if (upgradeUI == null) return;
        upgradeUI.SetActive(false);

    }
}
