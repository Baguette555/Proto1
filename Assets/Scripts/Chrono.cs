using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chrono : MonoBehaviour
{
    [SerializeField] public float timeValue = 0;
    [SerializeField] TextMeshProUGUI TimeText;
    public bool timePaused = false;

    private void Start()
    {
        TimeText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(!timePaused)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            // does nothin
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        // get the total full seconds
        var t0 = (int)timeToDisplay;

        // full seconds to minutes and seconds
        var m = t0 / 60;

        // get the remaining seconds
        var s = (t0 - m * 60);

        // get the 2 values of the milliseconds
        var ms = (int)((timeToDisplay - t0) * 100);

        TimeText.text = $"{m:00}:{s:00}:{ms:00}";
    }

    public void TimerPaused()       // Ne sera s�rement presque jamais appel� : l'id�e du chrono est chang�e
    {
        timePaused = !timePaused;
        if(timePaused)
        {
            Debug.Log("Temps mis en pause");
        }
        else
        {
            Debug.Log("Temps repris");
        }
    }
}
