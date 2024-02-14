using UnityEngine;

[CreateAssetMenu(fileName ="CursorManager", menuName ="Managers/CursorManager")]
public class CursorManager : ScriptableObject
{
    public Texture2D upgradeCursorTexture;
    public Texture2D demolishCursorTexture;
    public Vector2 hotSpot;


    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, CursorMode.Auto);
    }
}
