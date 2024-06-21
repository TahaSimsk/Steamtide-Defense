using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] FaceTarget faceTarget;
    [SerializeField] Shooting shooting;
    [SerializeField] GameObject towerVisual;
    TowerData towerData;
    public int CurrentAmmoCount { get; private set; }


    private void Start()
    {
        towerData = towerInfo.InstTowerData;
        CurrentAmmoCount = towerData.TowerAmmoCapacity;
        UpdateAmmoText();
    }

    public bool ReduceAmmoAndCheckHasAmmo()
    {
        if (CurrentAmmoCount == 0)
        {
            return false;
        }
        CurrentAmmoCount--;
        UpdateAmmoText();
        if (CurrentAmmoCount > 0)
        {
            return true;
        }
        else
        {
            shooting.enabled = false;
            //faceTarget.enabled = false;
            towerVisual.transform.localRotation = Quaternion.Euler(40, 0, 0);
            return false;
        }
    }

    public void AddAmmo(int amount)
    {
        CurrentAmmoCount += amount;
        if (CurrentAmmoCount > towerData.TowerAmmoCapacity)
        {
            CurrentAmmoCount = towerData.TowerAmmoCapacity;
        }

        UpdateAmmoText();
        if (shooting.enabled == false)
        {
            shooting.enabled = true;
            //faceTarget.enabled = true;
            towerVisual.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void UpgradeAmmoCapacity(int amount)
    {
        towerData.TowerAmmoCapacity = towerInfo.DefTowerData.TowerAmmoCapacity + (int)(amount * towerInfo.DefTowerData.TowerAmmoCapacity * 0.01f);
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        ammoText.text = CurrentAmmoCount + "/" + towerData.TowerAmmoCapacity;
    }
}
