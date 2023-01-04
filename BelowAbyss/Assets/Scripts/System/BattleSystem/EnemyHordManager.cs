using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHordManager : MonoBehaviour
{
    public Enemy[] enemies;
    
    public void SetEnemyOnIndex(int enemyCode, int index)
    {
        EnemyFormat data = EnemyDataBase.instance.GetEnemy(enemyCode);
        SetEnemyData(index, data);
    }

    private void SetEnemyData(int index, EnemyFormat data)
    {
        // 체력 관련
        enemies[index].stat.maxHp = data.MaxHealth;
        enemies[index].stat.currentHp = data.MaxHealth;
        
        // 사이즈 / 추가 효과 관련.
        enemies[index].stat.size = data.Size;
        enemies[index].stat.additionalEffect = data.additionalEffect;
        enemies[index].stat.additionalEffectCooltime = data.additionalEffectCoolTime;

        // 공격 관련
        enemies[index].stat.attackDamage = data.attackDamage;
        enemies[index].stat.attackSpeed = data.attackSpeed;

        // 코드 관련 업데이트
        enemies[index].stat.enemySpriteCode = data.enemySpriteCode;
        enemies[index].stat.enemyCode = data.EnemyCode;
    }

    private void Start()
    {
        enemies = new Enemy[4];
        for(int i = 0; i < 4; i++)
        {
            enemies[i] = transform.GetChild(i).GetComponent<Enemy>();
        }
    }

    private void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            enemies[i].UpdateSprite();
        }
    }
    public void Test1()
    {
        SetEnemyOnIndex(1, 0);   
        SetEnemyOnIndex(2, 1);
        SetEnemyOnIndex(3, 2);
        SetEnemyOnIndex(4, 3);
    }

}
