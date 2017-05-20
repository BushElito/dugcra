using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public static class SaveAndLoadManager
{

    public static string windowsSaveLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    public static string gridSaveFolder = "Levels";

    public static string SaveLocation(string name, string worldName = "world")
    {
        string saveLocation = gridSaveFolder + "/" + worldName + "/";

        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }

        saveLocation += name + ".json";

        return saveLocation;
    }

    public static void CheckDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public static string SaveTo(string directory, string fileName)
    {
        string saveLocation = directory + "/" + fileName;
        return saveLocation;
    }

    //Grid saving and loading
    #region
    public static void SaveGrid(Grid grid, bool savePacked)
    {
        Save save = new Save();
        List<Save.Tiles> t = new List<Save.Tiles>();
        Save.Tiles ti;
        for (int x = 0; x < Grid.gridSize; x++)
        {
            for (int y = 0; y < Grid.gridSize; y++)
            {
                ti.tile = grid.tiles[x, y];
                ti.pos = new WorldPos(x, y);
                t.Add(ti);
            }
        }
        save.ToArray(t);

        File.WriteAllText(SaveLocation(grid.ToString(), LevelManager.levelName), save.saveToString(savePacked));
    }

    public static bool LoadGrid(Grid grid)
    {
        if (!File.Exists(SaveLocation(grid.ToString(), LevelManager.levelName)))
        {
            return false;
        }

        Stream file = new FileStream(SaveLocation(grid.ToString(), LevelManager.levelName), FileMode.Open);
        StreamReader text = new StreamReader(file);
        string t = text.ReadToEnd();
        file.Close();
        text.Close();


        Save save = JsonUtility.FromJson<Save>(t);

        try
        {
            foreach (var item in save.ToDictionary())
            {
                grid.tiles[item.Key.x, item.Key.y] = item.Value;
            }
        }
        catch (Exception)
        {
            Debug.Log(grid);
        }
        grid.update = true;

        return true;
    }
    #endregion

    public static void GetLevelConfig()
    {
        if (!File.Exists(gridSaveFolder + "/" + LevelManager.levelName + "/levelConfig.cfg"))
        {
            FileStream fi = new FileStream(gridSaveFolder + "/" + LevelManager.levelName + "/levelConfig.cfg", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fi);
            writer.WriteLine("gridSize=" + Grid.gridSize);
            writer.Write("unlocked=false");
            writer.Close();
            fi.Close();
        }

        FileStream f = new FileStream(gridSaveFolder + "/" + LevelManager.levelName + "/levelConfig.cfg", FileMode.Open);
        StreamReader reader = new StreamReader(f);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.StartsWith("gridSize="))
            {
                int index = line.IndexOf('=');
                Grid.gridSize = int.Parse(line.Substring(index + 1));
            }
        }
        f.Close();
        reader.Close();
    }

    public static Level GetLevelConfig(Level level)
    {
        if (!File.Exists(gridSaveFolder + "/" + level.levelName + "/levelConfig.cfg"))
        {
            FileStream fi = new FileStream(gridSaveFolder + "/" + level.levelName + "/levelConfig.cfg", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fi);
            writer.WriteLine("gridSize=" + Grid.gridSize);
            writer.Write("unlocked=" + level.unlocked);
            writer.Close();
            fi.Close();
        }

        FileStream f = new FileStream(gridSaveFolder + "/" + level.levelName + "/levelConfig.cfg", FileMode.Open);
        StreamReader reader = new StreamReader(f);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.StartsWith("gridSize="))
            {
                int index = line.IndexOf('=');
                level.size = int.Parse(line.Substring(index + 1));
            }
            if (line.StartsWith("unlocked="))
            {
                int index = line.IndexOf('=');
                level.unlocked = bool.Parse(line.Substring(index + 1));
            }
        }
        f.Close();
        reader.Close();
        return level;
    }

    public static void SaveLevelConfig(Level level)
    {
        File.WriteAllText(gridSaveFolder + "/" + LevelManager.levelName + "/levelConfig.cfg", string.Empty);
        FileStream f = new FileStream(gridSaveFolder + "/" + LevelManager.levelName + "/levelConfig.cfg", FileMode.OpenOrCreate);
        
        StreamWriter writer = new StreamWriter(f);                
        writer.WriteLine("gridSize=" + Grid.gridSize);
        writer.Write("unlocked=" + level.unlocked);
        writer.Close();
        f.Close();
    }
}
