[System.Serializable]
public class ConsumeItemData : OtherItemData
{
    public string[] itemUseCode1; // 효과유형, 배열로 만들어야 여러효과를 발동 가능.
    public string[] itemUseCode2; // 효과유형, 배열로 만들어야 여러효과를 발동 가능.
    public string[] itemUseCode3; // 효과유형, 배열로 만들어야 여러효과를 발동 가능.
}
