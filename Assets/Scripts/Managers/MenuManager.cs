using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Menu Customization")]
    public Color selectedColor;
    [Space]
    public Color[] playerColors;


    [Header("References")]
    [SerializeField] private GameObject optionsParent;
    [SerializeField] private Animator optionsPopup;


    [Header("Tools")]
    public GameObject[] buttonList;
    public Image[] playerIcons;
    private int[] playerColorIndex = { 0, 0 };
    private int selectedIndex = 0;

    private bool inOptions = false;


    void Start()
    {
        SelectButton(0);
    }

    
    void Update()
    {
        if (inOptions)
        {
            if (optionsParent.activeSelf == false)
                inOptions = false;
            return;
        }

        // Temporary input
        if (Input.GetKeyUp(KeyCode.W))
        {
            SelectButton(-1);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            SelectButton(1);
        }

        if (Input.GetKeyUp(KeyCode.A))
            ChangePlayerColorCCW(0);
        if (Input.GetKeyUp(KeyCode.D))
            ChangePlayerColorCW(0);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            ChangePlayerColorCCW(1);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            ChangePlayerColorCW(1);


        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            TriggerButton();
        }    
    }


    private void SelectButton(int dir)
    {
        buttonList[selectedIndex].GetComponent<Text>().color = Color.white;

        selectedIndex = (selectedIndex + dir < 0 ? 2 : selectedIndex + dir) % 3;

        buttonList[selectedIndex].GetComponent<Text>().color = selectedColor;
    }

    public void StartGame()
    {
        Debug.Log("MenuManager :: StartGame");

    }

    public void OpenSettings()
    {
        inOptions = !inOptions;

        optionsParent.SetActive(inOptions);
        optionsPopup.SetBool("Visible", inOptions);
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

    public void ChangePlayerColorCCW(int playerID)
    {
        int index = playerColorIndex[playerID];
        index = index - 1 < 0 ? playerColors.Length - 1 : index - 1;
        playerColorIndex[playerID] = index;

        playerIcons[playerID].color = playerColors[index];
    }

    public void ChangePlayerColorCW(int playerID)
    {
        int index = playerColorIndex[playerID];
        index = (index + 1) % playerColors.Length;
        playerColorIndex[playerID] = index;

        playerIcons[playerID].color = playerColors[index];
    }
}
