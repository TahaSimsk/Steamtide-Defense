using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [Header("Weapon Section")]

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


    [Header("Enemy Section")]
    [SerializeField] GameObject enemy1Prefab;
    [SerializeField] int enemy1PoolSize;

    [SerializeField] GameObject enemy2Prefab;
    [SerializeField] int enemy2PoolSize;

    [SerializeField] GameObject enemy3Prefab;
    [SerializeField] int enemy3PoolSize;

    [SerializeField] GameObject enemy4Prefab;
    [SerializeField] int enemy4PoolSize;



    List<GameObject> weaponCannon = new List<GameObject>();
    List<GameObject> weaponCannonBall = new List<GameObject>();
    List<GameObject> weaponCannonHover = new List<GameObject>();

    List<GameObject> enemy1 = new List<GameObject>();
    List<GameObject> enemy2 = new List<GameObject>();
    List<GameObject> enemy3 = new List<GameObject>();
    List<GameObject> enemy4 = new List<GameObject>();


    //public List<GameObject> Enemies()
    //{
    //    List<GameObject> enemies = new List<GameObject>();

    //    enemies = enemy1.Concat(enemy2).Concat(enemy3).Concat(enemy4).ToList();
    //    return enemies;
    //}

    private void Awake()
    {
        #region Weapon Region



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


        #endregion



        #region Enemy Region

        PopulatePool(enemy1, enemy1PoolSize, enemy1Prefab);
        PopulatePool(enemy2, enemy2PoolSize, enemy2Prefab);
        PopulatePool(enemy3, enemy3PoolSize, enemy3Prefab);
        PopulatePool(enemy4, enemy4PoolSize, enemy4Prefab);

        #endregion
    }


    void PopulatePool(List<GameObject> objectList, int objectPoolSize, GameObject objectPrefab)
    {

        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject weapon = Instantiate(objectPrefab, gameObject.transform);
            weapon.SetActive(false);
            objectList.Add(weapon);
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

    GameObject ReturnProjectile(List<GameObject> weaponToSet)
    {
        if (weaponToSet == null) { return null; }

        for (int i = 0; i < weaponToSet.Count; i++)
        {
            if (!weaponToSet[i].activeInHierarchy && weaponToSet[i].GetComponent<Projectile>().currentTower == null)
            {
                return weaponToSet[i];
            }

        }
        return null;
    }


    #region Weapon Region

    #region Ballista Region
    public GameObject GetWeaponBallista()
    {
        return ReturnWeapon(weaponBallista);
    }

    public GameObject GetWeaponBallistaArrow()
    {
        return ReturnProjectile(weaponBallistaArrow);
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
        return ReturnProjectile(weaponBlasterLaser);
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
        return ReturnProjectile(weaponCannonBall);
    }
    public GameObject GetWeaponCannonHover()
    {
        return ReturnWeapon(weaponCannonHover);
    }


    #endregion

    #endregion



    #region Enemy Region


    public GameObject GetEnemy(int enemyWave)
    {
        switch (enemyWave)
        {
            case 1:
                return ReturnWeapon(enemy1);
            case 2:
                return ReturnWeapon(enemy2);
            case 3:
                return ReturnWeapon(enemy3);
            case 4:
                return ReturnWeapon(enemy4);
        }
        return null;
    }


    #endregion
}
