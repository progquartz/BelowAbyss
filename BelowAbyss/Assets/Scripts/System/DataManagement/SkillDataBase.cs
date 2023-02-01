using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDataBase : MonoBehaviour
{
    public static SkillDataBase instance;

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

    public SkillDatas skillDatas = new SkillDatas();

    public SkillData FindSkillData(int skillCode)
    {
        return skillDatas.FindSkillData(skillCode);
    }


}

[System.Serializable]
public class SkillDatas
{
    public SkillData[] skillDatas;

    public SkillData FindSkillData(int skillCode)
    {
        for(int i = 0; i < skillDatas.Length; i++)
        {
            if(skillDatas[i].skillCode == skillCode)
            {
                return skillDatas[i];
            }
        }
        Debug.Log("비정상적으로 스킬 데이터가 로드되었습니다!");
        return null;
    }
}
