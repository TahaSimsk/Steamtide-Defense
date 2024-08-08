using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public Action<object> OnLevelUp;
    [SerializeField] int maxLvl;
    [SerializeField] Slider slider;
    [SerializeField] AnimationCurve xpCurve;
    [SerializeField] TextMeshProUGUI levelText;

    float currentXP = 0;
    int currentLvl = 1;

    float currentLvlXPReq = 1;

    void Awake()
    {
        UpdateUI();
    }
  
    public void GainXp()
    {
        currentLvlXPReq = xpCurve.Evaluate(currentLvl);
        currentXP += 5;
        if (currentXP >= currentLvlXPReq)
        {
            HandleLevelUp();
        }
        UpdateUI();
    }

    void HandleLevelUp()
    {
        currentLvl++;
        currentXP -= currentLvlXPReq;
        OnLevelUp?.Invoke(1f);
    }

    void UpdateUI()
    {
        slider.value = currentXP / currentLvlXPReq;
        levelText.text = $"Level {currentLvl}/{maxLvl}";
    }
}
