using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapEscape : MonoBehaviour {

    public GameObject map;
    public GameObject mapCamera;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject pauseCanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (map.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                map.SetActive(false);
                mapCamera.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }
        else if (settingsMenu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settingsMenu.SetActive(false);
                pauseCanvas.SetActive(true);
            }
        }
    }
}
