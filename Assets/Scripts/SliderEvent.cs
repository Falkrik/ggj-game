using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderEvent : MonoBehaviour
{
    private Slider slider;

    public void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void ValueChanged(int type)
    {
        SliderType key = (SliderType)type;
        AudioManager.audioManager.UpdateVolume(key, slider.value);
    }

    public void ChangeValue(int factor)
    {
        slider.value += 0.1f * factor;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
