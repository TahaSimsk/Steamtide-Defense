using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject weaponBallistaPrefab;
    [SerializeField] int weaponBallistaPoolSize;

    [SerializeField] GameObject weaponBallistaArrowPrefab;
    [SerializeField] int weaponBallistaArrowPoolSize;

    [SerializeField] GameObject weaponBallistaHoverPrefab;
    [SerializeField] int weaponBallistaHoverPoolSize;



    List<GameObject> weaponBallista = new List<GameObject>();
    List<GameObject> weaponBallistaArrow = new List<GameObject>();
    List<GameObject> weaponBallistaHover = new List<GameObject>();




    private void Awake()
    {
        PopulatePool(weaponBallista, weaponBallistaPoolSize, weaponBallistaPrefab);
        PopulatePool(weaponBallistaHover, weaponBallistaHoverPoolSize, weaponBallistaHoverPrefab);
        PopulatePool(weaponBallistaArrow, weaponBallistaArrowPoolSize, weaponBallistaArrowPrefab);

    }


    void PopulatePool(List<GameObject> objectList, int objectPoolSize, GameObject objectPrefab)
    {

        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject weapon = Instantiate(objectPrefab, gameObject.transform);
            objectList.Add(weapon);
            objectList[i].SetActive(false);
        }
    }


    public GameObject GetWeaponBallista()
    {
        if (weaponBallista == null) { return null; }

        for (int i = 0; i < weaponBallista.Count; i++)
        {
            if (!weaponBallista[i].activeInHierarchy)
            {
                return weaponBallista[i];
            }

        }
        return null;
    }

    public GameObject GetWeaponBallistaHover()
    {
        if (weaponBallistaHover == null) { return null; }

        for (int i = 0; i < weaponBallistaHover.Count; i++)
        {
            if (!weaponBallistaHover[i].activeInHierarchy)
            {
                return weaponBallistaHover[i];
            }

        }
        return null;
    }

    public GameObject GetWeaponBallistaArrow()
    {
        if (weaponBallistaArrow == null) { return null; }

        for (int i = 0; i < weaponBallistaArrow.Count; i++)
        {
            if (!weaponBallistaArrow[i].activeInHierarchy)
            {
                return weaponBallistaArrow[i];
            }

        }
        return null;
    }
}
