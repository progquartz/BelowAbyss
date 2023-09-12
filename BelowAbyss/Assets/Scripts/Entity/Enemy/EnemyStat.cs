[System.Serializable]
public class EnemyStat : EntityStat
{
    public int size;
    public int enemyCode;
    public int position;
    public string enemySpriteCode;

    public int attackDamage;
    public float attackSpeed;

    public string[] additionalEffect1; // 가하는 효과 → [효과 문서.](https://www.notion.so/0234e5b37b9245d59afb7b510dd841e4) 
    public string[] additionalEffect2; // 가하는 효과 → [효과 문서.](https://www.notion.so/0234e5b37b9245d59afb7b510dd841e4) 
    public string[] additionalEffect3; // 가하는 효과 → [효과 문서.](https://www.notion.so/0234e5b37b9245d59afb7b510dd841e4) 
    public float[] additionalEffectCoolTime; // 효과 쿨타임 //
    public int[] additionalEffectSprite;



    public int soundCode;
    
}
