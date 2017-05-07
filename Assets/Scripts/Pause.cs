using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    
    public GameObject pauseMenu;
    public GameObject map;
    public bool isPaused = false;

	// Use this for initialization
	void Start () {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !map.activeInHierarchy)
        {
            isPaused = !isPaused;
            Pause_action(isPaused);
            pauseMenu.SetActive(isPaused);
        }
    }
    public void Pause_action(bool pause = false)
    {
        isPaused = pause;
        if (!isPaused)
        {            
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
