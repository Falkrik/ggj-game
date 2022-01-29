using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Text timerText;
    [SerializeField] private Animator suddenDeathAnim;


    [Header("Tools")]
    private int prevTime;
    private bool finalCountdownStarted = false;


    public void UpdateStockCount(int playerNumber, int stockCount)
    {

    }

    public void UpdateDuality(int playerNumber, int dualityCount)
    {

    }

    public void UpdateTimer(float currentTime)
    {
        if (currentTime <= 0)
            return;

        if (currentTime <= 1)
        {
            timerText.GetComponent<Animator>().SetBool("Countdown", false);
        }

        if (!finalCountdownStarted && currentTime <= 10.3f)
        {
            finalCountdownStarted = true;
            timerText.GetComponent<Animator>().SetBool("Countdown", true);
        }

        currentTime = (int)currentTime;
        
        if (prevTime == currentTime)
            return;

        prevTime = (int)currentTime;

        string syntax = "";

        if(currentTime >= 60)
        {
            syntax += (int)(currentTime / 60);
            syntax += ":" + (currentTime%60).ToString("00");
        }
        else
        {
            syntax += currentTime;
        }

        timerText.text = syntax;
    }

    public void SuddenDeathPopup()
    {
        suddenDeathAnim.Play("SuddenDeathPopup");
    }

    public void WinPopup(int playerNumber)
    {

    }

    public void TransitionScene()
    {

    }

}
