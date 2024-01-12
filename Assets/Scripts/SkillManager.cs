using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] GameObject bombHoverPrefab;
    [SerializeField] GameObject bombPrefab;

    [SerializeField] float bombDamage;

    GameObject instantiatedBombHover;

    FlagManager flagManager;

    private void Awake()
    {
        flagManager = FindObjectOfType<FlagManager>();
    }


    private void Update()
    {
        CreateBombHover();

        SetBombHoverPos();

        DropBomb();
    }

    void CreateBombHover()
    {

        if (flagManager.bombMode && instantiatedBombHover == null)
        {
            instantiatedBombHover = Instantiate(bombHoverPrefab, Input.mousePosition, Quaternion.identity);

        }
        else if (!flagManager.bombMode && instantiatedBombHover != null)
        {
            DestroyBombHover();
        }
    }

    void SetBombHoverPos()
    {
        if (instantiatedBombHover == null || flagManager.hoverMode) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            instantiatedBombHover.transform.position = hit.transform.position + Vector3.up * 2;
            
        }
    }

    void DropBomb()
    {
        if (instantiatedBombHover == null || flagManager.hoverMode) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bombPrefab, instantiatedBombHover.transform.position, Quaternion.identity);
        }
    }

    public void DestroyBombHover()
    {
        if (instantiatedBombHover == null) { return; }
        Destroy(instantiatedBombHover);
    }

    public void ActivateBombHover(bool value)
    {
        if (instantiatedBombHover == null) { return; }
        instantiatedBombHover.SetActive(value);
    }
}
