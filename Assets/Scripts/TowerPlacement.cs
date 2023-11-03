using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    [SerializeField] Vector3 offset;

    GameObject hoveredBallista;


    bool isPlaced;
    bool hoverShowed;

    ObjectPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnMouseDown()
    {
        PlaceWeaponBallista();
    }


    private void OnMouseEnter()
    {
        ShowWeaponBallistaWhenHovered();
    }


    private void OnMouseExit()
    {
        DestroyWeaponBallistaWhenExited();
    }



    void PlaceWeaponBallista()
    {
        if (isPlaced) { return; }

        GameObject weaponBallista = objectPool.GetWeaponBallista();
        weaponBallista.SetActive(true);
        weaponBallista.transform.position = transform.position + offset;

        hoveredBallista.SetActive(false);

        isPlaced = true;
    }

    void ShowWeaponBallistaWhenHovered()
    {
        if (isPlaced || hoverShowed) { return; }

        hoveredBallista = objectPool.GetWeaponBallistaHover();
        hoveredBallista.SetActive(true);
        hoveredBallista.transform.position = transform.position + offset;

        


        hoverShowed = true;
    }

    void DestroyWeaponBallistaWhenExited()
    {
        
        hoveredBallista.SetActive(false);

        hoverShowed = false;
    }
}
