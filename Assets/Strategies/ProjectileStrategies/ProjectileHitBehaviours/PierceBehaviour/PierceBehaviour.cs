using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileHitBehaviours/PierceBehaviour")]

public class PierceBehaviour : ProjectileHitBehaviours
{
    [field: SerializeReference] public override float UpgradeCost { get; set; }
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
        Debug.Log("pierce count: " + pierceCount);
        if (pierceCount > pierceLimit)
        {
            Debug.Log("deactivated " + pierceCount);
            transform.gameObject.SetActive(false);
            return;
        }
        currentDamage = baseDamage - (baseDamage * damagePerHitFallout[pierceCount - 1] * 0.01f);


    }


}
