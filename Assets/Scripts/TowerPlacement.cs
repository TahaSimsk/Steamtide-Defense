using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    WeaponType weaponType;

    [SerializeField] Vector3 offset;

    GameObject hoveredWeapon;


    bool isPlaced;
    bool hoverShowed;

    ObjectPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        weaponType = FindObjectOfType<WeaponType>();
    }

    private void OnMouseDown()
    {
        if (weaponType.ballista)
        {
            PlaceWeapon(objectPool.GetWeaponBallista());
        }
        else if (weaponType.blaster)
        {
            PlaceWeapon(objectPool.GetWeaponBlaster());
        }
        else if (weaponType.cannon)
        {
            PlaceWeapon(objectPool.GetWeaponCannon());
        }
    }



    private void OnMouseEnter()
    {
        if (weaponType.ballista)
        {
            ShowWeaponHovered(objectPool.GetWeaponBallistaHover());
        }
        else if (weaponType.blaster)
        {
            ShowWeaponHovered(objectPool.GetWeaponBlasterHover());
        }
        else if (weaponType.cannon)
        {
            ShowWeaponHovered(objectPool.GetWeaponCannonHover());
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
        if(hoveredWeapon==null) { return; }
        hoveredWeapon.SetActive(false);

        hoverShowed = false;
    }
}
