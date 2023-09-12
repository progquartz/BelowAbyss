[System.Serializable]
public class WeaponItemData : OtherItemData
{
    public int damage;
    public double attackSpeed; // 공격속도 //0.7
    public int criticalChance; // 치명타 확률


    public string[] hitEffect1; // 기본 공격시 발동되는 효과
    public string[] hitEffect2; 
    public string[] hitEffect3; 


    public string[] criticalEffect1; // 치명타시의 효과
    public string[] criticalEffect2; // 치명타시의 효과
    public string[] criticalEffect3; // 치명타시의 효과

    public int[] additionalSkills; // 추가 스킬
}
