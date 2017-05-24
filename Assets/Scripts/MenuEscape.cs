using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEscape : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject settingsMenu;
    public GameObject levelsMenu;
    public GameObject HighScoreMenu;
    public GameObject customMapsMenu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!mainMenu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (newGameMenu.activeInHierarchy)
                {
                    newGameMenu.SetActive(false);
                    mainMenu.SetActive(true);
                }
                else if (settingsMenu.activeInHierarchy)
                {
                    settingsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                }
                else if (levelsMenu.activeInHierarchy)
                {
                    levelsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                }
                else if (customMapsMenu.activeInHierarchy)
                {
                    customMapsMenu.SetActive(false);
                    newGameMenu.SetActive(true);
                }
                else
                {
                    HighScoreMenu.SetActive(false);
                    mainMenu.SetActive(true);
                }
            }
        }
    }
}
