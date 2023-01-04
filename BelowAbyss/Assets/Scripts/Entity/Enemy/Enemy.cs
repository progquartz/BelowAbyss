using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStat stat;
    public EnemyStat battleStat; // 전투중에 이루어지는 모든 버프들 계산.    

    public SpriteRenderer sprite;

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

    ///<summary>
    /// 효과 문서.
    /// 현재 적에게 적용되는 효과들은 다음과 같음.
    /// 
    /// 체력 관련 - CH / MH / CA 
    /// 공격력 관련 = AA / MA / SA (버프 형식)
    /// 디버프 - PO / BD / FI / AH / DA
    /// 회복 - CU
    /// </summary>
    
    /// <summary>
    /// 현재 체력을 조정하는 함수.
    /// </summary>
    /// <param name="amount"></param>
    public void CurrentHealthControl(int amount)
    {
        stat.currentHp += amount;
        if(stat.currentHp < 0)
        {
            stat.currentHp = 0;
        }
        if(stat.currentHp > stat.maxHp)
        {
            stat.currentHp = stat.maxHp;
        }
    }

    /// <summary>
    /// 최대 체력을 조정하는 함수.
    /// </summary>
    /// <param name="amount"></param>
    public void MaxHealthControl(int amount, double duration)
    {
        stat.maxHp += amount;
    }

    public void MaxHealthControl(int amount)
    {

    }

    public void CurrentArmourControl(int amount, double duration)
    {
        stat.armour += amount;
    }

    public void DisarmArmour()
    {
        stat.armour = 0;
    }

    public void AllAttackDamageControl(int amount, double duration)
    {
        stat.additionalAllDamage += amount;
    }

    public void AllWeaponDamageControl(int amount, double duration)
    {
        stat.additionalWeaponDamage += amount;
    }

    public void AllSkillDamageControl(int amount, double duration)
    {
        stat.additionalSkillDamage += amount;
    }

    public void PoisionControl(int amount, double duration)
    {

    }

    public void BloodControl(int amount, double duration)
    {

    }

    public void FireControl(int amount, double duration)
    {

    }

    public void AllHitDamageControl(int amount, double duration)
    {

    }

    private void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    
}
