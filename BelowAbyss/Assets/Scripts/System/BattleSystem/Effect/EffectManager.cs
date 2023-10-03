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

public enum EffectType
{
    NONE,
    CURHP,
    MAXHP,
    CURSAT,
    MAXSAT,
    CURARM,
    CURTHR,
    MAXTHR,
    CURMIN,
    MAXMIN,
    CURVIT,
    MAXVIT,
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


    public EnemyHord enemyHordManager;
    public Player playerStat;

    public EffectData testData;
    public int fireTickDamage = 5;
    
    /// <summary>
    /// 첫번째 문절. 대상의 
    /// </summary>
    ///
    [SerializeField]
    private EffectTarget target;
    [SerializeField]
    private int targetCount; // 앞 또는 뒤 기준 n칸의 적.
    [SerializeField]
    private EffectType status;
    [SerializeField]
    private int effectPower;

    [SerializeField]
    private EffectCountFor effectIndicator;
    [SerializeField]
    private int effectDuration;

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
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (!GameManager.instance.isFirstGame)
        {
            enemyHordManager = BattleManager.instance.enemyHord;
            playerStat = Player.instance;
        }
    }



    private void ResetTempDatas()
    {
        target = EffectTarget.NONE;
        targetCount = 0;
        status = EffectType.NONE;
        effectPower = 0;
        effectIndicator = EffectCountFor.NONE;
        effectDuration = 0;
        playerGraphicEffectCode = null;
        enemyGraphicEffectCode = null;
        needsCondition = false;
    }

    private bool PutFirstLineDatas(string str)
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
                status = EffectType.CURHP;
                break;
            case "MH":
                status = EffectType.MAXHP;
                break;
            case "CS":
                status = EffectType.CURSAT;
                break;
            case "MS":
                status = EffectType.MAXSAT;
                break;
            case "CT":
                status = EffectType.CURTHR;
                break;
            case "MT":
                status = EffectType.MAXTHR;
                break;
            case "CA":
                status = EffectType.CURARM;
                break;
            case "DA":
                status = EffectType.DISARM;
                break;
            case "AA":
                status = EffectType.ALLATK;
                break;
            case "MA":
                status = EffectType.MELATK;
                break;
            case "SA":
                status = EffectType.SKLATK;
                break;
            case "PO":
                status = EffectType.POISON;
                break;
            case "BD":
                status = EffectType.BLOOD;
                break;
            case "FI":
                status = EffectType.FIRE;
                break;
            case "CV":
                status = EffectType.CURVIT;
                break;
            case "MV":
                status = EffectType.MAXVIT;
                break;
            case "MI":
                status = EffectType.CURMIN;
                break;
            case "MM":
                status = EffectType.MAXMIN;
                break;
            case "CU":
                status = EffectType.CURE;
                break;
            case "AH":
                status = EffectType.ALLHIT;
                break;
            default:
                status = EffectType.NONE;
                break;
        }

        effectPower = int.Parse(strs[3]);

        if (status == EffectType.NONE || target == EffectTarget.NONE)
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

        effectDuration = int.Parse(strs[1]);

        playerGraphicEffectCode = strs[2];
        enemyGraphicEffectCode = strs[3];

        if (effectIndicator == EffectCountFor.NONE)
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
        Debug.Log(effect.str1 + "에 대한 효과 발동함.");
        ResetTempDatas();
        bool dataValid = true;

        dataValid &= PutFirstLineDatas(effect.str1);
        dataValid &= PutSecondLineDatas(effect.str2);
        dataValid &= PutThirdLineDatas(effect.str3);

        if (dataValid)
        {
            PutEffect();
        }
        else
        {
            Debug.Log("정상적이지 않은 str형식이 로딩되어서 effect가 정상적으로 발생되지 않았음.");
            return false;
        }

        return true;
    }

    public bool AmplifyVisualEffect(EffectData effect, int skillVisualDataBuff = 0, int skillVisualDataAttack = 0)
    {
        Debug.Log(effect.str1 + "에 대한 효과 발동함.");
        ResetTempDatas();
        bool dataValid = true;

        dataValid &= PutFirstLineDatas(effect.str1);
        dataValid &= PutSecondLineDatas(effect.str2);
        dataValid &= PutThirdLineDatas(effect.str3);

        if (dataValid)
        {
            VisualSkillEffect(effect, skillVisualDataBuff, skillVisualDataAttack);
        }
        else
        {
            Debug.Log("정상적이지 않은 str형식이 로딩되어서 effect visualizing이 정상적으로 발생되지 않았음.");
            return false;
        }
        return true;
    }

    private void VisualSkillEffect(EffectData effect, int skillVisualDataBuff = 0, int skillVisualDataAttack = 0)
    {
        List<EntityStat> targetData = GetTarget();
        // 자신에 대한 버프 비주얼 이펙트가 존재하는 경우.
        if (skillVisualDataBuff != 0)
        {
            Player.instance.GetComponentInChildren<EntitySkillVisualCaller>().CallSkillVisual(skillVisualDataBuff);
        }

        if (skillVisualDataAttack != 0)
        {
            if (targetData[0] is EnemyStat)
            {
                foreach(EnemyStat data in targetData)
                {
                    data.gameObject.GetComponentInChildren<EntitySkillVisualCaller>().CallSkillVisual(skillVisualDataAttack);
                }
            }
        }
    }



    public void TestToPutEffect()
    {
        AmplifyEffect(testData);
    }

    public void PutEffect()
    {
        switch (status) // 만약 플레이어에게 버프 단위로 들어가는 스킬들 (전투 단위 버프)은 전부 PlayerStat의 List에 들어갈것.
        {
            case EffectType.CURHP:
                CurHPChange();
                break;
            case EffectType.MAXHP:
                break;
            case EffectType.CURSAT:
                CurSatChange();
                break;
            case EffectType.MAXSAT:
                break;
            case EffectType.CURARM:
                CurArmourChange();
                break;
            case EffectType.DISARM:
                DisArm();
                break;
            case EffectType.ALLATK:
                AllAttackDamageChange();
                break;
            case EffectType.MELATK:
                break;
            case EffectType.SKLATK:
                break;
            case EffectType.POISON:
                PoisonChange();
                break;
            case EffectType.BLOOD:
                BloodChange();
                break;
            case EffectType.FIRE:
                FireChange();
                break;
            case EffectType.CURVIT:
                CurVitChange();
                break;
            case EffectType.MAXVIT:
                break;
            case EffectType.CURMIN:
                CurMinChange();
                break;
            case EffectType.MAXMIN:
                break;
            case EffectType.CURTHR:
                CurThrChange();
                break;
            case EffectType.MAXTHR:
                break;
            case EffectType.CURE:
                Cure();
                break;
            case EffectType.ALLHIT:
                break;
            case EffectType.NONE:
                break;
        }

    }

    // p [I,S,B] / E [I, S]
    private void AllAttackDamageChange()
    {
        if (effectIndicator == EffectCountFor.INSTANT)
        {
            List<EntityStat> target = GetTarget();
            for (int i = 0; i < target.Count; i++)
            {
                target[i].AllAttackDamageControl(effectPower, 'I', 0.0f);
            }
        }
        else if (effectIndicator == EffectCountFor.BATTLE)
        {
            List<EntityStat> target = GetTarget();
            for (int i = 0; i < target.Count; i++)
            {
                target[i].AllAttackDamageControl(effectPower, 'B', effectDuration);
            }
        }
        else if (effectIndicator == EffectCountFor.SECOND)
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
    private void SanityChange()
    {
        PlayerStat target = GetTarget()[0] as PlayerStat;
        target.CurrentSanityControl(effectPower);
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
        for (int i = 0; i < target.Count; i++)
        {
            target[i].MaxHPControl(effectPower);
        }
    }

    private bool CurHPChange()
    {
        List<EntityStat> target = GetTarget();
        for(int i = 0; i < target.Count; i++)
        {
            target[i].CurrentHPControl(effectPower);
        }
        return false;
    }

    private bool CurSatChange()
    {
        PlayerStat target = Player.instance.stat;
        target.CurrentSaturControl(effectPower);
        return false;
    }

    private bool CurMinChange()
    {
        PlayerStat target = Player.instance.stat;
        target.CurrentSanityControl(effectPower);
        return false;
    }

    private bool CurVitChange()
    {
        PlayerStat target = Player.instance.stat;
        target.CurrentVitalControl(effectPower);
        return false;
    }

    private bool CurThrChange()
    {
        PlayerStat target = Player.instance.stat;
        target.CurrentThirstControl(effectPower);
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
