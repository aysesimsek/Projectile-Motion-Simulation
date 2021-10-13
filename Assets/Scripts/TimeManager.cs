using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor;
    public float slowdownLenght = 2f;

    void Update()
    {
        Time.timeScale = slowdownFactor;
       // Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void slowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void adjustSlowdownFactor(float newValue)
    {
        slowdownFactor = newValue;
    }
}
