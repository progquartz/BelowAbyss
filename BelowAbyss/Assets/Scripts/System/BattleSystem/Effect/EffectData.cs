using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData : MonoBehaviour
{
    public string str1;
    public string str2;
    public string str3;

    public void SetEffectData(string _str1, string _str2, string _str3)
    {
        str1 = _str1;
        str2 = _str2;
        str3 = _str3;
    }

    public EffectData GetAllData()
    {
        return this;
    }
}
