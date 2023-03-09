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


    public int currentSatur; // 배고픔 수치.
    public int maxSatur;
    public int currentThirst; // 목마름 수치
    public int maxThirst;
    public int currentSanity; // 정신 수치 체력회복에 관여
    public int maxSanity;


    public void CurrentSatControl(int amount)
    {
        currentSatur += amount;
    }

    public void MaxSatControl(int amount)
    {
        maxSatur += amount;
        if(currentSatur > maxSatur)
        {
            currentSatur = maxSatur;
        }
    }

    public void currentThirstControl(int amount)
    {
        currentSatur += amount;
    }

    public void maxThirstControl(int amount)
    {
        maxThirst += amount;
        if(currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
        }
    }

    public void currentSanityControl(int amount)
    {
        currentSanity += amount;
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
