using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject optionsParent;
    [SerializeField] private Animator optionsPopup;
    [SerializeField] private Animator menuPopup;
    [SerializeField] private Animator creditsPopup;
    [Space]
    public AudioClip menuMusic;


    [Header("Tools")]
    private int selectedIndex = 0;
    [SerializeField] private RectTransform highlighter;

    private bool inOptions = false;
    private bool inCredits = false;


    void Start()
    {
        SelectButton(0);
        menuPopup.SetBool("Visible", true);

        AudioManager.audioManager.PlayMusic(menuMusic);
    }

    
    void Update()
    {
        if(inCredits)
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Escape))
                OpenCredits();

            return;
        }

        if (inOptions)
        {
            if (optionsParent.activeSelf == false)
                inOptions = false;
            return;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            SelectButton(-1);
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            SelectButton(1);
        }


        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            TriggerButton();
        }    
    }


    private void SelectButton(int dir)
    {
        selectedIndex = (selectedIndex + dir < 0 ? 2 : selectedIndex + dir) % 3;

        highlighter.anchoredPosition = new Vector3(-380 + selectedIndex * 382, -123, 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainStage");
    }

    public void OpenSettings()
    {
        inOptions = !inOptions;

        optionsParent.SetActive(inOptions);
        optionsPopup.SetBool("Visible", inOptions);
        optionsParent.GetComponent<OptionsManager>().ResetIndex();
    }


    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OpenCredits()
    {
        inCredits = !inCredits;

        creditsPopup.SetBool("Visible", inCredits);
    }

    private void TriggerButton()
    {
        switch(selectedIndex)
        {
            case 0:
                StartGame();
                break;

            case 1:
                OpenSettings();
                break;

            case 2:
                OpenCredits();
                break;
        }    
    }
}
