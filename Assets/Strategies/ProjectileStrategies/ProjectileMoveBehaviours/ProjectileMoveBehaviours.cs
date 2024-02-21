using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class ProjectileMoveBehaviours : ScriptableObject
{
    public abstract void WhenEnabled();
    public abstract void Move(Transform transform, ref Transform target, float moveSpeed);
}
