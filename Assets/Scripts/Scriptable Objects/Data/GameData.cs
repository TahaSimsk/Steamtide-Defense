using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameData : ScriptableObject
{
    public string objectName;
    public int hashCode;

    private void OnValidate()
    {
        hashCode = objectName.GetHashCode();
    }
}
