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

    public int GetThemeCode(string themeName)
    {
        return themes.FindThemeCode(themeName);
    }

    public int GetRandomEventFromTheme(string themeName)
    {
        int randomNum = Random.Range(0, themes.FindTheme(themeName).eventList.Length);
        Debug.Log(themes.FindTheme(themeName).eventList[randomNum] + "코드의 이벤트를 일반 방에 호출");
        return themes.FindTheme(themeName).eventList[randomNum];
    }

    public int GetRandomBossFromTheme(string themeName)
    {
        int randomNum = Random.Range(0, themes.FindTheme(themeName).lastRoomData.Length);
        Debug.Log(themes.FindTheme(themeName).lastRoomData[randomNum] + "코드의 이벤트를 보스 방에 호출");
        return themes.FindTheme(themeName).lastRoomData[randomNum];
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
            if (themes[i].themeCode == themeName)
            {
                return themes[i];
            }
        }
        return null;
    }

    public int FindThemeCode(string themeName)
    {
        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].themeCode == themeName)
            {
                return i;
            }
        }
        return -1;
    }
}
