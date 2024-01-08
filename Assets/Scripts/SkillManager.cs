using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;

    [HideInInspector] public GameObject instantiatedBomb;

    public void CreateBomb()
    {
        instantiatedBomb = Instantiate(bombPrefab, Input.mousePosition, Quaternion.identity);
    }

    public void DestroyBomb()
    {
        if (instantiatedBomb == null) { return; }
        Destroy(instantiatedBomb);
    }

    public void ActivateBomb(bool value)
    {
        if (instantiatedBomb == null) { return; }
        instantiatedBomb.SetActive(value);
    }
}
