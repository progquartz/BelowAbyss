using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance; // SingleTone
    

    void Awake()
    {
        // Singletone
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        StartNewGame();
    }

    private void TempFirstSetup()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Parser.instance.ParseAllData();
        SkillInventory.instance.FirstSetup();
        MapManager.Instance.FlushAllMapDatas();
        MapManager.Instance.GenerateNextStage(true);
    }

    public void StartNewGame()
    {
        TempFirstSetup();
    }

    private void Update()
    {
        Cursor.visible = true;
    }

}
