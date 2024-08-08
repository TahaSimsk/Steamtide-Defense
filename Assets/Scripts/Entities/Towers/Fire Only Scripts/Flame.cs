using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] XPManager xpManager;
    FireData fireData;


    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (other.GetComponent<EnemyHealth>().ReduceHealth(fireData.ProjectileDamage))
        {
            xpManager.GainXp();
        }

    }
}
