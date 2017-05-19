using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoreMainMenu : MonoBehaviour {
    public Text highscoreMainText;
    public static int highscore;
    public static int highscore2;
    public static int highscore3;
    private string stringEcho;

    // Use this for initialization
    void Start () {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        highscore2 = PlayerPrefs.GetInt("highscore2", highscore2);
        highscore3 = PlayerPrefs.GetInt("highscore3", highscore3);

        stringEcho = (String.Format("1st: {0,-10}\n2nd: {1,-10}\n3rd: {2,-10}"
            , highscore, highscore2, highscore3));

        highscoreMainText.text = stringEcho;
        //highscoreMainText.text = "1st: " + highscore.ToString() + "\n2nd: " + highscore2.ToString() +
        //    "\n3rd: " + highscore3.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
