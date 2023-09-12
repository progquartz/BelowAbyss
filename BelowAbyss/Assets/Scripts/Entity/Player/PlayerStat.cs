using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStat : EntityStat
{
    /// <summary>
    /// 다음 리스트들은 N회의 전투동안 유지되는 버프들의 리스트들이다.
    /// 버프들은 N회 전투동안 N만큼의 효과 유지로.
    /// </summary>
    public List<Tuple<int, int>> maxHpBuffs = new List<Tuple<int, int>>(); // 최대 체력이 N회의 전투동안 유지됨.
    public List<Tuple<int, int>> currentArmourBuffs = new List<Tuple<int, int>>(); // N턴동안 전투가 시작될때 N만큼의 방어력을 획득함.

    public List<Tuple<int, int>> allAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 공격피해 증가.
    public List<Tuple<int, int>> meleeAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 무기피해 증가.
    public List<Tuple<int, int>> skillAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 스킬피해 증가.

    public List<Tuple<int, int>> allHitBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 받는 모든 공격피해 증가.

    public int currentVital;
    public int maxVital;
    public int currentSatur;
    public int maxSatur;
    public int currentThirst; // 목마름 수치
    public int maxThirst;
    public int currentSanity; // 정신 수치 체력회복에 관여
    public int maxSanity;

    public void ResetPlayerStat()
    {
        currentVital = 100;
        maxVital = 100;
        currentSatur = 100;
        maxSatur = 100;
        currentThirst = 100; // 목마름 수치
        maxThirst = 100;
        currentSanity = 100; // 정신 수치 체력회복에 관여
        maxSanity = 100;

        maxHpBuffs = new List<Tuple<int, int>>(); 
        currentArmourBuffs = new List<Tuple<int, int>>(); 

        allAttackBuffs = new List<Tuple<int, int>>(); 
        skillAttackBuffs = new List<Tuple<int, int>>(); 
        meleeAttackBuffs = new List<Tuple<int, int>>(); 

        allHitBuffs = new List<Tuple<int, int>>();

        currentHp = 100;
        maxHp = 100;

        armour = 0;

        additionalAllDamage = 0; // 가하는 모든 데미지 추가.
        additionalWeaponDamage = 0; // 가하는 무기 데미지 추가.
        additionalSkillDamage = 0; // 가하는 스킬 데미지 추가

        poisionStack = 0; // 독 스택. 매 초 N만큼의 데미지를 줌.
        bloodStack = 0; // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음.
        onFire = false; // 불 붙음 여부. 불이 붙었을 경우 1초마다 정해진 만큼의 피해를 줌.
        additionalHitDamage = 0; // 피격 추가 데미지 여부.
    }

    public void CurrentThirstControl(int amount)
    {
        int tmpthirst = currentThirst + amount;
        if(tmpthirst > maxThirst)
        {
            tmpthirst = maxThirst;
        }
        else if(tmpthirst < 0)
        {
            tmpthirst = 0;
        }
        currentThirst = tmpthirst;
    }

    public void CurrentSaturControl(int amount)
    {
        int tmpthirst = currentSatur + amount;
        if (tmpthirst > maxSatur)
        {
            tmpthirst = maxSatur;
        }
        else if (tmpthirst < 0)
        {
            tmpthirst = 0;
        }
        currentSatur = tmpthirst;
    }

    public void CurrentSanityControl(int amount)
    {
        int tmpthirst = currentSanity + amount;
        if (tmpthirst > maxSanity)
        {
            tmpthirst = maxSanity;
        }
        else if (tmpthirst < 0)
        {
            tmpthirst = 0;
        }
        currentSanity = tmpthirst;
    }

    public void CurrentVitalControl(int amount)
    {
        int tmpthirst = currentVital + amount;
        if (tmpthirst > maxVital)
        {
            tmpthirst = maxVital;
        }
        else if (tmpthirst < 0)
        {
            tmpthirst = 0;
        }
        currentVital = tmpthirst;
    }



}
