using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyStat stat;
    public EnemyVisual enemyVisual;

    private bool isEnemyHasAdditionalSkill = false;

    private float[] additionalSkillCooltime;
    private float attackCooltimeLeft;

    private void CheckStat()
    {
        if (stat.additionalEffect1.Length > 0)
        {
            isEnemyHasAdditionalSkill = true;
        }
        else
        {
            isEnemyHasAdditionalSkill = false;
        }

        additionalSkillCooltime = new float[stat.additionalEffect1.Length];
        attackCooltimeLeft = stat.attackSpeed;
    }

    public void CheckDeath()
    {
        if(stat.currentHp <= 0)
        {
            int realindex = 0;
            for(int i = 0; i < transform.GetSiblingIndex(); i++)
            {
                if (transform.parent.GetChild(i).gameObject.activeInHierarchy)
                {
                    realindex++;
                }
            }
            Debug.Log("실행됨?");
            stat.ResetStat();
            enemyVisual.DeathReset();
            enemyVisual.DeathAnimation();
            transform.GetComponentInParent<EnemyHord>().Death(realindex);
            stat.enemyCode = 0;
        }
    }

    private void Update()
    {
        if(stat.enemyCode != 0)
        {
            CheckDeath();
            if (BattleManager.instance.isBattleStarted)
            {
                CheckCooltime();
            }
        }
    }

    private void CheckCooltime()
    {
        if (attackCooltimeLeft <= 0.0f)
        {
            Attack();
            enemyVisual.AttackAnimationOn();
            BattleSoundManager.instance.PlaySound(stat.soundCode, 0.2f);
            attackCooltimeLeft = stat.attackSpeed;
            attackCooltimeLeft -= Time.deltaTime;
        }
        else
        {
            attackCooltimeLeft -= Time.deltaTime;
        }

        if (isEnemyHasAdditionalSkill)
        {
            UseSkill();
        }
        
    }

    private void Attack()
    {
        EffectData atkData = new EffectData("P_0_CH_-"+stat.attackDamage.ToString() ,"I_0_1_1", "F");
        EffectManager.instance.AmplifyEffect(atkData);

    }

    private void UseSkill()
    {
        for(int i = 0; i < additionalSkillCooltime.Length; i++)
        {
            EffectManager.instance.AmplifyEffect(new EffectData(stat.additionalEffect1[i], stat.additionalEffect2[i], stat.additionalEffect3[i]));
        }
    }

    private void Start()
    {
        enemyVisual = transform.GetComponentInChildren<EnemyVisual>();
        stat = transform.GetComponent<EnemyStat>();
    }

    public void ChangeEnemyData(EnemyFormat data)
    {
        // 체력 관련
        stat.maxHp = data.MaxHealth;
        stat.currentHp = data.MaxHealth;
        stat.size = data.Size;

        stat.additionalEffect1 = data.additionalEffect1;
        stat.additionalEffect2 = data.additionalEffect2;
        stat.additionalEffect3 = data.additionalEffect3;
        stat.additionalEffectCoolTime = data.additionalEffectCoolTime;

        stat.attackDamage = data.attackDamage;
        stat.attackSpeed = data.attackSpeed;

        stat.enemySpriteCode = data.enemySpriteCode;
        stat.enemyCode = data.EnemyCode;
        stat.soundCode = data.SoundCode;
        ChangeEnemyVisualData();
    }

    private void ChangeEnemyVisualData()
    {
        enemyVisual.ChangeSpriteLibrary(stat.enemySpriteCode);
    }
}
