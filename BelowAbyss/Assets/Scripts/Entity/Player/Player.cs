using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerStat stat;
    /// <summary>
    /// 전투 이전에 저장되어야 하는 
    /// </summary>
    public PlayerStat statBeforeBattle;
    public TextAsset firstPlayerStat;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        CheckDeath();

    }

    /// <summary>
    /// 맵의 이벤트 이동시마다 자동으로 줄어드는 허기 . 이동과 관련된 함수.
    /// </summary>
    public void MovingStatControl()
    {
        stat.MovingStatControl();
    }

    public void BattleEndStatControl()
    {
        stat.BattleEndStatControl();
    }

    public void CheckDeath()
    {
        if (stat.currentHp <= 0)
        {
            BattleManager.instance.OnPlayerDeath();
        }
        else if(stat.currentVitality <= 0)
        {
            BattleManager.instance.OnPlayerDeath();
        }
        else if(stat.currentSanity <= 0)
        {
            BattleManager.instance.OnPlayerDeath();
        }
    }


    private void Start()
    {
        //stat = new PlayerStat();
        stat = this.GetComponent<PlayerStat>();
    }




}
