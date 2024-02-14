using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onEnemyReachEndOfPath;

    [SerializeField] Vector3 offsetY;

    float currentMoveSpeed, defaultMoveSpeed;

    List<GameObject> path = new List<GameObject>();



    private void Awake()
    {
        GetPathFromParent();
        GameData enemyData = GetComponent<EnemyHealth>().enemyData;
        defaultMoveSpeed = ((IEnemy)enemyData).DefaultMoveSpeed;

    }


    private void OnEnable()
    {
        SnapEnemyToStart();
        StartCoroutine(MoveAlongPath());
        currentMoveSpeed = defaultMoveSpeed;
    }


    void GetPathFromParent()
    {
        Transform pathParent = GameObject.FindWithTag("Path").transform;
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
                transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position + offsetY, currentMoveSpeed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }
        //reaching at the end of the path
        onEnemyReachEndOfPath.RaiseEvent(gameObject);
        gameObject.SetActive(false);
    }

    public void DecreaseMoveSpeedByPercentage(float value)
    {
        currentMoveSpeed = defaultMoveSpeed - currentMoveSpeed * value / 100;
    }

    public void ResetMoveSpeed()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }
}
