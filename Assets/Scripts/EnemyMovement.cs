using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform pathParent;

    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offsetY;

    List<GameObject> path = new List<GameObject>();


    private void Start()
    {
        GetPathFromParent();
        SnapEnemyToStart();
        StartCoroutine(MoveAlongPath());
    }


    void GetPathFromParent()
    {
        foreach (Transform child in pathParent)
        {
            path.Add(child.gameObject);
        }
    }

    void SnapEnemyToStart()
    {
        transform.position = path[0].transform.position + offsetY;
    }


    IEnumerator MoveAlongPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            while (transform.position != path[i].transform.position + offsetY)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position + offsetY, moveSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }

        gameObject.SetActive(false);
    }
}
