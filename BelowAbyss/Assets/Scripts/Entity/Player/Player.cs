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


    public void CheckDeath()
    {
        if (stat.currentHp <= 0)
        {
            BattleManager.instance.OnPlayerDeath();
        }
    }




}
