using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    UIManager weaponType;

    [SerializeField] Vector3 offset;

    GameObject hoveredWeapon;


    bool isPlaced;
    bool hoverShowed;

    ObjectPool objectPool;
    MoneySystem moneySystem;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        weaponType = FindObjectOfType<UIManager>();
        moneySystem = FindObjectOfType<MoneySystem>();
    }

    private void OnMouseDown()
    {
       
        PlaceWeapon(weaponType.ballista, moneySystem.ballistaCost, objectPool.ballistaName);
        PlaceWeapon(weaponType.blaster, moneySystem.blasterCost, objectPool.blasterName);
        PlaceWeapon(weaponType.cannon, moneySystem.cannonCost, objectPool.cannonName);

    }



    private void OnMouseEnter()
    {
        
        ShowWeaponHovered(weaponType.ballista, moneySystem.ballistaCost, objectPool.ballistaHoverName, objectPool.ballistaHoverNPName);
        ShowWeaponHovered(weaponType.blaster, moneySystem.blasterCost, objectPool.blasterHoverName, objectPool.blasterHoverNPName);
        ShowWeaponHovered(weaponType.cannon, moneySystem.cannonCost, objectPool.cannonHoverName, objectPool.cannonHoverNPName);
    }


    private void OnMouseExit()
    {
        DestroyWeaponWhenExited();
    }


    void PlaceWeapon(bool weaponType, float weaponCost, string nameOfWeaponFromPool)
    {
        if (weaponType && moneySystem.IsPlaceable(weaponCost))
        {
            if (isPlaced) { return; }

            GameObject weapon = objectPool.GetObjectFromPool(nameOfWeaponFromPool, false);
            weapon.SetActive(true);
            weapon.transform.position = transform.position + offset;

            hoveredWeapon.SetActive(false);

            isPlaced = true;
            moneySystem.DecreaseMoney(weaponCost);
            moneySystem.UpdateMoneyDisplay();
        }

    }

    void ShowWeaponHovered(bool weaponType, float weaponCost, string nameOfPlaceableHover, string nameOfNPHover)
    {
        if (weaponType && moneySystem.IsPlaceable(weaponCost))
        {
            if (isPlaced || hoverShowed) { return; }

            hoveredWeapon = objectPool.GetObjectFromPool(nameOfPlaceableHover, false);
            hoveredWeapon.SetActive(true);
            hoveredWeapon.transform.position = transform.position + offset;
            hoverShowed = true;
        }
        else if (weaponType && !moneySystem.IsPlaceable(weaponCost))
        {
            if (isPlaced || hoverShowed) { return; }

            hoveredWeapon = objectPool.GetObjectFromPool(nameOfNPHover, false);
            hoveredWeapon.SetActive(true);
            hoveredWeapon.transform.position = transform.position + offset;
            hoverShowed = true;
        }
    }

    void DestroyWeaponWhenExited()
    {
        if (hoveredWeapon == null) { return; }
        hoveredWeapon.SetActive(false);

        hoverShowed = false;
    }
}
