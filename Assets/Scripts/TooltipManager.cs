using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeCostText;

    Transform parent;

    private void Start()
    {
        parent = upgradeCostText.transform.parent;
    }

    public void ShowTip(string text, Vector3 pos)
    {
      
        upgradeCostText.text = text;
        parent.gameObject.SetActive(true);


        float pivotX = pos.x / Screen.width;
        float pivotY = pos.y / Screen.height;

        parent.GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
        parent.position = pos;
    }

    public void DisableTip()
    {
       parent.gameObject.SetActive(false);
    }

}
