using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSliderBaseClass : MonoBehaviour
{
    [SerializeField] protected TargetingSystem targetingSystem;
    [SerializeField] TextMeshProUGUI sliderText;

    Slider slider;


    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = GetPoint();
        sliderText.text = slider.value.ToString();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangePoint);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangePoint);
    }

    void ChangePoint(float amount)
    {
        GetPoint() = (int)amount;
        sliderText.text = amount.ToString();
    }

    protected virtual ref int GetPoint()
    {
        return ref targetingSystem.closestTargetPoint;
    }
}
