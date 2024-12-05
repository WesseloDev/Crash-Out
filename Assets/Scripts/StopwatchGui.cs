using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchGui : MonoBehaviour
{

    public Text text;

    void Update()
    {
        if (!GameManager.Instance.gameActive)
            return;

        var time = TimeSpan.FromSeconds(GameManager.Instance.elapsedTime);

        text.text = string.Format("{0:0}:{1:00}", time.Minutes, time.Seconds);
    }

}
