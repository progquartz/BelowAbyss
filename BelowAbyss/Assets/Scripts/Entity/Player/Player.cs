using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerStat stat;
    public bool isPlayerDead = false;

    /// <summary>
    /// 전투 이전에 저장되어야 하는 
    /// </summary>
    public PlayerStat statBeforeBattle;
    [SerializeField]
    private PlayerVisualNoneAnimation visualNoneAnimation;
    [SerializeField]
    private PlayerMovingComponent visualAnimation;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            PlayerStatReset();
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

    public void PlayerStatReset()
    {
        stat.ResetPlayerStat();
    }


    public void CheckDeath()
    {
        if (stat.currentHp <= 0 && !isPlayerDead)
        {
            visualAnimation.DeathState();
            BattleManager.instance.OnPlayerDeath();
            PlayerStatReset();
            isPlayerDead = true;
        }
    }

    public void PlayerAttackAnimation()
    {
        visualAnimation.AttackState();
    }

    public void PlayerMoveToOriginalPosition()
    {
        visualAnimation.MoveToOriginalPos();
    }
    public void PlayerHurtAnimation()
    {
        visualAnimation.HurtState();
    }

}
