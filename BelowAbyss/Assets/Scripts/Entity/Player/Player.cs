using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerStat stat;
    public bool isPlayerDead = false;

    public PlayerVisualNoneAnimation visualNoneAnimation;
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
            GameManager.instance.GameOver();
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
