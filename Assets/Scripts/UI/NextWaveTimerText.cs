using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextWaveTimerText : MonoBehaviour
{
    [SerializeField] GameEvent1ParamSO onWaveEnd;
    TextMeshProUGUI myText;
    float countdown;
    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.text = "";
    }

    private void OnEnable()
    {
        onWaveEnd.onEventRaised += StartTimer;
    }
    private void OnDisable()
    {
        onWaveEnd.onEventRaised -= StartTimer;
    }

    void StartTimer(object timer)
    {
        if (timer is float)
        {
            countdown = (float)timer;

            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            countdown -= Time.deltaTime;

            myText.text = "Next Wave Starts In: " + Mathf.FloorToInt(countdown);

            if (countdown <= 1)
            {
                break;
            }

            yield return null;
        }
        myText.text = "";
    }
}
