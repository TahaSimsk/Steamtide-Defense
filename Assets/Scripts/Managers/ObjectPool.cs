using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameData> objectsToPool;
    Dictionary<int, List<GameObject>> hashcodeListPairs = new Dictionary<int, List<GameObject>>();

    public static ObjectPool Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        PopulatePoolAndAllocateToData();

    }
  

    //populates pool and assigns each object into their own list. Each data (scriptable object) is responsible for tracking and holding a reference to itself. During instantiation they get instantiated from their own list which is inside data (scriptable object)
    void PopulatePoolAndAllocateToData()
    {
        
        foreach (var data in objectsToPool)
        {
            if (data is IPoolable i)
            {
                i.objList = new List<GameObject>();
                PopulatePool(i.objList, i.ObjectPoolsize, i.ObjectPrefab);
            }

            if (data is EnemyData enemy)
            {
                if (enemy.ProjectilePoolSize <= 0) continue;
                enemy.ProjectileList = new List<GameObject>();
                PopulatePool(enemy.ProjectileList, enemy.ProjectilePoolSize, enemy.ProjectilePrefab);
            }
        }

    }

    void PopulatePool(List<GameObject> objectList, int objectPoolSize, GameObject objectPrefab)
    {
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject weapon = Instantiate(objectPrefab, gameObject.transform);
            weapon.SetActive(false);

            //this is the code responsible for adding the instantiated weapon into each data list
            objectList.Add(weapon);
        }
    }





    /// <summary>
    ///  hashcode as a type. if hashcode found in the dictionary, corresponding list will be searched for valid gameobject. If hashcode is not found in the dictionary, fresh new list with a valid gameobject will be created and added to that hashcode. When searching the list if non-valid objects are all there is in the list, a new object will be created and added to that list.
    /// </summary>
    /// <param name="hashCode">acts as type</param>
    /// <param name="objectPrefab">for initialization and non-valid instantiation</param>
    /// <returns></returns>
    public GameObject GetObject(int hashCode, GameObject objectPrefab)
    {
        //1. searches the passed hashcode in the dictionary,
        if (hashcodeListPairs.ContainsKey(hashCode))
        {
            List<GameObject> temp = hashcodeListPairs[hashCode];

            //if the list is not empty and have at least one object in it, it checks whether if the object/s is/are currently  not at use(disabled)
            foreach (var item in temp)
            {
                //if object is not at use (disabled) then it returns that object
                if (!item.activeInHierarchy)
                {
                    //item.SetActive(true);
                    return item;
                }

            }

            //if all object/s is/are at use (active) then it instantiates the passed object prefab and adds it to the list then returns the object
            GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
            newObject.SetActive(false);
            hashcodeListPairs[hashCode].Add(newObject);
            return newObject;
        }
        else
        {
            //if hascode is not found in the dictionary, it creates a list, instantiates the passed object prefab, adds it to the newly created list, then adds this list to the dictionary with the passed hashcode so next time that hashcode is passed the dictionary will have a list that contains an instantiated object
            List<GameObject> temp = new List<GameObject>();
            GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
            newObject.SetActive(false);
            temp.Add(newObject);
            hashcodeListPairs.Add(hashCode, temp);
            return newObject;
        }
    }

}
