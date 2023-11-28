using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public List<ObjectToPool> objectsToPool;

    [Header("Weapon Section")]

    [Header("Ballista")]

    public string ballistaName;
    public string ballistaProjectileName;
    public string ballistaHoverName;
    public string ballistaHoverNPName;

    [Header("Blaster")]

    public string blasterName;
    public string blasterProjectileName;
    public string blasterHoverName;
    public string blasterHoverNPName;

    [Header("Cannon")]

    public string cannonName;
    public string cannonProjectileName;
    public string cannonHoverName;
    public string cannonHoverNPName;



    List<ObjectToPool> enemies = new List<ObjectToPool>();

    private void Awake()
    {
        foreach (var objects in objectsToPool)
        {
            objects.objList.Clear();
            PopulatePool(objects.objList, objects.objPrefabPoolSize, objects.objPrefab);

            if (objects.objPrefab.CompareTag("Enemy") && !enemies.Contains(objects))
            {
                enemies.Add(objects);
            }
        }
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


    public GameObject GetObjectFromPool(string nameOfObject, bool isProjectile)
    {
        foreach (var objects in objectsToPool)
        {
            if (objects.name == nameOfObject)
            {
                for (int i = 0; i < objects.objList.Count; i++)
                {
                    if (isProjectile && !objects.objList[i].activeInHierarchy && objects.objList[i].GetComponent<Projectile>().currentTower == null)
                    {
                        return objects.objList[i];
                    }
                    else if (!objects.objList[i].activeInHierarchy)
                    {
                        return objects.objList[i];
                    }
                }
            }
        }
        return null;
    }



    public GameObject GetEnemy(int enemyID)
    {
        foreach (var item in enemies)
        {
            if (item.objPrefab.GetComponent<EnemyHealth>().enemyId == enemyID)
            {
                return GetObjectFromPool(item.name, false);
            }
        }
        return null;
    }
}
