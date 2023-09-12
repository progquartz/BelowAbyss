using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyHord : MonoBehaviour
{
    private float[] enemyHolderRowXpositionData = new float[4] {0,250,500,750};

    public List<Enemy> enemies;
    public List<GameObject> enemyHolder;

    public int firstIndex = 0; // 이건 몹중 처음에 있는걸 의미 (첫번째 칸을 의미하는게 아님.)
    public int lastIndex = -1; // 이건 몹중 마지막에 있는걸 의미 (마지막 칸을 의미하는게 아님)
    public int enemyCount;

    // 범위 스킬이 있음. 1열 기준 2칸 공격. 적이 2칸을 차지하는 애가 있음 그럼 얘혼자 맞음?
    // 데미지는 1번. 

    /// <summary>
    /// 캐릭터를 죽은 판정을 내게 만듬. 어떠한 형식으로 죽든간에 Enemy에서 호출되게 되어있음.
    /// </summary>
    /// <param name="index"></param>
    public void Death(int index)
    {
        enemies.Remove(enemies[index]);
        enemyHolder[index].SetActive(false);
        enemyHolder.Remove(enemyHolder[index]);
        enemyCount--;
        lastIndex--;
        RearrangeHordPositions();
    }

    /// <summary>
    /// 모든 적의 배열이 순서대로 잘 가 있는지 검사.
    /// </summary>
    private void RearrangeHordPositions()
    {
        int indexHaveToBe = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].stat.position != indexHaveToBe)
            {
                enemies[i].stat.position = indexHaveToBe;
                indexHaveToBe += enemies[i].stat.size;
            }
            else
            {
                indexHaveToBe += enemies[i].stat.size;
            }
        }
    }
    /// <summary>
    /// 가장 앞 열 기준으로 count만큼의 칸 내에 있는 적 열을 가져옴.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<int> GetRowFromFrontHord(int count)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < count; i++)
        {
            temp.Add(firstIndex + i);
        }
        return temp;
    }

    /// <summary>
    /// 가장 뒷 열 기준으로 count한만큼의 칸 내에 있는 적 열을 가져옴.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<int> GetRowFromBackHord(int count)
    {
        List<int> temp = new List<int>();
        for(int i = 0; i < count; i++)
        {
            temp.Add(lastIndex - i);
        }
        return temp;
    }
    
    public List<int> GetAllFromHord()
    {
        List<int> temp = new List<int>();
        for(int i = 0; i < enemyCount;i++)
        {
            temp.Add(i);
        }

        return temp;
    }

    public void PlaceEnemyOnBack(int enemyCode)
    {
        EnemyFormat data = EnemyDataBase.instance.GetEnemy(enemyCode);

        enemies.Add(transform.GetChild(lastIndex + 1).GetComponent<Enemy>());
        enemyHolder.Add(transform.GetChild(lastIndex + 1).gameObject);

        // 체력 관련
        enemies[lastIndex + 1].stat.maxHp = data.MaxHealth;
        enemies[lastIndex + 1].stat.currentHp = data.MaxHealth;

        // 사이즈 / 추가 효과 관련.
        enemies[lastIndex + 1].stat.size = data.Size;

        enemies[lastIndex + 1].stat.additionalEffect1 = data.additionalEffect1;
        enemies[lastIndex + 1].stat.additionalEffect2 = data.additionalEffect2;
        enemies[lastIndex + 1].stat.additionalEffect3 = data.additionalEffect3;
        enemies[lastIndex + 1].stat.additionalEffectCoolTime = data.additionalEffectCoolTime;

        // 공격 관련
        enemies[lastIndex + 1].stat.attackDamage = data.attackDamage;
        enemies[lastIndex + 1].stat.attackSpeed = data.attackSpeed;

        // 코드 관련 업데이트
        enemies[lastIndex + 1].stat.enemySpriteCode = data.enemySpriteCode;
        enemies[lastIndex + 1].stat.enemyCode = data.EnemyCode;

        if (lastIndex == -1)
        {
            enemies[lastIndex + 1].stat.position = 0;
        }
        else
        {
            enemies[lastIndex + 1].stat.position = lastIndex + enemies[lastIndex].stat.size;
        }
        

        lastIndex += 1;
    }

    private void Start()
    {
        enemies = new List<Enemy>();
        enemyHolder = new List<GameObject>();
    }

    private void Update()
    {
        UpdateEnemyPositionData();
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateSprite();
        }
        CheckEnemyAllDeath();
    }

    private void CheckEnemyAllDeath()
    {
        
        if(BattleManager.instance.isBattleStarted && enemyCount == 0)
        {
            Debug.Log("모든 적이 죽음으로써 전투가 종료됩니다.");
            BattleManager.instance.BattlePhaseEnded();
        }
        
    }

    private void UpdateEnemyPositionData()
    {
        // 죽으면 리스트에서 제외되기 때문에, 업데이트가 필요함.
        for (int i = 0; i < enemyCount; i++)
        {
            UpdateEnemyPlaceHolderPosition(i, enemies[i].stat.position ,enemies[i].stat.size);
        }
    }

    private void UpdateEnemyPlaceHolderPosition(int enemyIndex, int position, int size)
    {
        Vector3 NewPos = new Vector3(((enemyHolderRowXpositionData[position] + enemyHolderRowXpositionData[position + size - 1])) / 2, 
                                             0, 0);
        enemyHolder[enemyIndex].transform.localPosition = NewPos;
    }

    /// <summary>
    /// 기존에 있던 적들을 싹 밀고 새로운 호드데이터 기반의 적 소환.
    /// </summary>
    /// <param name="hordData"></param>
    public void SpawnHord(List<int> hordData)
    {
        ElimateAllHord();

        for(int i = 0; i < hordData.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            PlaceEnemyOnBack(hordData[i]);

            enemyCount = hordData.Count;
        }
        for(int i = hordData.Count; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ElimateAllHord()
    {
        firstIndex = 0;
        lastIndex = -1;
        enemyCount = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            enemyHolder[i].SetActive(false);
        }
        enemies = new List<Enemy>();
        enemyHolder = new List<GameObject>();
    }
    public void Test1()
    {
        
        List<int> hordData = new List<int>();
        hordData.Add(1);
        hordData.Add(2);
        hordData.Add(3);


        SpawnHord(hordData);
    }

    public void Test2()
    {

        List<int> hordData = new List<int>();
        hordData.Add(1);
        hordData.Add(3);
        hordData.Add(3);
        hordData.Add(1);


        SpawnHord(hordData);
    }

    public void Test3()
    {

        List<int> hordData = new List<int>();
        hordData.Add(2);
        hordData.Add(2);


        SpawnHord(hordData);
    }

}
