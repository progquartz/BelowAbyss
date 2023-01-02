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
    
    /// <summary>
    /// 버프 리스트. 이들이 엔티티에 들어갈지는 아직 미지수.
    /// </summary>
    public int additionalWeaponDamage; // 가하는 무기 데미지 추가.
    public int additionalSkillDamage; // 가하는 스킬 데미지 추가
    public int weaponAndSkillCooldown; // 공통 쿨다운.

}
