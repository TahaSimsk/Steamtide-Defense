using UnityEngine;

[CreateAssetMenu(menuName = "Projectile Move Behaviours/Front Move Behaviour")]
public class FrontMoveBehaviour : ProjectileMoveBehaviours
{
    float timer;

    public override void WhenEnabled()
    {
        timer = 0;
    }

    public override void Move(Transform transform, ref Transform target, float moveSpeed)
    {
        if (target != null)
            transform.LookAt(target.position + Vector3.up * 4);
        target = null;
        timer += Time.deltaTime;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (timer >= 1f)
        {
            transform.gameObject.SetActive(false);
        }
    }
}