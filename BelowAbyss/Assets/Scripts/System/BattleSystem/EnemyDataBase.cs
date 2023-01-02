using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemies
{
    public EnemyFormat[] enemies;

    public EnemyFormat GetEnemy(int index)
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].EnemyCode == index)
            {
                return enemies[i];
            }
        }
        Debug.Log("!!!!! 적 데이터 발견되지 않음!!");
        return null;
    }
}
public class EnemyDataBase : MonoBehaviour
{
    public static EnemyDataBase instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Enemies enemies = new Enemies();

    public EnemyFormat GetEnemy(int index)
    {
        return enemies.GetEnemy(index);
    }

}
