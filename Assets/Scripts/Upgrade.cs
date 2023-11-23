using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [SerializeField] float upgradedProjectileSpeed;
    [SerializeField] float upgradedProjectileDmg;
    [SerializeField] float upgradedProjectileLife;
    [SerializeField] float upgradedShootingDelay;
    [SerializeField] float upgradedTowerRange;

    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 hotSpot;


    Tower tower;
    UIFlags flags;
    MeshRenderer meshRenderer;


    private void Start()
    {
        flags = FindObjectOfType<UIFlags>();
        meshRenderer = GetComponent<MeshRenderer>();
        tower = GetComponent<Tower>();
    }

    private void OnMouseDown()
    {
        if (flags.upgradeMode)
        {
            UpgradeWeapon();
        }
    }
    private void OnMouseEnter()
    {
        if (flags.upgradeMode)
        {
            SetCursor(cursorTexture);
        }
    }
    private void OnMouseExit()
    {
        if (flags.upgradeMode)
        {
            SetCursor(null);
        }
    }

    void UpgradeWeapon()
    {
        foreach (var material in meshRenderer.materials)
        {
            material.color = Color.red;
        }

        
        tower.SetWeaponUpgradeAttributes(upgradedProjectileSpeed, upgradedProjectileDmg, upgradedProjectileLife, upgradedShootingDelay, upgradedTowerRange);

    }

    void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, CursorMode.Auto);
    }
}
