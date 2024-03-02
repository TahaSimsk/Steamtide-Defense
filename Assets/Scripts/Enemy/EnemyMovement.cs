using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onEnemyReachEndOfPath;

    [SerializeField] Vector3 offsetY;

    float currentMoveSpeed, defaultMoveSpeed, timerToNormaliseMoveSpeed;
    bool stunned;

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
        stunned = false;
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

                yield return null;
            }
        }
        //reaching at the end of the path
        onEnemyReachEndOfPath.RaiseEvent(gameObject);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        ResetMoveSpeed();
    }

    public void DecreaseMoveSpeedByPercentage(float value)
    {
        currentMoveSpeed = defaultMoveSpeed - currentMoveSpeed * value / 100;
    }
    public void DecreaseMoveSpeedByPercentage(float value, float time)
    {
        if (stunned) return;
        currentMoveSpeed = defaultMoveSpeed - defaultMoveSpeed * value / 100;
        if (currentMoveSpeed <= 0.01f)
        {
            stunned = true;
        }
        timerToNormaliseMoveSpeed = time;
    }


    public void ResetMoveSpeed()
    {
        if (timerToNormaliseMoveSpeed > 0)
        {
            timerToNormaliseMoveSpeed -= Time.deltaTime;
            if (timerToNormaliseMoveSpeed <= 0)
            {
                currentMoveSpeed = defaultMoveSpeed;
                stunned = false;
            }
        }
    }

}
