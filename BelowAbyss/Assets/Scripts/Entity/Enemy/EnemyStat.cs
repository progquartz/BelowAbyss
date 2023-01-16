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



    public int soundCode;
    
}
