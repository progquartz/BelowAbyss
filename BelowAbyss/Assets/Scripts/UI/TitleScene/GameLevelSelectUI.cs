using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameLevel
{
    level1,
    level2, 
    level3, 
    level4
}

[System.Serializable]
public class EscapeLevel
{
    public GameLevel level;
    public int clearLevel;
    public Button buttonToHandle;
}
public class GameLevelSelectUI : MonoBehaviour
{
    private string gameEndLevelKey = "GameEndLevel";
    public GameObject levelSelectUI;
    public EscapeLevel[] escapeLevels;

    private void Start()
    {
        // 모든 버튼에 이벤트 연결
        foreach (var escapeLevel in escapeLevels)
        {
            int clearLevel = escapeLevel.clearLevel;  // 람다 캡처 방지
            escapeLevel.buttonToHandle.onClick.AddListener(() => SetClearLevel(clearLevel));
        }
    }

    private void SetClearLevel(int level)
    {
        PlayerPrefs.SetInt(gameEndLevelKey, level);
        PlayerPrefs.Save();
        Debug.Log("세이브된거여~");
    }

    public void CloseUI()
    {
        levelSelectUI.SetActive(false);
    }

    public void OpenUI()
    {
        levelSelectUI.SetActive(true);
    }
}
