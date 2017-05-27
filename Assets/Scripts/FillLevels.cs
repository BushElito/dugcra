using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class FillLevels : MonoBehaviour
{
    List<string> directories;
    List<string> directoriesCL;
    public GameObject content;
    public GameObject contentCL;
    public GameObject levelPrefab;

    private void Awake()
    {
        SaveAndLoadManager.gridSaveFolder = SaveAndLoadManager.LEVELS_FOLDER;
        LevelManager.levels.Clear();
        if (!Directory.Exists(SaveAndLoadManager.LEVELS_FOLDER))
        {
            Directory.CreateDirectory(SaveAndLoadManager.LEVELS_FOLDER);
        }
        directories = new List<string>(Directory.GetDirectories(SaveAndLoadManager.LEVELS_FOLDER));
        directories.Sort(new NaturalStringComparer());
        foreach (var item in directories)
        {
            string levelName = item.Substring(SaveAndLoadManager.LEVELS_FOLDER.Length);
            Level level = SaveAndLoadManager.GetLevelConfig(new Level(levelName));
            LevelManager.levels.Add(level);
        }

        SaveAndLoadManager.gridSaveFolder = SaveAndLoadManager.CUSTOM_LEVELS_FOLDER;
        LevelManager.customLevels.Clear();
        if (!Directory.Exists(SaveAndLoadManager.CUSTOM_LEVELS_FOLDER))
        {
            Directory.CreateDirectory(SaveAndLoadManager.CUSTOM_LEVELS_FOLDER);
        }
        directoriesCL = new List<string>(Directory.GetDirectories(SaveAndLoadManager.CUSTOM_LEVELS_FOLDER));
        directoriesCL.Sort(new NaturalStringComparer());
        foreach (var item in directoriesCL)
        {
            string levelName = item.Substring(SaveAndLoadManager.CUSTOM_LEVELS_FOLDER.Length);
            Level level = SaveAndLoadManager.GetLevelConfig(new Level(levelName));
            LevelManager.customLevels.Add(level);
        }
    }

    private void OnEnable()
    {
        SaveAndLoadManager.gridSaveFolder = SaveAndLoadManager.LEVELS_FOLDER;
        foreach (var item in directories)
        {
            string levelName = item.Substring(SaveAndLoadManager.LEVELS_FOLDER.Length);
            GameObject level = Instantiate(levelPrefab, content.transform);
            level.transform.GetChild(0).GetComponent<Text>().text = levelName.Replace('_', ' ');
            var value = LevelManager.levels.Find(o => o.Equals(levelName));
            if (value != null)
            {
                if (value.unlocked)
                {
                    level.GetComponent<Button>().onClick.AddListener(delegate { Change_scene.ChangeToLevel(1, levelName, false); });
                }
                else
                {
                    level.transform.GetChild(0).GetComponent<Text>().text += " (locked)";
                }
            }
        }

        SaveAndLoadManager.gridSaveFolder = SaveAndLoadManager.CUSTOM_LEVELS_FOLDER;
        foreach (var item in directoriesCL)
        {
            string levelName = item.Substring(SaveAndLoadManager.CUSTOM_LEVELS_FOLDER.Length);
            GameObject level = Instantiate(levelPrefab, contentCL.transform);
            level.transform.GetChild(0).GetComponent<Text>().text = levelName.Replace('_', ' ');
            var value = LevelManager.customLevels.Find(o => o.Equals(levelName));
            if (value != null)
            {
                level.GetComponent<Button>().onClick.AddListener(delegate { Change_scene.ChangeToLevel(1, levelName, false, true); });
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform item in content.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in content.transform)
        {
            Destroy(item.gameObject);
        }
    }
}

[SuppressUnmanagedCodeSecurity]
internal static class SafeNativeMethods
{
    [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
    public static extern int StrCmpLogicalW(string psz1, string psz2);
}

public sealed class NaturalStringComparer : IComparer<string>
{
    public int Compare(string a, string b)
    {
        return SafeNativeMethods.StrCmpLogicalW(a, b);
    }
}

public sealed class NaturalFileInfoNameComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo a, FileInfo b)
    {
        return SafeNativeMethods.StrCmpLogicalW(a.Name, b.Name);
    }
}
