using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [SerializeField] float upgradedProjectileSpeed;
    [SerializeField] float upgradedProjectileDmg;
    [SerializeField] float upgradedProjectileLife;
    [SerializeField] float upgradedShootingDelay;
    [SerializeField] float upgradedTowerRange;
    [SerializeField] float upgradeCost;

    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 hotSpot;
    //[SerializeField] TMP_Text upgradeCostText;

    Tower tower;
    UIManager flags;
    MeshRenderer meshRenderer;
    MoneySystem moneySystem;
    TooltipManager tooltipManager;

    private void Start()
    {
        tooltipManager=FindObjectOfType<TooltipManager>();
        moneySystem = FindObjectOfType<MoneySystem>();
        flags = FindObjectOfType<UIManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        tower = GetComponent<Tower>();
    }

    private void OnMouseDown()
    {
        if (flags.upgradeMode && moneySystem.IsPlaceable(upgradeCost))
        {
            UpgradeWeapon();
            moneySystem.DecreaseMoney(upgradeCost);
        }
    }
    private void OnMouseEnter()
    {
        if (flags.upgradeMode)
        {
            SetCursor(cursorTexture);
            tooltipManager.ShowTip("Upgrade Cost: " + upgradeCost + "$", Input.mousePosition);
        }
    }
    private void OnMouseExit()
    {
        if (flags.upgradeMode)
        {
            SetCursor(null);
            tooltipManager.DisableTip();
            
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
