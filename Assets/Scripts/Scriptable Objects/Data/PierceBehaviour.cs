using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileHitBehaviours/PierceBehaviour")]

public class PierceBehaviour : ProjectileHitBehaviours
{
    public float upgradeCost;
    public bool canPierce;
    public int pierceLimit;
    public List<float> damagePerHitFallout;
    int pierceCount;

    public override string NameOfType { get; set; } = "pierceHitBehaviour";
    public override void WhenEnabled()
    {
        pierceCount = 0;
    }

    public override void Collide(Transform transform, Collider collider, ref float currentDamage, float baseDamage)
    {
        pierceCount++;
        if (pierceCount > pierceLimit)
        {
            transform.gameObject.SetActive(false);
            return;
        }
        currentDamage = baseDamage - (baseDamage * damagePerHitFallout[pierceCount - 1] * 0.01f);


    }


}
