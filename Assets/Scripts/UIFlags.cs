using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlags : MonoBehaviour
{
    public bool ballista;
    public bool blaster;
    public bool cannon;

    public bool upgradeMode;


    public void Ballista(bool yes)
    {
        this.ballista = yes;
    }
    public void Blaster(bool yes)
    {
        this.blaster = yes;
    }
    public void Cannon(bool yes)
    {
        this.cannon = yes;
    }


    public void UpgradeMode(bool yes)
    {
        this.upgradeMode = yes;
    }

    public List<bool> ReturnFlags()
    {
        List<bool> flags = new List<bool>();
        flags.Add(this.ballista);
        flags.Add(this.blaster);
        flags.Add(this.cannon);
        return flags;
    }

    public void SetFlags(bool ballista,  bool blaster, bool cannon)
    {
        this.ballista = ballista;
        this.blaster = blaster;
        this.cannon = cannon;
    }
}
