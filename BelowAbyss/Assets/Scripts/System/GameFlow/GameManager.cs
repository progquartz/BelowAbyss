using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // SingleTone
    [SerializeField]
    public GameOverUI gameOverUI;
    public bool isFirstGame = true;

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
        /// 첫 로딩 시에만 이렇게 로딩되게 하고, 이후 게임시에는 씬 로딩시 start로 알아서 잘 로딩될 것.
        if(isFirstGame)
        {
            Parser.instance.ParseAllData();
            MapManager.Instance. FlushAllMapDatas();
            MapManager.Instance.GenerateNextStage(true);
            gameOverUI = GameObject.Find("GameOverUIHolder").GetComponent<GameOverUI>();
            EffectManager.instance.enemyHordManager = BattleManager.instance.enemyHord;
            EffectManager.instance.playerStat = Player.instance;
        }
        //SkillInventory.instance.FirstSetup();
    }

    public void StartNewGame()
    {
        TempFirstSetup();
    }

    public void GameOver()
    {
        gameOverUI.OnGameOver();
        MapManager.Instance.OnGameOver();
        isFirstGame = false;
    }

}
