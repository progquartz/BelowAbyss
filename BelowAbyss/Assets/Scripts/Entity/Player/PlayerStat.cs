using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStat : EntityStat
{
    /// <summary>
    /// 다음 리스트들은 N회의 전투동안 유지되는 버프들의 리스트들이다.
    /// 버프들은 N회 전투동안 N만큼의 효과 유지로.
    /// </summary>
    public List<BuffBattleData> curArmBBuffs = new List<BuffBattleData>(); // N턴동안 전투가 시작될때 N만큼의 방어력을 획득함.


    // 전투 횟수동안 지속되는 경우.
    public List<BuffBattleData> allAtkBBuffs = new List<BuffBattleData>(); // N회의 전투동안 가하는 모든 공격피해 증가.
    public List<BuffBattleData> melAtkBBuffs = new List<BuffBattleData>(); // N회의 전투동안 가하는 모든 무기피해 증가.
    public List<BuffBattleData> skiAtkBBuffs = new List<BuffBattleData>(); // N회의 전투동안 가하는 모든 스킬피해 증가.

    public List<BuffBattleData> allHitBBuffs = new List<BuffBattleData>(); // N회의 전투동안 받는 모든 공격피해 증가.

    public int vitalityMaxHealth;
    public int normalMaxHealth;

    public int currentSatur; // 배고픔 수치.
    public int maxSatur;
    public int currentThirst; // 목마름 수치
    public int maxThirst;
    public int currentVitality; // 활력 수치.
    public int maxVitality;
    public int currentSanity; // 정신 수치 체력회복에 관여
    public int maxSanity;


    private void Update()
    {
        vitalityMaxHealth = currentVitality;
        maxHp =  normalMaxHealth + vitalityMaxHealth;

    }


    public void BattleEndStatControl()
    {
        CurrentSanityControl(10);
    }

    public void MovingStatControl()
    {
        CurrentSatControl(-5);
        CurrentThirstControl(-10);

        // vitality control
        MovingVitalityDecreasing();
        // sanity control
        MovingSanityDecreasing();
    }

    private void MovingVitalityDecreasing()
    {
        if(currentSatur >= 60)
        {
            CurrentVitalityControl(3);
        }

        if(currentThirst <= 5)
        {
            CurrentVitalityControl(-20);
        }
        else if(currentThirst <= 20)
        {
            CurrentVitalityControl(-5);
        }

        if(currentSatur <= 5)
        {
            CurrentVitalityControl(-8);
        }
        else if(currentSatur <= 20)
        {
            CurrentVitalityControl(-3);
        }
    }

    private void MovingSanityDecreasing()
    {
        if(currentSatur >= 75)
        {
            CurrentSanityControl(3);
        }

        if(currentSatur <= 20)
        {
            CurrentSanityControl(-3);
        }
        
        if(currentThirst <= 40)
        {
            CurrentSanityControl(-5);
        }
    }

    public override int CurrentHPControl(int amount)
    {
        currentHp += amount;
        if (currentHp > maxHp)
        {
            int delta = currentHp - maxHp;
            currentHp = maxHp;
            return delta;
        }
        else if (currentHp <= 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public override void MaxHPControl(int amount)
    {
        normalMaxHealth += amount;
        maxHp = normalMaxHealth + vitalityMaxHealth;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }


    public void MaxVitalityControl(int amount)
    {
        maxVitality += amount;
        if(currentVitality > maxVitality)
        {
            currentVitality = maxVitality;
        }
    }

    public void CurrentVitalityControl(int amount)
    {
        currentVitality += amount;
        if(currentVitality > maxVitality)
        {
            currentVitality = maxVitality;
        }
        vitalityMaxHealth = currentVitality;
        maxHp = normalMaxHealth + vitalityMaxHealth;
    }

    public void CurrentSatControl(int amount)
    {
        currentSatur += amount;
        if (currentSatur >= maxSatur)
        {
            currentSatur = maxSatur;
        }
        else if (currentSatur < 0)
        {
            currentSatur = 0;
        }
    }

    public void MaxSatControl(int amount)
    {
        maxSatur += amount;
        if(currentSatur > maxSatur)
        {
            currentSatur = maxSatur;
        }
    }

    public void CurrentThirstControl(int amount)
    {
        currentSatur += amount;
    }

    public void MaxThirstControl(int amount)
    {
        maxThirst += amount;
        if(currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
        }
    }

    public void CurrentSanityControl(int amount)
    {
        currentSanity += amount;
        if(currentSanity >= maxSanity)
        {
            currentSanity = maxSanity;
        }
    }

    public void MaxsanityControl(int amount)
    {
        maxSanity += amount;
        if(currentSanity > maxSanity)
        {
            currentSanity = maxSanity;
        }
    }

    /// <summary>
    /// 전투가 시작할때 플레이어가 받고 있는 모든 전투 단위(B) 버프를 받게 함.
    /// </summary>
    public void CheckBattleBuff()
    {
        InstallAllBattleBuffs();
    }

    public void EndBattleBuff()
    {
        UninstallAllSecondBuffs();
        UninstallAllBattleBuffs();
    }

    private void InstallAllBattleBuffs()
    {
        InstallBattleBuff(ref armour, curArmBBuffs);
        InstallBattleBuff(ref realAdditionalMeleeDamage, melAtkBBuffs);
        InstallBattleBuff(ref realAdditionalAllDamage, allAtkBBuffs);
        InstallBattleBuff(ref realAdditionalSkillDamage, skiAtkBBuffs);
        InstallBattleBuff(ref realAdditionalHitDamage, allHitBBuffs);
    }

    private void UninstallAllBattleBuffs()
    {
        UninstallBattleBuff(ref armour, curArmBBuffs);
        armour = 0;
        UninstallBattleBuff(ref realAdditionalMeleeDamage, melAtkBBuffs);
        UninstallBattleBuff(ref realAdditionalAllDamage, allAtkBBuffs);
        UninstallBattleBuff(ref realAdditionalSkillDamage, skiAtkBBuffs);
        UninstallBattleBuff(ref realAdditionalHitDamage, allHitBBuffs);
    }

    private void UninstallAllSecondBuffs()
    {
        UninstallSecondBuff(ref realAdditionalAllDamage, additionalAllDamage);
        UninstallSecondBuff(ref realAdditionalHitDamage, additionalHitDamage);
        UninstallSecondBuff(ref realAdditionalSkillDamage, additionalSkillDamage);
        UninstallSecondBuff(ref realAdditionalMeleeDamage, additionalWeaponDamage);
    }

    private void UninstallSecondBuff(ref int realbuff, List<BuffData> buffDatas)
    {
        int i = 0;
        while (i < buffDatas.Count)
        {
            realbuff -= buffDatas[i].buffPower;
            buffDatas.RemoveAt(i);
        }
    }

    private void UninstallBattleBuff(ref int realbuff, List<BuffBattleData> buffdatas)
    {
        int i = 0;
        while (i < buffdatas.Count)
        {
            if (buffdatas[i].buffBattleCount == 0)
            {
                realbuff -= buffdatas[i].buffPower;
                buffdatas.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }
    
    private void InstallBattleBuff(ref int realbuff, List<BuffBattleData> buffDatas)
    {
        for (int i = 0; i < buffDatas.Count; i++)
        {
            realbuff += buffDatas[i].buffPower;
            buffDatas[i].buffBattleCount--;
        }
    }



}
