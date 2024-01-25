using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float defaultMoveSpeed;
    [HideInInspector]
    public float currentMoveSpeed;

    [SerializeField] Vector3 offsetY;

    List<GameObject> path = new List<GameObject>();

    UIManager uiManager;

    Transform pathParent;

    public bool reachedEnd;

    private void Awake()
    {
        GetPathFromParent();
        SnapEnemyToStart();
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }

    private void OnEnable()
    {

        SnapEnemyToStart();
        StartCoroutine(MoveAlongPath());
        reachedEnd = false;
        currentMoveSpeed = defaultMoveSpeed;
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
        uiManager.UpdateRemainingEnemiesText(false);
        reachedEnd = true;
        gameObject.SetActive(false);
    }

    public void ChangeMoveSpeed(float value)
    {
        currentMoveSpeed = defaultMoveSpeed - currentMoveSpeed * value / 100;
    }

    public void ResetMoveSpeed()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }
}
