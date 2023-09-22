using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // using text mesh for the clock display

using UnityEngine.Rendering; // used to access the volume component

public enum DayTime
{
    Day=0,
    Evening=1,
    Night=2,
    Sunrise=3
}

public class DayCycle : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI dayDiplay; 
    public Volume ppv; //post processing volume

    public float tick = 10;
    public float seconds=0;
    public int mins = 0;
    int time = 60;
    public int days = 1;

    public bool activateLights;
    public GameObject[] lights;
    public DayTime DTime = DayTime.Day;


    public delegate void DayCycleDelegate();
    public static DayCycleDelegate DayStart;
    public static DayCycleDelegate EveningStart;
    public static DayCycleDelegate NightStart;
    public static DayCycleDelegate SunriseStart;

    // Start is called before the first frame update
    void Start()
    {
        DayStart?.Invoke();
        ppv = gameObject.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcTime();
        DisplayTime();
    }

    public void CalcTime()
    {
        seconds += Time.deltaTime;
        //Debug.Log("paso "+ Time.fixedDeltaTime);

        if(seconds >= time)
        {
            seconds = 0;
            if (((int)DTime + 1) < 4)
            {
                DTime = (DayTime)((int)DTime + 1);
                if(((int)DTime + 1) == 1)
                {
                    EveningStart?.Invoke();
                }else if(((int)DTime + 1) == 2)
                {
                    NightStart?.Invoke();
                }else if(((int)DTime + 1) == 3)
                {
                    SunriseStart?.Invoke();
                }
            }
            else
            {
                DayStart?.Invoke();
                DTime = (DayTime)0;
                days++;
            }
        }
        if (DTime == DayTime.Evening || DTime == DayTime.Sunrise)
        {
            time = 5;
        }
        else
        {
            time = 10;
        }

        ControlPPV();
    }

    public void ControlPPV()
    {
        if (DTime == DayTime.Evening)
        {
            ppv.weight = seconds / time;
            if (activateLights == false)
            {
                if (seconds > (time / 2))
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);
                    }
                    activateLights = true;
                }
            }
        }
        if (DTime == DayTime.Sunrise) {
            ppv.weight = 1 - (seconds / time);
            if (activateLights == true)
            {
                if (seconds > (time/2))
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false);
                    }
                    activateLights = false;
                }
            }
        }
    }

    public void DisplayTime()
    {
        timeDisplay.text = "Time: " + (int)seconds;
        dayDiplay.text = "Day: " + days;
    }


}
