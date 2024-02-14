using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Float List")]
public class FloatListSO : ScriptableObject
{
    public string upgradeName;

    public List<float> floats = new List<float>();
    
}
