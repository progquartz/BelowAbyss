using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyStat stat;

    public SpriteRenderer sprite;

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


    public void UpdateSprite()
    {
        if (stat.enemySpriteCode == null)
        {
            sprite.sprite = null;
        }
        else
        {
            string path = "Sprites/Enemy/";
            Sprite image;
            image = Resources.Load<Sprite>((path + stat.enemySpriteCode + "_idle").ToString());
            sprite.sprite = image;
        }
    }



    private void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        stat = new EnemyStat();
    }
}
