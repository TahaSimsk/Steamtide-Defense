using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class Data : ScriptableObject
{
    public string objectName;

    public int hashCode;




    [Header("Object Prefab")]
    public GameObject objectPrefab;
    [Header("ObjectPooling")]
    public int objectPrefabPoolSize;

    [Header("Economy")]
    public float cost;

    [HideInInspector]
    public List<GameObject> objList = new List<GameObject>();



    private void OnValidate()
    {
        hashCode = objectName.GetHashCode();
    }

}
