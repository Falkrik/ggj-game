using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Text timerText;
    // Stock
    // Duality count
    [Space]
    [SerializeField] private Animator suddenDeathAnim;
    [Space]
    [SerializeField] private Text winnerText;
    [SerializeField] private Animator winAnim;
    [SerializeField] private GameObject replayPopup;
    [SerializeField] private Image replayButtonHighlight;
    [Space]
    [SerializeField] private GameObject pauseParent;
    [SerializeField] private GameObject pausePopup;
    [SerializeField] private Image pauseButtonHighlight;


    [Header("Tools")]
    private int prevTime;
    private bool finalCountdownStarted = false;
    private int pauseSelectedIndex = 0;
    private int replaySelectedIndex = 0;
    private bool gamePaused = false;
    private bool gameOver = false;


    private void Start()
    {
        //WinPopup(1);
        //PauseScreen();
    }

    private void Update()
    {
        if (!gamePaused && !gameOver)
            return;


        if (Input.GetKeyUp(KeyCode.A))
            SelectButton(-1);
        if (Input.GetKeyUp(KeyCode.D))
            SelectButton(1);

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            TriggerButton();
    }

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
        gameOver = true;
        winnerText.text = "Player " + playerNumber + " won!";
        winAnim.Play("SuddenDeathPopup");

        replaySelectedIndex = 0;
        replayButtonHighlight.rectTransform.anchoredPosition = new Vector3(-140, -125, 0);
        StartCoroutine(WinPopupCooldown());
    }

    public void PauseScreen()
    {
        if (gameOver)
            return;

        gamePaused = !gamePaused;

        if(gamePaused)
        {
            pauseButtonHighlight.rectTransform.anchoredPosition = new Vector3(-276, -86, 0);
            pauseSelectedIndex = 0;

            pauseParent.SetActive(true);
            pausePopup.GetComponent<Animator>().SetBool("Visible", true);
        }
        else
        {
            StartCoroutine(PausePopupCooldown());
        }
    }

    private void SelectButton(int dir)
    {
        if(gamePaused)
        {
            pauseSelectedIndex = (pauseSelectedIndex + dir < 0 ? 2 : pauseSelectedIndex + dir) % 3;
            pauseButtonHighlight.rectTransform.anchoredPosition = new Vector3(-275 + 315 * pauseSelectedIndex, -86, 0);
        }
        else if(gameOver)
        {
            replaySelectedIndex = (replaySelectedIndex + dir < 0 ? 1 : replaySelectedIndex + dir) % 2;
            replayButtonHighlight.rectTransform.anchoredPosition = new Vector3(-140 + 300 * replaySelectedIndex, -125, 0);
        }
    }

    private void TriggerButton()
    {
        if(gamePaused)
        {
            switch(pauseSelectedIndex)
            {
                case 0:
                    PauseScreen();
                    break;

                case 1:
                    //
                    break;

                case 2:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
        else if(gameOver)
        {
            switch(replaySelectedIndex)
            {
                case 0:
                    replayPopup.GetComponent<Animator>().SetBool("Visible", false);
                    // GameManager.RefreshBattleManager();
                    gameOver = false;
                    break;

                case 1:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }

    private IEnumerator WinPopupCooldown()
    {
        yield return new WaitForSeconds(3.5f);
        replayPopup.GetComponent<Animator>().SetBool("Visible", true);
    }

    private IEnumerator PausePopupCooldown()
    {
        pausePopup.GetComponent<Animator>().SetBool("Visible", false);
        yield return new WaitForSeconds(0.8f);
        pauseParent.SetActive(false);
    }
}
