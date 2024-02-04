using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<Data> datas;

    List<Data> enemies = new List<Data>();


    void Awake()
    {
        PopulatePoolAndAllocateToData();
    }

    //populates pool and assigns each object into their own list. Each data (scriptable object) is responsible for tracking and holding a reference to itself. During instantiation they get instantiated from their own list which is inside data (scriptable object)
    void PopulatePoolAndAllocateToData()
    {
        foreach (var data in datas)
        {
            data.objList.Clear();
            PopulatePool(data.objList, data.objectPrefabPoolSize, data.objectPrefab);

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


    public GameObject GetObjectFromPool(int hashCode)
    {
        foreach (var data in datas)
        {
            if (data.hashCode == hashCode)
            {
                for (int i = 0; i < data.objList.Count; i++)
                {

                    if (!data.objList[i].activeInHierarchy)
                        return data.objList[i];

                }
            }
            else
            {
                Debug.LogWarning(hashCode + " cannot found");
            }
        }
        return null;
    }



}
