using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeDataBase : MonoBehaviour
{
    public static ThemeDataBase instance;

    public StageThemeDatas stageThemeDatas = new StageThemeDatas();
    public Themes themes = new Themes();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public ThemeData FindThemes(string themeName)
    {
        return themes.FindTheme(themeName);
    }
}

[System.Serializable]
public class StageThemeDatas
{
    public StageThemeData[] stageThemeDatas;
}

[System.Serializable]
public class Themes
{
    public ThemeData[] themes;

    public ThemeData FindTheme(string themeName)
    {
        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].themeName == themeName)
            {
                return themes[i];
            }
        }
        return null;
    }
}
