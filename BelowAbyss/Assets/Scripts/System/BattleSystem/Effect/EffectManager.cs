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

public enum EffectStatus
{
    NONE,
    CURHP,
    MAXHP,
    CURSAT,
    MAXSAT,
    CURARM,
    DISARM,
    ALLATK,
    MELATK,
    SKLATK,
    POISON,
    BLOOD,
    FIRE,
    CURE,
    ALLHIT,
    HEALTH,
    MIND
}

public enum EffectCountFor
{
    NONE,
    INSTANT,
    BATTLE,
    SECOND
}



public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;


    /// <summary>
    /// 첫번째 문절. 대상의 
    /// </summary>
    private EffectTarget target;
    private int targetCount; // 앞 또는 뒤 기준 n칸의 적.
    private EffectStatus status;

    private EffectCountFor effectIndicator;
    private int effectCount;
    private string playerGraphicEffectCode;
    private string enemyGraphicEffectCode;

    private bool needsCondition;
    // 조건에 대해서는 조금 있다가.



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void ResetTempDatas()
    {
        target = EffectTarget.NONE;
        targetCount = 0;
        status = EffectStatus.NONE;
        effectIndicator = EffectCountFor.NONE;
        effectCount = 0;
        playerGraphicEffectCode = null;
        enemyGraphicEffectCode = null;
        needsCondition = false;
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

        }
        else
        {
            Debug.Log("정상적이지 않은 str형식이 로딩되어서 effect가 정상적으로 발생되지 않았음.");
            return false;
        }

        return true;
        
    }

    private bool PutFirstLineDatas(string str)
    {
        string[] strs = str.Split('_');

        if (strs.Length != 3)
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
                status = EffectStatus.CURHP;
                break;
            case "MH":
                status = EffectStatus.MAXHP;
                break;
            case "CS":
                status = EffectStatus.CURSAT;
                break;
            case "MS":
                status = EffectStatus.MAXSAT;
                break;
            case "CA":
                status = EffectStatus.CURARM;
                break;
            case "DA":
                status = EffectStatus.DISARM;
                break;
            case "AA":
                status = EffectStatus.ALLATK;
                break;
            case "MA":
                status = EffectStatus.MELATK;
                break;
            case "SA":
                status = EffectStatus.SKLATK;
                break;
            case "PO":
                status = EffectStatus.POISON;
                break;
            case "BD":
                status = EffectStatus.BLOOD;
                break;
            case "FI":
                status = EffectStatus.FIRE;
                break;
            case "HT":
                status = EffectStatus.HEALTH;
                break;
            case "MI":
                status = EffectStatus.MIND;
                break;
            case "CU":
                status = EffectStatus.CURE;
                break;
            case "AH":
                status = EffectStatus.ALLHIT;
                break;
            default:
                status = EffectStatus.NONE;
                break;
        }
        
        if(status == EffectStatus.NONE || target == EffectTarget.NONE)
        {
            return false;
        }
        return true;
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
                effectIndicator = EffectCountFor.BATTLE;
                break;
            case "S":
                effectIndicator = EffectCountFor.SECOND;
                break;
            case "I":
                effectIndicator = EffectCountFor.INSTANT;
                break;
            default:
                effectIndicator = EffectCountFor.NONE;
                break;
        }

        effectCount = int.Parse(strs[1]);

        playerGraphicEffectCode = strs[2];
        enemyGraphicEffectCode = strs[3];

        if(effectIndicator == EffectCountFor.NONE)
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

}
