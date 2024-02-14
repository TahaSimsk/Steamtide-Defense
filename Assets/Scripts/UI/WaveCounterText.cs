using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounterText : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onWaveStart;
    TextMeshProUGUI myText;
    int currentWave;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        onWaveStart.onEventRaised += UpdateWave;
    }
    private void OnDisable()
    {
        onWaveStart.onEventRaised -= UpdateWave;
    }

    void UpdateWave(object num)
    {
        currentWave++;
        myText.text = "Wave: " + currentWave;
    }
}
