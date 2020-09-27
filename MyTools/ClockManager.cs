using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Simulates an AM/PM clock system starting at StartTimeHour/Minute where display minutes increase in increments of 10
public class ClockManager : MonoBehaviour
{
    private int militaryTimeMinute = 0;
    private int militaryTimeHour = 0;

    private int displayHour = 0; //AM/PM hour
    private int displayMinute = 0; //multiples of 10 minutes

    private bool isAM;

    private float tickTimer = 0; //ticks to determine a second in real time using delta time
    public float realSecondsPerGameMin; //real seconds per in game minute

    public bool timePaused;
    public bool currentlyIndoors; //if indoors, game time does not proceed

    public int startTimeHour;
    public int startTimeMinute;


    private virtual void Start()
    {
        Inititialize();
    }


    private virtual void Update()
    {
        if (currentlyIndoors)
        {
            //Indoors so we don't pass time
        }

        else
        {
            if (!timePaused)
            {
                tickTimer += Time.deltaTime; //change in time

                if (tickTimer >= realSecondsPerGameMin)
                {
                    MinutePassed();
                }
            }
        }
    }


    public virtual void Inititialize()
    {
        isAM = true;

        militaryTimeHour = startTimeHour;
        militaryTimeMinute = startTimeMinute;
        displayHour = startTimeHour;
        displayMinute = startTimeMinute;
    }


    public virtual void MinutePassed()
    {
        militaryTimeMinute += 1;
        tickTimer -= realSecondsPerGameMin; //Reset tickTimer so it can start counting to 1 

        if(militaryTimeMinute%10 == 0) //only change display minutes if its a multiple of 10
        {
            displayMinute = militaryTimeMinute;
        }

        if(militaryTimeMinute >= 60)
        {
            HourPassed();
        }
    }


    private virtual void HourPassed()
    {
        militaryTimeHour++;
        displayHour++;
        militaryTimeMinute -= 60;
        displayMinute -= 60;

        if (displayHour == 12) //if its 12 we change AM/PM
        {
            isAM = !isAM;
        }

        if (displayHour >= 13) //Passed Noon/Midnight
        {
            displayHour -= 12;
        }

        if(militaryTimeHour >= 25)
        {
            militaryTimeHour -= 24;
        }
    }


    public virtual void Pausetime()
    {
        timePaused = true;
    }


    public virtual void UnPauseTime()
    {
        timePaused = false;
    }


    public virtual void EnteredIndoors()
    {
        currentlyIndoors = true;
    }


    public virtual void ExitedIndoors()
    {
        currentlyIndoors = false;
    }
}
