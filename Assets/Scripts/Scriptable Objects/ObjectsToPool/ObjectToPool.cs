using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectToPool", menuName = "ObjectToPool")]
public class ObjectToPool : ScriptableObject
{
    public GameObject objPrefab;
    public int objPrefabPoolSize;



    
    [HideInInspector]   
    public List<GameObject> objList = new List<GameObject>();
}
