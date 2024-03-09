using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EMPBomb : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] TargetScanner scanner;
    ShockData shockData;


    void Start()
    {
        shockData = (ShockData)towerInfo.InstTowerData;
        button.onClick.AddListener(StunEnemies);
    }


    private void OnDestroy()
    {
        button.onClick.RemoveListener(StunEnemies);
    }

    void StunEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(towerInfo.transform.position, scanner.GetComponent<SphereCollider>().radius * scanner.transform.lossyScale.x, shockData.enemyLayer);
        if (colliders.Length <= 0) return;
        EnemyMovement enemyMovement;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<IBoss>() != null && !shockData.canFreezeBosses) continue;
            
            enemyMovement = collider.GetComponent<EnemyMovement>();
            if (enemyMovement == null) continue;
            enemyMovement.DecreaseMoveSpeedByPercentage(100, shockData.freezeDuration);

        }
        StartCoroutine(StartTimer());

        //TODO: Play an emp anim here

    }
    
    IEnumerator StartTimer()
    {

        button.interactable = false;
        yield return new WaitForSeconds(shockData.empCooldown);
        button.interactable = true;
    }


}
