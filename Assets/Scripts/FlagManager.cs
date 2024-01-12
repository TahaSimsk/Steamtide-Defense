using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
    [HideInInspector] public bool ballistaMode;
    [HideInInspector] public bool blasterMode;
    [HideInInspector] public bool cannonMode;
    [HideInInspector] public bool bombMode;
    [HideInInspector] public bool upgradeMode;
    [HideInInspector] public bool hoverMode;

    public void SetBallistaMode(bool value)
    {
        ballistaMode = value;
    }

    public void SetBlasterMode(bool value)
    {
        blasterMode = value;
    }

    public void SetCannonMode(bool value)
    {
        cannonMode = value;
    }

    public void SetBombMode(bool value)
    {
        bombMode = value;
    }

    public void SetUpgradeMode(bool value)
    {
        upgradeMode = value;
    }

    public void ClearMode()
    {
        ballistaMode = false;
        blasterMode = false;
        cannonMode = false;
        bombMode = false;
        upgradeMode = false;
    }
}
