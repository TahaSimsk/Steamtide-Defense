using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Ballista Section")]
    [SerializeField] GameObject weaponBallistaPrefab;
    [SerializeField] int weaponBallistaPoolSize;

    [SerializeField] GameObject weaponBallistaArrowPrefab;
    [SerializeField] int weaponBallistaArrowPoolSize;

    [SerializeField] GameObject weaponBallistaHoverPrefab;
    [SerializeField] int weaponBallistaHoverPoolSize;



    List<GameObject> weaponBallista = new List<GameObject>();
    List<GameObject> weaponBallistaArrow = new List<GameObject>();
    List<GameObject> weaponBallistaHover = new List<GameObject>();


    [Header("Blaster Section")]
    [SerializeField] GameObject weaponBlasterPrefab;
    [SerializeField] int weaponBlasterPoolSize;

    [SerializeField] GameObject weaponBlasterLaserPrefab;
    [SerializeField] int weaponBlasterLaserPoolSize;

    [SerializeField] GameObject weaponBlasterHoverPrefab;
    [SerializeField] int weaponBlasterHoverPoolSize;



    List<GameObject> weaponBlaster = new List<GameObject>();
    List<GameObject> weaponBlasterLaser = new List<GameObject>();
    List<GameObject> weaponBlasterHover = new List<GameObject>();



    [Header("Cannon Section")]
    [SerializeField] GameObject weaponCannonPrefab;
    [SerializeField] int weaponCannonPoolSize;

    [SerializeField] GameObject weaponCannonBallPrefab;
    [SerializeField] int weaponCannonBallPoolSize;

    [SerializeField] GameObject weaponCannonHoverPrefab;
    [SerializeField] int weaponCannonHoverPoolSize;



    List<GameObject> weaponCannon = new List<GameObject>();
    List<GameObject> weaponCannonBall = new List<GameObject>();
    List<GameObject> weaponCannonHover = new List<GameObject>();


    private void Awake()
    {
        #region Ballista Region
        PopulatePool(weaponBallista, weaponBallistaPoolSize, weaponBallistaPrefab);
        PopulatePool(weaponBallistaArrow, weaponBallistaArrowPoolSize, weaponBallistaArrowPrefab);
        PopulatePool(weaponBallistaHover, weaponBallistaHoverPoolSize, weaponBallistaHoverPrefab);
        #endregion

        #region Blaster Region
        PopulatePool(weaponBlaster, weaponBlasterPoolSize, weaponBlasterPrefab);
        PopulatePool(weaponBlasterLaser, weaponBlasterLaserPoolSize, weaponBlasterLaserPrefab);
        PopulatePool(weaponBlasterHover, weaponBlasterHoverPoolSize, weaponBlasterHoverPrefab);
        #endregion

        #region Cannon Region
        PopulatePool(weaponCannon, weaponCannonPoolSize, weaponCannonPrefab);
        PopulatePool(weaponCannonBall, weaponCannonBallPoolSize, weaponCannonBallPrefab);
        PopulatePool(weaponCannonHover, weaponCannonHoverPoolSize, weaponCannonHoverPrefab);
        #endregion
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

    
    GameObject ReturnWeapon(List<GameObject> weaponToSet)
    {
        if (weaponToSet == null) { return null; }

        for (int i = 0; i < weaponToSet.Count; i++)
        {
            if (!weaponToSet[i].activeInHierarchy)
            {
                return weaponToSet[i];
            }

        }
        return null;
    }


    #region Ballista Region
    public GameObject GetWeaponBallista()
    {
        return ReturnWeapon(weaponBallista);
    }

    public GameObject GetWeaponBallistaArrow()
    {
        return ReturnWeapon(weaponBallistaArrow);
    }
    public GameObject GetWeaponBallistaHover()
    {
        return ReturnWeapon(weaponBallistaHover);
    }
    #endregion



    #region Blaster Region
    public GameObject GetWeaponBlaster()
    {
        return ReturnWeapon(weaponBlaster);
    }

    public GameObject GetWeaponBlasterLaser()
    {
        return ReturnWeapon(weaponBlasterLaser);
    }
    public GameObject GetWeaponBlasterHover()
    {
        return ReturnWeapon(weaponBlasterHover);
    }
    #endregion



    #region Cannon Region

    public GameObject GetWeaponCannon()
    {
        return ReturnWeapon(weaponCannon);
    }

    public GameObject GetWeaponCannonBall()
    {
        return ReturnWeapon(weaponCannonBall);
    }
    public GameObject GetWeaponCannonHover()
    {
        return ReturnWeapon(weaponCannonHover);
    }


    #endregion
}
