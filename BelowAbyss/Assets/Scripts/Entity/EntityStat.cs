using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EntityStat
{
    public int currentHp;
    public int maxHp;

    public int armour;


    

    /// <summary>
    /// 체력을 변경시키는 함수. 음수도 가능함.
    /// 만약에 체력이 초과되었을 경우에는 그 수치만큼을.
    /// 만약에 체력이 0이하로 떨어지면 -1을, 일반에는 0을 리턴함.
    /// </summary>
    /// <param name="value">변경될 체력 수치.</param>
    /// <returns></returns>
    public int AddHealth(int value)
    {
        currentHp += value;
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


    /// <summary>
    /// 디버프 리스트.
    /// </summary>

    public int poisionStack; // 독 스택. 매 초 N만큼의 데미지를 줌.
    public int bloodStack; // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음.
    public bool onFire; // 불 붙음 여부. 불이 붙었을 경우 1초마다 정해진 만큼의 피해를 줌.
    
    



}
