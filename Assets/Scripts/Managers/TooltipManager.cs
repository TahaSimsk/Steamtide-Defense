
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] GameEvent0ParamSO onEscPressed;
    RectTransform tooltip;

    public static TooltipManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        onEscPressed.onEventRaised += DisableTip;
    }
    private void OnDisable()
    {
        onEscPressed.onEventRaised -= DisableTip;
    }

    private void Start()
    {
        tooltip = tooltipText.transform.parent.GetComponent<RectTransform>();
    }

    public void ShowTip(string text, Vector3 pos, bool isPlaceable)
    {
        tooltip.gameObject.SetActive(true);

        Vector2 position = pos;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        float finalPivotX = 0f;
        float finalPivotY = 0f;


        if (pivotX < 0.5) //If mouse on left of screen move tooltip to right of cursor and vice versa
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

        tooltipText.text = text;


        if (isPlaceable)
        {

            tooltip.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {

            tooltip.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

    }

    public void ShowTip(string text, Vector3 pos)
    {
        tooltip.gameObject.SetActive(true);

        Vector2 position = pos;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        float finalPivotX = 0f;
        float finalPivotY = 0f;


        if (pivotX < 0.5) //If mouse on left of screen move tooltip to right of cursor and vice versa
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

        tooltipText.text = text;

    }

    public void DisableTip()
    {
        if (tooltip==null)
        {
            return;
        }
        tooltip.gameObject.SetActive(false);
    }

}
