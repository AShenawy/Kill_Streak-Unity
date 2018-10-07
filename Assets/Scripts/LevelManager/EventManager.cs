using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour 
{
    public delegate void EventsDelegate();
    public static event EventsDelegate KillStreakInitiatedEvent;
    public static event EventsDelegate KillStreakStopEvent;

    public static event EventsDelegate ThirtySecondEvent;
    public static event EventsDelegate SixtySecondEvent;
    public static event EventsDelegate TwoMinuteEvent;


    bool isThirtyPassed = false;
    bool isSixtyPassed = false;
    bool isTwoMinPassed = false;

    private void Update()
    {
        if (Time.timeSinceLevelLoad > 30f && !isThirtyPassed)
        {
            if (ThirtySecondEvent != null)
                ThirtySecondEvent();
            isThirtyPassed = true;
        }

        if (Time.timeSinceLevelLoad > 60f && !isSixtyPassed)
        {
            if (SixtySecondEvent != null)
                SixtySecondEvent();
            isSixtyPassed = true;
        }

        if (Time.timeSinceLevelLoad > 120f && !isTwoMinPassed)
        {
            if (TwoMinuteEvent != null)
                TwoMinuteEvent();
            isTwoMinPassed = true;
        }
    }

    public static void InitiateKillStreak()
    {
        if (KillStreakInitiatedEvent != null)
        KillStreakInitiatedEvent();
    }

    public static void StopKillStreak()
    {
        if (KillStreakStopEvent != null)
        KillStreakStopEvent();
    }
}
