using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDataBase : MonoBehaviour
{
    public static TraitDataBase instance;


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

    public TraitDatas traitDatas = new TraitDatas();

    public TraitData GetTraitData(int traitCode)
    {
        return traitDatas.FindTraitData(traitCode);
    }

}


[System.Serializable]
public class TraitDatas
{
    public TraitData[] traitDatas;

    public TraitData FindTraitData(int traitCode)
    {
        for (int i = 0; i < traitDatas.Length; i++)
        {
            if (traitDatas[i].traitCode == traitCode)
            {
                return traitDatas[i];
            }
        }
        Debug.Log("비정상적으로 특성 데이터가 로드되었습니다!");
        return null;
    }
}
