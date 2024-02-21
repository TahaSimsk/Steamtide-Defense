using UnityEngine;

[CreateAssetMenu(menuName = "Projectile Move Behaviours/Homing Move Behaviour")]
public class HomingMoveBehaviour : ProjectileMoveBehaviours
{
    Vector3 targetPos;

    public override void Move(Transform transform, ref Transform target, float moveSpeed)
    {
        if (target != null)
        {
            targetPos = target.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        if ((transform.position - targetPos).sqrMagnitude < 0.1f)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public override void WhenEnabled()
    {
        
    }
}
