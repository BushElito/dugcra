using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMainMenu : MonoBehaviour {
    public Text highscoreMainText;
    public static int highscore;

    // Use this for initialization
    void Start () {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        highscoreMainText.text = highscore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
