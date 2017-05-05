using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakerEscape : MonoBehaviour
{

    public GameObject levelMenu;
    public GameObject placablesMenu;
    bool isLevelPanel;
    bool isActive;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = false;
            if (levelMenu.activeInHierarchy)
            {
                isLevelPanel = true;
                isActive = true;
            }
            else if (placablesMenu.activeInHierarchy)
            {
                isLevelPanel = false;
                isActive = true;
            }

            if (isLevelPanel)
            {
                if (isActive)
                {
                    levelMenu.SetActive(false);
                }
                else
                {
                    levelMenu.SetActive(true);
                }
            }
            else
            {
                if (isActive)
                {
                    placablesMenu.SetActive(false);
                }
                else
                {
                    placablesMenu.SetActive(true);
                }
            }
        }

    }
}
