using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextWaveTimerText : MonoBehaviour
{
    TextMeshProUGUI myText;
    float countdown;
    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.text = "";
    }

    private void OnEnable()
    {
        EventManager.onWaveEnd += StartTimer;
    }
    private void OnDisable()
    {
        EventManager.onWaveEnd -= StartTimer;
    }

    void StartTimer(float timer)
    {
        countdown = timer;
        while (true)
        {
            if (countdown > 1)
            {
                countdown -= Time.deltaTime;
                myText.text = "Next Wave Starts In: " + Mathf.FloorToInt(countdown);
            }
            else
            {
                break;
            }
        }


        myText.text = "";




    }
}
