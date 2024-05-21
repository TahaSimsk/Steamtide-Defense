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

public interface IPlayerDamageable
{
    void GetDamage(float damage);
}

public interface IResource
{
    public int Amount { get; set; }
    public void Drop();
        
}