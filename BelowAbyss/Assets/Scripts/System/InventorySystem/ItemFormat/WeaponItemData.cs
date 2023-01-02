[System.Serializable]
public class WeaponItemData : OtherItemData
{
    public int durability; //내구도
    public int damage; // 무기 데미지 //5
    public double attackSpeed; // 공격속도 //0.7
    public int criticalChance; // 치명타 확률
    public string[] criticalEffect; // 치명타시의 효과
    public string[] hitEffect; // 타격시 효과 
    public string[] additionalEffect; // 추가 효과 → [효과 문서.](https://www.notion.so/0234e5b37b9245d59afb7b510dd841e4) 
    public double[] additionalEffectCoolTime; // 효과 쿨타임 //
}
