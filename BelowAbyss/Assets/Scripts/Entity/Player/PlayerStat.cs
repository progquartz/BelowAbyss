using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStat : EntityStat
{
    /// <summary>
    /// 다음 리스트들은 N회의 전투동안 유지되는 버프들의 리스트들이다.
    /// 버프들은 N회 전투동안 N만큼의 효과 유지로.
    /// </summary>
    public List<Tuple<int, int>> maxHpBuffs = new List<Tuple<int, int>>(); // 최대 체력이 N회의 전투동안 유지됨.
    public List<Tuple<int, int>> currentArmourBuffs = new List<Tuple<int, int>>(); // N턴동안 전투가 시작될때 N만큼의 방어력을 획득함.

    public List<Tuple<int, int>> allAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 공격피해 증가.
    public List<Tuple<int, int>> meleeAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 무기피해 증가.
    public List<Tuple<int, int>> skillAttackBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 가하는 모든 스킬피해 증가.

    public List<Tuple<int, int>> allHitBuffs = new List<Tuple<int, int>>(); // N회의 전투동안 받는 모든 공격피해 증가.


    public int currentSatur;
    public int maxSatur;
    public int currentThirst; // 목마름 수치
    public int maxThirst;
    public int currentSanity; // 정신 수치 체력회복에 관여
    public int maxSanity;

}
