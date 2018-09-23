using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float SlowdownFactor = 0.05f;
    public float SlowdownLength = 2f;

    private void Update()
    {
        Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion()
    {
        Time.timeScale = SlowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
