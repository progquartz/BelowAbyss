[System.Serializable]
public class PlayerStat : EntityStat
{
    /// <summary>
    /// 플레이어만이 가질 수 있는 고유한 수치.
    /// </summary>
    public int currentSatur;
    public int maxSatur;
    public int currentThirst; // 목마름 수치
    public int maxThirst;
    public int currentSanity; // 정신 수치 체력회복에 관여
    public int maxSanity;

}
