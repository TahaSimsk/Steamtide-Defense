using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapThrower : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] RangeUpgrade rangeUpgrade;
    FireData fireData;
    float timer;
    Collider[] paths;
    private void Start()
    {
        fireData = towerInfo.InstTowerData as FireData;
        CalculatePaths();

        rangeUpgrade.OnRangeUpgraded += CalculatePaths;
    }

    private void OnDestroy()
    {
        rangeUpgrade.OnRangeUpgraded -= CalculatePaths;
    }

    private void Update()
    {
        if (!fireData.canThrowTrap) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float range = fireData.trapRandomPosOffset;
            int randomIndex = Random.Range(0, paths.Length);
            Vector3 randomPathPos = paths[randomIndex].transform.position;
            randomPathPos.x += Random.Range(-range, range);
            randomPathPos.y = 2f;
            randomPathPos.z += Random.Range(-range, range);
            GameObject go = Instantiate(fireData.fireTrap, randomPathPos, Quaternion.identity);
            go.GetComponent<FireTrap>().damage = fireData.trapDamage;
            timer = fireData.trapCooldown;
        }
    }

    void CalculatePaths()
    {
        paths = null;
        paths = Physics.OverlapSphere(transform.position, fireData.TowerRange / 2, fireData.pathLayer);
    }
}
