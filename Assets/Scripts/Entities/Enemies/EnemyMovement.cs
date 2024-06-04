using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(MoveAlongPath(path));
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
        //transform.LookAt(path[1].transform);
    }


    public virtual IEnumerator MoveAlongPath(List<GameObject> _path)
    {
        for (int i = 0; i < _path.Count; i++)
        {
            Vector3 nextPathPos = _path[i].transform.position;
            if (!gameObject.activeInHierarchy)
            {
                yield break;
            }
            StartCoroutine(FaceWaypoint(nextPathPos));
            while (transform.position != nextPathPos + offsetY)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPathPos + offsetY, currentMoveSpeed * Time.deltaTime);

                yield return null;
            }
        }
        //reaching the end of the path
        onEnemyReachEndOfPath.RaiseEvent(gameObject);
        //gameObject.SetActive(false);
    }

    public IEnumerator FaceWaypoint(Vector3 pos)
    {
        float timer = 0;
        while (timer < 5 / enemyData.DefaultMoveSpeed)
        {
            
            timer += Time.deltaTime;
            HelperFunctions.LookAtTarget(pos, transform, enemyData.DefaultMoveSpeed);
            yield return null;
        }
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
