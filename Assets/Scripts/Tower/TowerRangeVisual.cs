using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeVisual : MonoBehaviour
{
    [SerializeField] TargetScanner targetScanner;

  
    private void OnMouseEnter()
    {
        targetScanner.ToggleRangeVisual();
    }

    private void OnMouseExit()
    {
        targetScanner.ToggleRangeVisual();
    }
}
