using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offsetY;

    List<GameObject> path = new List<GameObject>();


    Transform pathParent;

    private void Awake()
    {
        GetPathFromParent();
        SnapEnemyToStart();
    }

    private void OnEnable()
    {
        
        SnapEnemyToStart();
        StartCoroutine(MoveAlongPath());
    }


    void GetPathFromParent()
    {
        pathParent = GameObject.FindWithTag("Path").transform;
        foreach (Transform child in pathParent)
        {
            path.Add(child.gameObject);
        }
    }

    void SnapEnemyToStart()
    {
        transform.position = path[0].transform.position + offsetY;
        Debug.Log("snapped");
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
        //reaching at the end of the path
        gameObject.SetActive(false);
    }
}
