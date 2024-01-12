using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    FlagManager flagManager;


    private void Start()
    {
        flagManager = FindObjectOfType<FlagManager>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        flagManager.hoverMode = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        flagManager.hoverMode = false;
    }

   
    public void OnPointerExit(PointerEventData eventData)
    {
        flagManager.hoverMode = false;
    }

}
