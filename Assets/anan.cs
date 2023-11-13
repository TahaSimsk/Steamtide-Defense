using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anan : MonoBehaviour
{
    
    void Update()
    {
        GameObject[] asd = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        List<GameObject> enemies = new List<GameObject>();
        foreach (var obj in asd)
        {
            if (obj.CompareTag("Enemy"))
            {
                enemies.Add(obj);
            }

        }
        Debug.Log(enemies.Count);
    }

    // Update is called once per frame
   
}
