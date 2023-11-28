using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UIManager flags;

    List<bool> bools = new List<bool>();

    private void Start()
    {
        flags = FindObjectOfType<UIManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bools = flags.ReturnFlags();
        flags.SetFlags(false, false, false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flags.SetFlags(bools[0], bools[1], bools[2]);
        bools.Clear();
    }
}
