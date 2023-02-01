[System.Serializable]
public class SkillData
{
    public int skillCode; // 스킬 코드.
    public int skillIconCode; // 스킬 아이콘 코드
    public string skillName; // 스킬 이름.
    public string skillLore; // 스킬 설명.
    public string[] skillEffect1; // 스킬 효과들. (string) 배열
    public string[] skillEffect2; // 스킬 효과들. (string) 배열
    public string[] skillEffect3; // 스킬 효과들. (string) 배열
    public float skillCooltime;
    public float skillDelay;
    
}
