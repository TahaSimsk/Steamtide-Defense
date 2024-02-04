using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeCostText;

    RectTransform tooltip;

  
    private void Start()
    {
        tooltip = upgradeCostText.transform.parent.GetComponent<RectTransform>();
    }

    public void ShowTip(string text, Vector3 pos, bool isPlaceable)
    {


        tooltip.gameObject.SetActive(true);

        Vector2 position = pos;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        float finalPivotX = 0f;
        float finalPivotY = 0f;


        if (pivotX < 0.5) //If mouse on left of screen move tooltip to right of cursor and vice vera
        {
            finalPivotX = -0.1f;
        }
        else
        {
            finalPivotX = 1.01f;
        }

        if (pivotY < 0.5) //If mouse on lower half of screen move tooltip above cursor and vice versa
        {
            finalPivotY = 0;
        }
        else
        {
            finalPivotY = 1;
        }

        if (pivotX < 0.5 && pivotY > 0.5) //If mouse on top of screen and left of screen move tooltip to left of cursor
        {
            finalPivotX = 1.01f;
        }


        tooltip.pivot = new Vector2(finalPivotX, finalPivotY);
        tooltip.position = position;

        upgradeCostText.text = text;

        if (isPlaceable)
        {
           
            tooltip.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {
            
            tooltip.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

    }

    public void DisableTip()
    {
        tooltip.gameObject.SetActive(false);
    }


}
