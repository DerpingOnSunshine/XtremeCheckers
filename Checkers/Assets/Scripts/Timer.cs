using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour { //Use this for counting down

    public int duration = 60;
    public int timeRemaining;
    public bool isCountingDown = false;

    public void Begin()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            timeRemaining = duration;
            Invoke("_tick", 1f);
        }
    }

    private void _tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("_tick", 1f);
        }
        else
        {
            isCountingDown = false;
        }
    }
}
