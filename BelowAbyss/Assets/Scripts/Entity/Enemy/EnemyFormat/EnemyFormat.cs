[System.Serializable]
public class EnemyFormat
{
    public int EnemyCode; // 적 코드
    public int MaxHealth;// 최대 체력
    public int Size; // 크기
    public string enemySpriteCode; // 스프라이트 / 애니메이션 코드 (애니메이션 어떻게 넣지)
    public int SoundCode; // 공격시 사운드 / 사망시 사운드 등의 연결.
    public int attackDamage;
    public double attackSpeed;
    public string[] additionalEffect; // 가하는 효과 → [효과 문서.](https://www.notion.so/0234e5b37b9245d59afb7b510dd841e4) 
    public double[] additionalEffectCoolTime; // 효과 쿨타임 //
}
