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


    public void ResetStat()
    {
        armour = 0;
        isPoisoned = false;
        isBleeding = false;
        poisonStack = new BuffData();// 독 스택. 매 초 N만큼의 데미지를 줌.
        bloodStack = new BuffData(); // 출혈 스택. 잃은 체력의 N%의 데미지를 매 초 입음.
        isOnFire = false; // 불 붙음 여부. 불이 붙었을 경우 1초마다 정해진 만큼의 피해를 줌.
        

    }

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
