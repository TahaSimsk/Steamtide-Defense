using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UIManager uiManager;

    SkillManager skillManager;

    List<bool> bools = new List<bool>();

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        skillManager = FindObjectOfType<SkillManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bools = uiManager.ReturnFlags();
        skillManager.ActivateBomb(false);
        //uiManager.ClearFlags();
        uiManager.SetFlags(false, false, false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        uiManager.SetFlags(bools[0], bools[1], bools[2]);
        skillManager.ActivateBomb(true);
        bools.Clear();
    }
}
