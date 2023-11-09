using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    public bool ballista;
    public bool blaster;
    public bool cannon;


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

}
