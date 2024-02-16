using System.Collections;
using System.Data;
using UnityEngine;

public abstract class ProjectileHitBehaviours : ScriptableObject
{
    public abstract string NameOfType { get; set; }
    public abstract void WhenEnabled();
    public abstract void Collide(Transform transform, Collider collider, ref float currentDamage, float baseDamage);
}
