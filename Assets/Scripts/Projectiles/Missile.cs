using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public GameObject targetIndicator;
    float timer;
    [HideInInspector]
    public CannonData cannonData;

    private void Start()
    {
        StartCoroutine(LaunchMissile());
    }

    Vector3 RandomizeLocation(Vector3 value)
    {
        value.x = Random.Range(value.x - 5f, value.x + 5f);
        value.y = Random.Range(value.y - 5f, value.y + 5f);
        value.z = Random.Range(value.z - 5f, value.z + 5f);
        return value;
    }

    IEnumerator LaunchMissile()
    {
        while (timer < cannonData.initialTravelTimeOfMissile)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime * cannonData.missileMoveSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(cannonData.timeToWaitBeforeAppearing);
        transform.position = RandomizeLocation(targetPos) + Vector3.up * cannonData.offsetToAppear;
        transform.rotation = Quaternion.Euler(180, transform.rotation.y, transform.rotation.z);
        while (true)
        {
            transform.Translate(-transform.up * Time.deltaTime * cannonData.missileMoveSpeed * 1.5f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path2") || other.CompareTag("Ground"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, cannonData.bombRadius, cannonData.enemyLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
                    enemy.ReduceHealth(cannonData.damage);
                }
            }
            if (targetIndicator != null)
                Destroy(targetIndicator);
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, cannonData.bombRadius);
    }
}
