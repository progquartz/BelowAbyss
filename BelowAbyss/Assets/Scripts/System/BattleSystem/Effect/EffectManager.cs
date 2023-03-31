using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTarget
{
    NONE,
    PLAYER,
    FRONT,
    BACK,
    ALL,
    REALALL
}

public enum EffectCountFor
{
    NONE,
    CURHP,
    MAXHP,
    CURSAT,
    MAXSAT,
    CURTHR,
    MAXTHR,
    CURARM,
    DISARM,
    ALLATK,
    MELATK,
    SKLATK,
    POISON,
    POIDEL,
    BLOOD,
    FIRE,
    CURE,
    ALLHIT,
    CURVIT,
    MAXVIT,
    SANITY
}

public enum EffectType
{
    NONE,
    INSTANT,
    BATTLE,
    SECOND
}



public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public EnemyHord enemyHordManager;
    public Player playerStat;

    public EffectData testData;

    
    /// <summary>
    /// 첫번째 문절. 대상의 
    /// </summary>
    ///
    [SerializeField]
    private EffectTarget target;
    [SerializeField]
    private int targetCount; // 앞 또는 뒤 기준 n칸의 적.
    [SerializeField]
    private EffectCountFor effData;
    [SerializeField]
    private int effectPower;

    [SerializeField]
    private EffectType effectType;
    [SerializeField]
    private float effectDuration;

    private string playerGraphicEffectCode;
    private string enemyGraphicEffectCode;

    private bool needsCondition;
    // 조건에 대해서는 조금 있다가.

    public int FireTickDamage = 5; // 불의 틱당 데미지 설정.



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void ResetTempDatas()
    {
        target = EffectTarget.NONE;
        targetCount = 0;
        effData = EffectCountFor.NONE;
        effectPower = 0;
        effectType = EffectType.NONE;
        effectDuration = 0;
        playerGraphicEffectCode = null;
        enemyGraphicEffectCode = null;
        needsCondition = false;
    }

    // 과거 데이터. 수정됨. 문제있으면 부활시킬것.
    /*private bool PutFirstLineDatas(string str)
    {
        string[] strs = str.Split('_');

        if (strs.Length != 4)
        {
            return false; // 정상적인 str 데이터가 들어오지 않음.
        }
        switch (strs[0])
        {
            case "A":
                target = EffectTarget.ALL;
                break;
            case "P":
                target = EffectTarget.PLAYER;
                break;
            case "F":
                target = EffectTarget.FRONT;
                break;
            case "B":
                target = EffectTarget.BACK;
                break;
            case "R":
                target = EffectTarget.REALALL;
                break;
            default:
                target = EffectTarget.NONE;
                break;
        }

        targetCount = int.Parse(strs[1]);

        switch (strs[2])
        {
            case "CH":
                effData = EffectCountFor.CURHP;
                break;
            case "MH":
                effData = EffectCountFor.MAXHP;
                break;
            case "CS":
                effData = EffectCountFor.CURSAT;
                break;
            case "MS":
                effData = EffectCountFor.MAXSAT;
                break;
            case "CA":
                effData = EffectCountFor.CURARM;
                break;
            case "DA":
                effData = EffectCountFor.DISARM;
                break;
            case "AA":
                effData = EffectCountFor.ALLATK;
                break;
            case "MA":
                effData = EffectCountFor.MELATK;
                break;
            case "SA":
                effData = EffectCountFor.SKLATK;
                break;
            case "PO":
                effData = EffectCountFor.POISON;
                break;
            case "BD":
                effData = EffectCountFor.BLOOD;
                break;
            case "FI":
                effData = EffectCountFor.FIRE;
                break;
            case "HT":
                effData = EffectCountFor.HEALTH;
                break;
            case "MI":
                effData = EffectCountFor.SANITY;
                break;
            case "CU":
                effData = EffectCountFor.CURE;
                break;
            case "AH":
                effData = EffectCountFor.ALLHIT;
                break;
            default:
                effData = EffectCountFor.NONE;
                break;
        }

        effectPower = int.Parse(strs[3]);

        if (effData == EffectCountFor.NONE || target == EffectTarget.NONE)
        {
            return false;
        }
        return true;
    }*/

    /// <summary>
    /// 주어진 3줄의 str 기반 데이터들을 실행.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private bool PutFirstLineDatas(string str)
    {
        string[] strs = str.Split('_');

        if (strs.Length != 4)
        {
            return false; // 정상적인 str 데이터가 들어오지 않음.
        }

        // Use a dictionary to map string values to enum values.
        Dictionary<string, EffectTarget> targetMap = new Dictionary<string, EffectTarget>()
        {
            { "A", EffectTarget.ALL },
            { "P", EffectTarget.PLAYER },
            { "F", EffectTarget.FRONT },
            { "B", EffectTarget.BACK },
            { "R", EffectTarget.REALALL }
        };

        Dictionary<string, EffectCountFor> countMap = new Dictionary<string, EffectCountFor>()
        {
            { "CH", EffectCountFor.CURHP },
            { "MH", EffectCountFor.MAXHP },
            { "CS", EffectCountFor.CURSAT },
            { "MS", EffectCountFor.MAXSAT },
            { "CT", EffectCountFor.CURTHR },
            { "MT", EffectCountFor.MAXTHR },
            { "CA", EffectCountFor.CURARM },
            { "DA", EffectCountFor.DISARM },
            { "AA", EffectCountFor.ALLATK },
            { "MA", EffectCountFor.MELATK },
            { "SA", EffectCountFor.SKLATK },
            { "PO", EffectCountFor.POISON },
            { "BD", EffectCountFor.BLOOD },
            { "FI", EffectCountFor.FIRE },
            { "CV", EffectCountFor.CURVIT },
            { "MV", EffectCountFor.MAXVIT },
            { "MI", EffectCountFor.SANITY },
            { "CU", EffectCountFor.CURE },
            { "AH", EffectCountFor.ALLHIT }
        };

        if (!targetMap.TryGetValue(strs[0], out target))
        {
            target = EffectTarget.NONE;
        }

        // Combine multiple if statements into one.
        if (!countMap.TryGetValue(strs[2], out effData) || target == EffectTarget.NONE)
        {
            return false;
        }

        int.TryParse(strs[1], out targetCount);
        int.TryParse(strs[3], out effectPower);

        return effData != EffectCountFor.NONE;
    }


    private bool PutSecondLineDatas(string str)
    {
        string[] strs = str.Split('_');

        if (strs.Length != 4)
        {
            return false; // 정상적인 str 데이터가 들어오지 않음.
        }

        switch (strs[0])
        {
            case "B":
                effectType = EffectType.BATTLE;
                break;
            case "S":
                effectType = EffectType.SECOND;
                break;
            case "I":
                effectType = EffectType.INSTANT;
                break;
            default:
                effectType = EffectType.NONE;
                break;
        }

        effectDuration = float.Parse(strs[1]);

        playerGraphicEffectCode = strs[2];
        enemyGraphicEffectCode = strs[3];

        if (effectType == EffectType.NONE)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 아직 완성 안됨. 조건부 부분은 이후 처리되어야만 함.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private bool PutThirdLineDatas(string str)
    {
        string[] strs = str.Split('_');
        needsCondition = false;
        return true;
    }

    public void AmplifyEffect(string s1, string s2, string s3)
    {
        EffectData eff = new EffectData(s1, s2, s3);
        AmplifyEffect(eff);
    }
    public bool AmplifyEffect(EffectData effect)
    {

        ResetTempDatas();
        bool dataValid = true;

        dataValid &= PutFirstLineDatas(effect.str1);
        dataValid &= PutSecondLineDatas(effect.str2);
        dataValid &= PutThirdLineDatas(effect.str3);

        if (dataValid)
        {
            Debug.Log(effect.str1 + "에 대한 효과 발동함.");
            PutEffect();
        }
        else
        {
            Debug.Log("정상적이지 않은 str형식이 로딩되어서 effect가 정상적으로 발생되지 않았음.");
            return false;
        }

        return true;
        
    }

    public void TestToPutEffect()
    {
        AmplifyEffect(testData);
    }

    public void PutEffect()
    {
        switch (effData) // 만약 플레이어에게 버프 단위로 들어가는 스킬들 (전투 단위 버프)은 전부 PlayerStat의 List에 들어갈것.
        {
            case EffectCountFor.CURHP: // P,E [I]
                CurHPChange();
                break;
            case EffectCountFor.MAXHP: //  P,E [I]
                MaxHPChange();
                break;
            case EffectCountFor.CURSAT: //  P [I]
                CurSatChange();
                break;
            case EffectCountFor.MAXSAT: // P [I]
                MaxSatChange();
                break;
            case EffectCountFor.CURTHR: // P [I]
                CurThrChange();
                break;
            case EffectCountFor.MAXTHR: // P [I]
                MaxThrChange();
                break;
            case EffectCountFor.CURARM: // P,E [I]
                CurArmourChange();
                break;
            case EffectCountFor.DISARM: // P,E [I]
                DisArm();
                break;
            case EffectCountFor.ALLATK: // p [I,S,B] / E [I, S]
                AllAttackDamageChange();
                break;
            case EffectCountFor.MELATK: // p [I,S,B] / E [I, S]
                break;
            case EffectCountFor.SKLATK: // p [I,S,B] / E [I, S]
                break;
            case EffectCountFor.POISON: // P,E [I]
                PoisonChange();
                break;
            case EffectCountFor.BLOOD: // P,E [I]
                BloodChange();
                break;
            case EffectCountFor.FIRE: // P,E [I]
                FireChange();
                break;
            case EffectCountFor.CURVIT: // P [I]
                CurrentVitalityChange();
                break;
            case EffectCountFor.MAXVIT: // P [I]
                MaxVitalityChange();
                break;
            case EffectCountFor.SANITY: // P [I]
                SanityChange();
                break;
            case EffectCountFor.CURE: // P,E [I]
                Cure();
                break;
            case EffectCountFor.ALLHIT: // p [I,S,B] / E [I, S] 
                break;
            case EffectCountFor.NONE:
                break;
        }

    }

    // p [I,S,B] / E [I, S]
    private void AllAttackDamageChange()
    {
        if(effectType == EffectType.INSTANT)
        {
            List<EntityStat> target = GetTarget();
            for (int i = 0; i < target.Count; i++)
            {
                target[i].AllAttackDamageControl(effectPower, 'I', 0.0f);
            }
        }
        else if(effectType == EffectType.BATTLE)
        {
            List<EntityStat> target = GetTarget();
            for (int i = 0; i < target.Count; i++)
            {
                target[i].AllAttackDamageControl(effectPower, 'B', effectDuration);
            }
        }
        else if(effectType == EffectType.SECOND)
        {
            List<EntityStat> target = GetTarget();
            for (int i = 0; i < target.Count; i++)
            {
                target[i].AllAttackDamageControl(effectPower, 'S', effectDuration);
            }
        }
    }

    private void Cure()
    {
        List<EntityStat> target = GetTarget();
        for (int i = 0; i < target.Count; i++)
        {
            target[i].Cure();
        }
    }
    // P,E [I]
    private void PoisonChange()
    {
        List<EntityStat> target = GetTarget();
        
        for (int i = 0; i < target.Count; i++)
        {
            target[i].AddPoison(effectPower);
        }
    }

    // P,E [I]
    private void BloodChange()
    {
        List<EntityStat> target = GetTarget();
        for (int i = 0; i < target.Count; i++)
        {
            target[i].AddBlood(effectPower);
        }
        
    }

    // P,E [I]
    private void FireChange()
    {
        List<EntityStat> target = GetTarget();
        for (int i = 0; i < target.Count; i++)
        {
            target[i].AddFire(1);
        }
    }
    // P [I]
    private void MaxThrChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.MaxThirstControl(effectPower);
    }
    // P [I]
    private void CurThrChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.CurrentThirstControl(effectPower);
    }
    // P [I]
    private void MaxSatChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.MaxSatControl(effectPower);
    }
    // P [I]
    private void CurSatChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.CurrentSatControl(effectPower);

    }
    // P [I]
    private void SanityChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.CurrentSanityControl(effectPower);
    }
    // P [I]
    private void CurrentVitalityChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.CurrentVitalityControl(effectPower);
    }
    // P [I]
    private void MaxVitalityChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.MaxVitalityControl(effectPower);
    }
    private void CurArmourChange()
    {
        List<EntityStat> target = GetTarget();
        for (int i = 0; i < target.Count; i++)
        {
            target[i].CurrentArmourControl(effectPower);
        }
    }
    private void DisArm()
    {
        List<EntityStat> target = GetTarget();
        for (int i = 0; i < target.Count; i++)
        {
            target[i].DisarmArmour();
        }

    }
    // P,E [I]
    private void MaxHPChange()
    {
        List<EntityStat> target = GetTarget();
        for(int i = 0; i < target.Count; i++)
        {
            target[i].MaxHPControl(effectPower); 
        }
    }

    // P,E [I]
    // 순수하게 데미지를 주는 목적이 강함.
    private bool CurHPChange()
    {
        List<EntityStat> target = GetTarget();
        for(int i = 0; i < target.Count; i++)
        {
            target[i].CurrentHPControl(effectPower);
        }
        return false;
    }

    private List<EntityStat> GetTarget()
    {
        List<EntityStat> data = new List<EntityStat>();
        if (target == EffectTarget.PLAYER)
        {
            data.Add(playerStat.stat);
            return data;
        }
        else if (target == EffectTarget.FRONT)
        {
            List<int> indexData = enemyHordManager.GetRowFromFrontHord(targetCount);
            for (int i = 0; i < indexData.Count; i++)
            {
                data.Add(enemyHordManager.enemies[indexData[i]].stat);
            }
            return data;
        }
        else if (target == EffectTarget.BACK)
        {
            List<int> rowData = enemyHordManager.GetRowFromBackHord(targetCount);
            for (int i = 0; i < rowData.Count; i++)
            {
                data.Add(enemyHordManager.enemies[rowData[i]].stat);
            }
            return data;
        }
        else if (target == EffectTarget.ALL)
        {
            List<int> rowData = enemyHordManager.GetAllFromHord();
            for (int i = 0; i < rowData.Count; i++)
            {
                data.Add(enemyHordManager.enemies[rowData[i]].stat);
            }
            return data;
        }
        else if(target == EffectTarget.REALALL)
        {
            List<int> rowData = enemyHordManager.GetAllFromHord();
            for (int i = 0; i < rowData.Count; i++)
            {
                data.Add(enemyHordManager.enemies[rowData[i]].stat);
            }
            data.Add(playerStat.stat);
            return data;
        }
        else
        {
            return null;
        }
    }
   



}
