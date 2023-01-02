[System.Serializable]
public class EnemyStat : EntityStat
{
    public int size;
    public int enemyCode;
    public int position;
    public string enemySpriteCode;

    public int attackDamage;
    public double attackSpeed;

    public string[] additionalEffect;
    public double[] additionalEffectCooltime;

    /// <summary>
    /// 버프 리스트. 이들이 엔티티에 들어갈지는 아직 미지수.
    /// </summary>
    public int additionalWeaponDamage; // 가하는 무기 데미지 추가.
    public int additionalSkillDamage; // 가하는 스킬 데미지 추가
    public int weaponAndSkillCooldown; // 공통 쿨다운.

    public int soundCode;
    
}
