using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    GameStateManager gameStateManager;


    private void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        gameStateManager.isHoveringUI = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //flagManager.hoverMode = false;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        gameStateManager.isHoveringUI = false;
    }



}
