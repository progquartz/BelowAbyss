using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
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

    public bool isPoisoned;
    // 독 스택. 매 초 Power만큼의 데미지를 줌. duration은 negative에서 테스트한다음에 다시 초기화됨.
    public BuffData poisonStack;

    public bool isBleeding;
    // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음. duration은 negative에서 테스트한다음에 다시 초기화됨.
    public BuffData bloodStack;

    public bool isOnFire; // 화염 효과. 매초 N의 데미지.
    public float fireDuration;
   
    public EntitySkillVisualCaller entitySkillVisualCaller;


    private void Start()
    {
        additionalAllDamage = new List<BuffData>();
        additionalWeaponDamage = new List<BuffData>();
        additionalSkillDamage = new List<BuffData>();
        additionalHitDamage = new List<BuffData>();
        poisonStack = new BuffData();
        bloodStack = new BuffData();
        entitySkillVisualCaller = GetComponentInChildren<EntitySkillVisualCaller>();
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
                Debug.Log("아야");
                entitySkillVisualCaller.CallBuffVisual("effectAniPoison");
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
                entitySkillVisualCaller.CallBuffVisual("effectAniBlood");
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
                entitySkillVisualCaller.CallBuffVisual("effectAniFire");
                CurrentHPControl(-EffectManager.instance.fireTickDamage);
            }
        }
    }


    private void RemoveBuffsAfterFight()
    {

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
    /// 전체 공격 데미지 컨트롤.
    /// </summary>
    /// <param name="amount"></param>
    public virtual void AllAttackDamageControl(int amount, char type, float duration)
    {
        switch (type)
        {
            case 'I':
                realAdditionalAllDamage += amount;
                break;
            case 'S':
                BuffData tmp = new BuffData(amount, duration);
                additionalAllDamage.Add(tmp);
                break;
        }
    }

    public virtual void AllHitDamageControl(int amount, char type, float duration)
    {
        switch (type)
        {
            case 'I':
                realAdditionalHitDamage += amount;
                break;
            case 'S':
                BuffData tmp = new BuffData(amount, duration);
                additionalHitDamage.Add(tmp);
                break;
        }
    }

    /// <summary>
    /// 체력을 변경시키는 함수. 음수도 가능함. 방어력부터 데미지가 들어감.
    /// 만약에 체력이 초과되었을 경우에는 그 수치만큼을.
    /// 만약에 체력이 0이하로 떨어지면 -1을, 일반에는 0을 리턴함.
    /// </summary>
    public virtual int CurrentHPControl(int amount)
    {
        int delta = 0;
        if (amount > 0)
        {
            currentHp += amount;
            delta = Math.Max(0, currentHp - maxHp);
            currentHp = Math.Min(currentHp, maxHp);
        }
        else if (amount < 0)
        {
            if (armour > 0)
            {
                armour += amount;
                if (armour < 0)
                {
                    currentHp += armour;
                    armour = 0;
                }
            }
            else
            {
                currentHp += amount;
            }

            if (currentHp <= 0)
            {
                delta = -1;
            }
        }

        return delta;
    }

    public virtual void MaxHPControl(int amount)
    {
        maxHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void CurrentArmourControl(int amount)
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

    public void Cure()
    {
        isPoisoned = false;
        poisonStack.buffPower = 0;
        poisonStack = new BuffData();
        isBleeding = false;
        bloodStack.buffPower = 0;
        bloodStack = new BuffData();
        isOnFire = false;
    }

    public void AddPoison(int amount)
    {
        if (poisonStack == null)
        {
            poisonStack = new BuffData();
        }
        poisonStack.buffPower += amount;
        bool isNewPoison = poisonStack.buffPower == amount;
        if (isNewPoison)
        {
            poisonStack.buffDuration = 1.0f;
        }
    }

    public void AddBlood(int amount)
    {
        if (bloodStack == null)
        {
            bloodStack = new BuffData();
        }

        bloodStack.buffPower += amount;
        bool isNewBlood = poisonStack.buffPower == amount;
        if (isNewBlood)
        {
            bloodStack.buffDuration = 1.0f;
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
