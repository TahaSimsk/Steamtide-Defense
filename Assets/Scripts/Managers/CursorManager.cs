using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D upgradeCursorTexture;
    public Texture2D demolishCursorTexture;
    public Vector2 hotSpot;


    private void OnEnable()
    {
        //DelegateManager.onMouseOverDemolish += SetCursor(upgradeCursorTexture);
        DelegateManager.onMouseOverDemolish += () => SetCursor(demolishCursorTexture);
        DelegateManager.onMouseOverUpgrade += () => SetCursor(upgradeCursorTexture);

    }

    private void OnDisable()
    {
        DelegateManager.onMouseOverDemolish -= () => SetCursor(demolishCursorTexture);
        DelegateManager.onMouseOverUpgrade -= () => SetCursor(upgradeCursorTexture);
    }


    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, CursorMode.Auto);
    }
}
