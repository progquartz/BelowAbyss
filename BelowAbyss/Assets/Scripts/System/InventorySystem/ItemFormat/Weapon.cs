[System.Serializable]
public class Weapon : Item
{
    public float attackSpeed;
    public int criticalPercentage;

    public string[] hitEff1; // 기본 공격시 발동되는 효과.
    public string[] hitEff2;
    public string[] hitEff3;
    
    public string[] criticalEff1;
    public string[] criticalEff2;
    public string[] criticalEff3;

    public int[] additionalSkills; // 추가 스킬


    public Weapon(int _itemcode, int _stack) : base (_itemcode, _stack)
    {
        itemcode = _itemcode;
        stack = 1;
    }

    public Weapon(int _itemcode, int _stack, int _stacklimit) : base (_itemcode, _stack, _stacklimit)
    {
        itemcode = _itemcode;
        stack = 1;
        stacklimit = 1;
    }
}
