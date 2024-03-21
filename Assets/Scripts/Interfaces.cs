using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    GameObject ObjectPrefab { get; set; }
    int ObjectPoolsize { get; set; }
    List<GameObject> objList { get; set; }

    GameObject GetObject();
}




