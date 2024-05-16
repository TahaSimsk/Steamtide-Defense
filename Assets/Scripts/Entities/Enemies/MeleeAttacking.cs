using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacking : MonoBehaviour
{
    [SerializeField] ObjectInfo enemyInfo;

    EnemyData enemyData;


    void Start()
    {
        enemyData = enemyInfo.DefObjectGameData as EnemyData;
    }



    public IEnumerator Attack(GameObject target)
    {

        IPlayerDamageable playerDamageable = target.GetComponent<IPlayerDamageable>();

        while (playerDamageable != null)
        {
            playerDamageable.GetDamage(enemyData.Damage);
            yield return new WaitForSeconds(enemyData.ShootingDelay);

        }
    }
}
