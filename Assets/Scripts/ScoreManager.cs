using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    public bool isDead;

    public static int highscore;
    public static int highscore2;
    public static int highscore3;

    public Text scoreText;

    void Start()
    {
        isDead = false;

        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score;

        PlayerPrefs.SetInt("score", 0);

        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("highscore2", 0);
        PlayerPrefs.SetInt("highscore3", 0);

        highscore = PlayerPrefs.GetInt("highscore", highscore);
        highscore2 = PlayerPrefs.GetInt("highscore2", highscore2);
        highscore3 = PlayerPrefs.GetInt("highscore3", highscore3);
    }
    void Update()
    {
        if (score > highscore3)
        {
            if (score >= highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
            }
            else if (isDead)
            {
                if (score >= highscore2)
                {
                    highscore2 = score;
                    PlayerPrefs.SetInt("highscore2", highscore2);
                }
                else
                {
                    highscore3 = score;
                    PlayerPrefs.SetInt("highscore3", highscore3);
                }
            }
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
    }

    public void Reset()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void SaveCurrentPoints()
    {
        PlayerPrefs.SetInt("score", score);
        scoreText.text = "Score: " + score;
    }
}
