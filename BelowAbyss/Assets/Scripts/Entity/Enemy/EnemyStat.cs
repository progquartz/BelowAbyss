using System;

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
    public Enemy statOwner;



    public int soundCode;

    public override int CurrentHPControl(int amount)
    {
        int delta = 0;
        if (amount > 0)
        {
            currentHp += amount;
            delta = Math.Max(0, currentHp - maxHp);
            currentHp = Math.Min(currentHp, maxHp);
        }
        else if (amount < 0)
        {
            if (armour > 0)
            {
                armour += amount;
                if (armour < 0)
                {
                    currentHp += armour;
                    armour = 0;
                }
            }
            else
            {
                currentHp += amount;
                statOwner.enemyVisual.HurtAnimationOn();
            }

            if (currentHp <= 0)
            {
                delta = -1;
            }
        }

        return delta;
    }
}
