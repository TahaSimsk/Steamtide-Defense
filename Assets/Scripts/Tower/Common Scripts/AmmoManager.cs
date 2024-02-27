using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] FaceTarget faceTarget;
    [SerializeField] Shooting shooting;
    [SerializeField] GameObject towerVisual;
    TowerData towerData;
    int currentAmmoCount;

    private void Start()
    {
        towerData = towerInfo.InstITower;
        currentAmmoCount = towerData.TowerAmmoCapacity;
        UpdateAmmoText();
    }

    public bool ReduceAmmoAndCheckHasAmmo()
    {
        if (currentAmmoCount == 0)
        {
            return false;
        }
        currentAmmoCount--;
        UpdateAmmoText();
        if (currentAmmoCount > 0)
        {
            return true;
        }
        else
        {
            shooting.enabled = false;
            faceTarget.enabled = false;
            towerVisual.transform.localRotation = Quaternion.Euler(40, 0, 0);
            return false;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmoCount += amount;

        if (shooting.enabled == false)
        {
            shooting.enabled = true;
            faceTarget.enabled = true;
            towerVisual.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void UpgradeAmmoCapacity(int amount)
    {
        towerData.TowerAmmoCapacity = towerInfo.DefITower.TowerAmmoCapacity + (int)(amount * towerInfo.DefITower.TowerAmmoCapacity * 0.01f);
        UpdateAmmoText();
    }
    void UpdateAmmoText()
    {
        ammoText.text = currentAmmoCount + "/" + towerData.TowerAmmoCapacity;
    }
}
