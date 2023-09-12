using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EntityStat
{
    
    public int currentHp;
    public int maxHp;

    public int armour;

    /// <summary>
    /// 버프 리스트. 
    /// </summary>
    public int additionalAllDamage; // 가하는 모든 데미지 추가.
    public int additionalWeaponDamage; // 가하는 무기 데미지 추가.
    public int additionalSkillDamage; // 가하는 스킬 데미지 추가

    /// <summary>
    /// 디버프 리스트.
    /// </summary>

    public int poisionStack; // 독 스택. 매 초 N만큼의 데미지를 줌.
    public int bloodStack; // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음.
    public bool onFire; // 불 붙음 여부. 불이 붙었을 경우 1초마다 정해진 만큼의 피해를 줌.
    public int additionalHitDamage; // 피격 추가 데미지 여부.

    /// <summary>
    /// 체력을 변경시키는 함수. 음수도 가능함.
    /// 만약에 체력이 초과되었을 경우에는 그 수치만큼을.
    /// 만약에 체력이 0이하로 떨어지면 -1을, 일반에는 0을 리턴함.
    /// </summary>
    /// <param name="value">변경될 체력 수치.</param>
    /// <returns></returns>
    public int CurrentHealthControl(int amount)
    {
        currentHp += amount;
        if(currentHp > maxHp)
        {
            int delta = currentHp - maxHp;
            currentHp = maxHp;
            return delta;
        }
        else if(currentHp <= 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public void MaxHealthControl(int amount)
    {
        maxHp += amount;
    }

    public void CurrentArmourControl(int amount, double duration)
    {
        armour += amount;
    }

    public void DisarmArmour()
    {
        armour = 0;
    }

    public void AllAttackDamageControl(int amount, double duration)
    {
        additionalAllDamage += amount;
    }

    public void AllWeaponDamageControl(int amount, double duration)
    {
        additionalWeaponDamage += amount;
    }

    public void AllSkillDamageControl(int amount, double duration)
    {
        additionalSkillDamage += amount;
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






}
