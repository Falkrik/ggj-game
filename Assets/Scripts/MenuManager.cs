using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Menu Customization")]
    public Color selectedColor;

    [Header("Tools")]
    public GameObject[] buttonList;
    private int selectedIndex = 0;


    void Start()
    {
        SelectButton(0);
    }

    
    void Update()
    {
        // Temporary input
        if (Input.GetKeyUp(KeyCode.W))
        {
            SelectButton(-1);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            SelectButton(1);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            TriggerButton();
        }    
    }


    private void SelectButton(int dir)
    {
        buttonList[selectedIndex].GetComponent<Text>().color = Color.white;
        buttonList[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Normal;

        selectedIndex = (selectedIndex + dir < 0 ? 2 : selectedIndex + dir) % 3;

        buttonList[selectedIndex].GetComponent<Text>().color = selectedColor;
        buttonList[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void StartGame()
    {
        Debug.Log("MenuManager :: StartGame");

    }

    public void OpenSettings()
    {
        Debug.Log("MenuManager :: OpenSettings");
    }


    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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
                QuitGame();
                break;
        }    
    }
}
