using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuffData
{
    public int buffPower;
    public float buffDuration;

    public BuffData(int _power, float _duration)
    {
        buffPower = _power;
        buffDuration = _duration;
    }

    public BuffData()
    {
        buffPower = 0;
        buffDuration = 0;
    }
}

public class BuffBattleData
{
    public int buffPower;
    public int buffBattleCount;

    public BuffBattleData(int _power, int _buffBattle)
    {
        buffPower = _power;
        buffBattleCount = _buffBattle;
    }

    public BuffBattleData()
    {
        buffPower = 0;
        buffBattleCount = 0;
    }
}

[System.Serializable]
public class EntityStat : MonoBehaviour
{
    public int currentHp;

    public int maxHp;

    public int armour;

    // 실제 전투에서 적용되는 수치.
    public int realAdditionalAllDamage;
    public int realAdditionalMeleeDamage;
    public int realAdditionalSkillDamage;
    public int realAdditionalHitDamage;

    /// <summary>
    /// 버프 리스트. 
    /// </summary>
    // 초 단위 버프들. (영구지속은 9999로 표현)
    public List<BuffData> additionalAllDamage; // 가하는 모든 데미지 추가.
    public List<BuffData> additionalWeaponDamage; // 가하는 무기 데미지 추가.
    public List<BuffData> additionalSkillDamage; // 가하는 스킬 데미지 추가
    
    /// <summary>
    /// 디버프 리스트.
    /// </summary>
    // 초 단위 버프들. (영구지속은 9999로 표현)
    public List<BuffData> additionalHitDamage; // 피격 추가 데미지 여부.

    private bool isPoisoned;
    // 독 스택. 매 초 Power만큼의 데미지를 줌. duration은 negative에서 테스트한다음에 다시 초기화됨.
    public BuffData poisonStack; 

    private bool isBleeding;
    // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음. duration은 negative에서 테스트한다음에 다시 초기화됨.
    public BuffData bloodStack;

    public bool isOnFire; // 화염 효과. 매초 N의 데미지.
    private float fireDuration;


    private void Start()
    {
        additionalAllDamage = new List<BuffData>();
        additionalWeaponDamage = new List<BuffData>();
        additionalSkillDamage = new List<BuffData>();
        additionalHitDamage = new List<BuffData>();
        poisonStack = new BuffData();
        bloodStack = new BuffData();
    }
    private void Update()
    {
        RemoveExpiredBuffs();
        CheckAndEffectNegatives();
    }

    private void CheckAndEffectNegatives()
    {
        PoisonCheck();
        BleedingCheck();
        FireCheck();
    }

    private void PoisonCheck()
    {
        if (!isPoisoned && poisonStack.buffPower != 0)
        {
            isPoisoned = true;
        }

        if (isPoisoned)
        {
            poisonStack.buffDuration -= Time.deltaTime;
            if (poisonStack.buffDuration < 0)
            {
                poisonStack.buffDuration = 1.0f;
                CurrentHPControl(-poisonStack.buffPower);
            }
        }
    }

    private void BleedingCheck()
    {
        if (!isBleeding && bloodStack.buffPower != 0)
        {
            isBleeding = true;
        }

        if (isBleeding)
        {
            bloodStack.buffDuration -= Time.deltaTime;
            if (bloodStack.buffDuration < 0)
            {
                bloodStack.buffDuration = 1.0f;
                CurrentHPControl(-bloodStack.buffPower);
            }
        }
    }

    private void FireCheck()
    {
        if (!isOnFire && fireDuration != 0)
        {
            isOnFire = true;

        }
        if (isOnFire)
        {
            fireDuration -= Time.deltaTime;
            if (fireDuration < 0)
            {
                fireDuration = 1.0f;
                CurrentHPControl(EffectManager.instance.FireTickDamage);
            }
        }
    }


    /// <summary>
    /// 시간이 지난 초 단위의 버프 / 디버프를 배제하는 함수.
    /// </summary>
    private void RemoveExpiredBuffs()
    {
        RemoveExpiredBuffs(additionalAllDamage, ref realAdditionalAllDamage);
        RemoveExpiredBuffs(additionalWeaponDamage, ref realAdditionalMeleeDamage);
        RemoveExpiredBuffs(additionalSkillDamage, ref realAdditionalSkillDamage);
        RemoveExpiredBuffs(additionalHitDamage, ref realAdditionalHitDamage);
    }

    private void RemoveExpiredBuffs(List<BuffData> target, ref int realBuffCount)
    {
        float deltaTime = Time.deltaTime;
        target.RemoveAll(data => {
            data.buffDuration -= deltaTime;
            return data.buffDuration <= 0;
        });

        realBuffCount = 0;
        foreach (var buff in target)
        {
            realBuffCount += buff.buffPower;
        }
    }

    /// <summary>
    /// 체력을 변경시키는 함수. 음수도 가능함.
    /// 만약에 체력이 초과되었을 경우에는 그 수치만큼을.
    /// 만약에 체력이 0이하로 떨어지면 -1을, 일반에는 0을 리턴함.
    /// </summary>
    public virtual int CurrentHPControl(int amount)
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

    public virtual void MaxHPControl(int amount)
    {
        maxHp += amount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void CurrentArmourControl(int amount, double duration)
    {
        armour += amount;
    }

    public void DisarmArmour()
    {
        armour = 0;
    }


    public void AllDamageControl(int amount, float duration)
    {
        additionalAllDamage.Add(new BuffData(amount, duration));
        realAdditionalAllDamage += amount;
    }

    public void AllWeaponDamageControl(int amount, float duration)
    {
        additionalWeaponDamage.Add(new BuffData(amount, duration));
        realAdditionalMeleeDamage += amount;
    }

    public void AllSkillDamageControl(int amount, float duration)
    {
        additionalSkillDamage.Add(new BuffData(amount, duration));
        realAdditionalSkillDamage += amount;
    }

    public void AllHitDamageControl(int amount, float duration)
    {
        additionalHitDamage.Add(new BuffData(amount, duration));
        realAdditionalHitDamage += amount;
    }

    public void AddPoison(int amount)
    {
        if (poisonStack != null)
        {
            poisonStack.buffPower += amount;
            bool isNewPoison = poisonStack.buffPower == amount;
            if (isNewPoison)
            {
                poisonStack.buffDuration = 1.0f;
            }
        }
    }

    public void AddBlood(int amount)
    {
        if(bloodStack != null)
        {
            bloodStack.buffPower += amount;
            bool isNewBlood = poisonStack.buffPower == amount;
            if(isNewBlood)
            {
                bloodStack.buffDuration = 1.0f;
            }
        }
    }

    public void AddFire(int amount)
    {
        if (!isOnFire)
        {
            fireDuration = 1.0f;
        }
    }








}
