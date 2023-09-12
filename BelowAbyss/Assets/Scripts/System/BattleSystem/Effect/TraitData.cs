[System.Serializable]
public class TraitData
{
    public int traitCode; // 스킬 코드.
    public string traitName; // 스킬 이름.
    public string traitEffectLore; // 스킬 효과에 대한 설명
    public string traitLore; // 스킬 설명.
    public string[] traitEffect1; // 스킬 효과들. (string) 배열
    public string[] traitEffect2; // 스킬 효과들. (string) 배열
    public string[] traitEffect3; // 스킬 효과들. (string) 배열

    public TraitData()
    {
        traitCode = -1; // 스킬 코드.
        traitLore = null; // 스킬 설명.
        traitEffectLore = null;
        traitName = null; // 스킬 이름.
        traitEffect1 = null; // 스킬 효과들. (string) 배열
        traitEffect2 = null; // 스킬 효과들. (string) 배열
        traitEffect3 = null; // 스킬 효과들. (string) 배열
    }
}
