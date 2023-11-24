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
        if (weaponType.ballista && moneySystem.IsPlaceable(moneySystem.ballistaCost))
        {
            PlaceWeapon(objectPool.GetWeaponBallista());
            moneySystem.DecreaseMoney(moneySystem.ballistaCost);
            moneySystem.UpdateMoneyDisplay();
        }
        else if (weaponType.blaster && moneySystem.IsPlaceable(moneySystem.blasterCost))
        {
            PlaceWeapon(objectPool.GetWeaponBlaster());
            moneySystem.DecreaseMoney(moneySystem.blasterCost);
            moneySystem.UpdateMoneyDisplay();
        }
        else if (weaponType.cannon && moneySystem.IsPlaceable(moneySystem.cannonCost))
        {
            PlaceWeapon(objectPool.GetWeaponCannon());
            moneySystem.DecreaseMoney(moneySystem.cannonCost);
            moneySystem.UpdateMoneyDisplay();
        }
    }



    private void OnMouseEnter()
    {
        if (weaponType.ballista)
        {
            ShowWeaponHovered(objectPool.GetWeaponBallistaHover(moneySystem.IsPlaceable(moneySystem.ballistaCost)));
        }
        else if (weaponType.blaster)
        {
            ShowWeaponHovered(objectPool.GetWeaponBlasterHover(moneySystem.IsPlaceable(moneySystem.blasterCost)));
        }
        else if (weaponType.cannon)
        {
            ShowWeaponHovered(objectPool.GetWeaponCannonHover(moneySystem.IsPlaceable(moneySystem.cannonCost)));
        }
    }


    private void OnMouseExit()
    {
        DestroyWeaponWhenExited();
    }



    void PlaceWeapon(GameObject weaponFromPool)
    {
        if (isPlaced) { return; }

        GameObject weapon = weaponFromPool;
        weapon.SetActive(true);
        weapon.transform.position = transform.position + offset;

        hoveredWeapon.SetActive(false);

        isPlaced = true;
    }

    void ShowWeaponHovered(GameObject weaponFromPool)
    {
        if (isPlaced || hoverShowed) { return; }

        hoveredWeapon = weaponFromPool;
        hoveredWeapon.SetActive(true);
        hoveredWeapon.transform.position = transform.position + offset;




        hoverShowed = true;
    }

    void DestroyWeaponWhenExited()
    {
        if (hoveredWeapon == null) { return; }
        hoveredWeapon.SetActive(false);

        hoverShowed = false;
    }
}
