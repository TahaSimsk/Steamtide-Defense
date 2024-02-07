using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounterText : MonoBehaviour
{
    TextMeshProUGUI myText;
    int currentWave;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager.onWaveStart += UpdateWave;
    }
    private void OnDisable()
    {
        EventManager.onWaveStart -= UpdateWave;
    }

    void UpdateWave(int num)
    {
        currentWave++;
        myText.text = "Wave: " + currentWave;
    }
}
