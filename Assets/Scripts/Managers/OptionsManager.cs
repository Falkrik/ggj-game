using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SliderEvent masterSlider;
    [SerializeField] private SliderEvent musicSlider;
    [SerializeField] private SliderEvent sfxSlider;

    [Header("Tools")]
    [SerializeField] private RectTransform highlighter;
    private int rowIndex = 0;


    void Start()
    {
        if(AudioManager.audioManager == null)
        {
            Debug.Log("NO AUDIOMANAGER WAS FOUND (IS CREATED IN MENU)");
            return;
        }

        masterSlider.SetValue(AudioManager.audioManager.master);
        musicSlider.SetValue(AudioManager.audioManager.music);
        sfxSlider.SetValue(AudioManager.audioManager.sfx);

        ChangeRow(0);
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
            ChangeRow(-1);
        if (Input.GetKeyUp(KeyCode.S))
            ChangeRow(1);

        if (Input.GetKeyUp(KeyCode.A))
            TriggerButton(-1);
        if (Input.GetKeyUp(KeyCode.D))
            TriggerButton(1);

        if (rowIndex == 3)
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
                TriggerButton(0);
        }
    }

    private void ChangeRow(int dir)
    {
        rowIndex = (rowIndex + dir < 0 ? 3 : rowIndex + dir) % 4;

        if(rowIndex < 3)
        {
            highlighter.anchoredPosition = new Vector3(-82, 103 + rowIndex * -127, 0);
            highlighter.sizeDelta = new Vector2(715, 65);
        }
        else
        {
            highlighter.anchoredPosition = new Vector3(-25, -230, 0);
            highlighter.sizeDelta = new Vector2(250, 65);
        }
    }

    private void TriggerButton(int dir)
    {
        switch(rowIndex)
        {
            case 0:
                masterSlider.ChangeValue(dir);
                break;

            case 1:
                musicSlider.ChangeValue(dir);
                break;

            case 2:
                sfxSlider.ChangeValue(dir);
                break;

            case 3:
                if(dir == 0)
                    gameObject.SetActive(false);
                break;
        }
    }

    public void ResetIndex()
    {
        rowIndex = 0;
        ChangeRow(0);
    }
}
