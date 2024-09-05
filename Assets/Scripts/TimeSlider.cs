using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public float currentTime;
    private float maxTime;
    Slider slider;
    public bool isStop = false;
    private void Start()
    {
        StartCoroutine(TimeFlow());
        slider = gameObject.GetComponent<Slider>();
        currentTime = SlotManager._slot.timer;
        maxTime = SlotManager._slot.maxTimer;
    }
    IEnumerator TimeFlow()
    {
        while (!isStop)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            currentTime++;
            slider.value = Mathf.Clamp(currentTime / maxTime, 0, 1);
            if(slider.value == 1)
            {
                if (Managers.Data.scoreData.currentScore >= SlotManager._slot.clearScore)
                {
                    Managers.Game.PlayerDied(2);
                }
                else Managers.Game.PlayerDied(1);
                yield break;
            }

            SlotManager._slot.TimerAt(currentTime);
        }
        yield return null;
    }


}
