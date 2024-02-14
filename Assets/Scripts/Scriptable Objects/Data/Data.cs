using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public abstract class Data : ScriptableObject
{
   
    [Header("General Data Attributes")]
    public string objectName;

    public int hashCode;

    public GameObject objectPrefab;

    public int objectPrefabPoolSize;

    public float objectCost_MoneyDrop;

    [HideInInspector]
    public List<GameObject> objList = new List<GameObject>();



    private void OnValidate()
    {
        hashCode = objectName.GetHashCode();
    }

    public GameObject GetObject()
    {
        foreach (var obj in objList)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;

            }
        }
        return null;
    }

}
