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
        Parser.instance.ParseAllData();
        /// 나중에 돌려놔야 함.
        //SkillInventory.instance.FirstSetup();
        //MapManager.Instance.FlushAllMapDatas();
        //MapManager.Instance.GenerateNextStage(true);
    }

    public void StartNewGame()
    {
        TempFirstSetup();
    }

}
