using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected GameEvent1ParamSO onEnemyReachEndOfPath;
    [SerializeField] protected GameEvent1ParamSO onEnemyReachEndOfBasePath;
    [SerializeField] ObjectInfo objectInfo;
    [SerializeField] protected Vector3 offsetY;

    [HideInInspector] public float CurrentMoveSpeed;
    protected float timerToNormaliseMoveSpeed;
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
        StartCoroutine(MoveAlongPath(path, false));
        CurrentMoveSpeed = enemyData.DefaultMoveSpeed;
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
        transform.LookAt(path[1].transform);
    }


    public virtual IEnumerator MoveAlongPath(List<GameObject> _path, bool _isBasePath)
    {
        for (int i = 0; i < _path.Count; i++)
        {
            Vector3 nextPathPos;
            if (_isBasePath)
            {
                nextPathPos = _path[i].transform.position;
                if (!gameObject.activeInHierarchy)
                {
                    yield break;
                }
                StartCoroutine(FaceWaypoint(nextPathPos));
            }
            else
            {
                if (i + 1 == _path.Count)
                {
                    nextPathPos = _path[i].transform.position;
                }
                else
                {
                    nextPathPos = _path[i + 1].transform.position;
                    if (!gameObject.activeInHierarchy)
                    {
                        yield break;
                    }
                    StartCoroutine(FaceWaypoint(nextPathPos));

                }
            }


            //if (nextPathPos == null)
            //{
            //    break;
            //}
            //if (!gameObject.activeInHierarchy)
            //{
            //    yield break;
            //}
            while (transform.position != nextPathPos + offsetY)
            {
                if (!gameObject.activeInHierarchy)
                {
                    yield break;
                }
                transform.position = Vector3.MoveTowards(transform.position, nextPathPos + offsetY, CurrentMoveSpeed * Time.deltaTime);

                yield return null;
            }
        }
        //reaching the end of the path
        if (_isBasePath)
        {
            onEnemyReachEndOfBasePath.RaiseEvent(gameObject);
        }
        else
        {
            onEnemyReachEndOfPath.RaiseEvent(gameObject);

        }
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
        CurrentMoveSpeed = enemyData.DefaultMoveSpeed - CurrentMoveSpeed * value / 100;
    }
    public void DecreaseMoveSpeedByPercentage(float value, float time)
    {
        if (stunned) return;
        CurrentMoveSpeed = enemyData.DefaultMoveSpeed - enemyData.DefaultMoveSpeed * value / 100;
        if (CurrentMoveSpeed <= 0.01f)
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

                CurrentMoveSpeed = enemyData.DefaultMoveSpeed;
                stunned = false;
            }
        }
    }
}
