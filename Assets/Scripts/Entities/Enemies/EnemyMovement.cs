using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected GameEvent1ParamSO onEnemyReachEndOfPath;
    [SerializeField] ObjectInfo objectInfo;
    [SerializeField] protected Vector3 offsetY;

    protected float currentMoveSpeed, timerToNormaliseMoveSpeed;
    bool stunned;

    protected List<GameObject> path = new List<GameObject>();

   protected EnemyData enemyData;

    private void Awake()
    {
        GetPathFromParent();
        enemyData = objectInfo.DefObjectGameData as EnemyData;
    }


    private void OnEnable()
    {
        SnapEnemyToStart();
        StartCoroutine(MoveAlongPath());
        currentMoveSpeed = enemyData.DefaultMoveSpeed;
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


    protected virtual IEnumerator MoveAlongPath()
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
        currentMoveSpeed = enemyData.DefaultMoveSpeed - currentMoveSpeed * value / 100;
    }
    public void DecreaseMoveSpeedByPercentage(float value, float time)
    {
        if (stunned) return;
        currentMoveSpeed = enemyData.DefaultMoveSpeed - enemyData.DefaultMoveSpeed * value / 100;
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
                currentMoveSpeed = enemyData.DefaultMoveSpeed;
                stunned = false;
            }
        }
    }
}
