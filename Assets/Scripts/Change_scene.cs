﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    public ScoreManager scoreManager;

    public void ChangeToScene(int sceneToChangeTo)
    {
        if (scoreManager != null)
        {
            scoreManager.isDead = true;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public static void ChangeToLevel(int levelScene, string levelName, bool random)
    {
        LevelManager.levelName = levelName;
        LevelManager.isRandom = random;

        Time.timeScale = 1;
        SceneManager.LoadScene(levelScene);
    }

    public void ToRandomLevel()
    {
        LevelManager.levelName = "";
        LevelManager.isRandom = true;

        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ToFirstLevel()
    {
        try
        {
            LevelManager.levelName = LevelManager.levels[0].ToString();
            LevelManager.isRandom = false;
        }
        catch (System.Exception)
        {
            LevelManager.isRandom = true;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        try
        {
            if (LevelManager.levelIndex + 1 < LevelManager.levels.Count)
            {
                LevelManager.levelName = LevelManager.levels[LevelManager.levelIndex + 1].ToString();
            }

            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        catch (System.Exception)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void ClearLevels()
    {
        SaveAndLoadManager.ClearAllLevelConfigs();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
