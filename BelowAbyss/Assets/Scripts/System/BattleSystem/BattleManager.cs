using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public EnemyHord enemyHord;
    private List<int> currentHordAgainst;
    public bool isBattleStarted = false;
    public bool isBattleHasAdditionalEvent = false;
    public int additionalEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        enemyHord = transform.GetComponentInChildren<EnemyHord>();
    }

    public void BattlePhaseBegin(BattleEvent battleEvent)
    {
        this.gameObject.SetActive(true);
        isBattleStarted = true;
        isBattleHasAdditionalEvent = battleEvent.isAdditionalEvent;
        if (isBattleHasAdditionalEvent)
        {
            additionalEvent = battleEvent.additionalEventCode;
        }
        else
        {
            additionalEvent = -1;
        }
        BattleStart(battleEvent);
    }

    public void BattlePhaseEnded()
    {
        this.gameObject.SetActive(false);
        isBattleStarted = false;
        if (isBattleHasAdditionalEvent)
        {
            EventManager.instance.LoadEvent(additionalEvent);
        }
    }

    public void OnPlayerDeath()
    {
        isBattleStarted = false;

        Debug.Log("게임 오버!");
        GameManager.instance.GameOver();
        
    }


    /// <summary>
    /// 현재 가진 상대호드데이터를 삭제하고, 배틀이벤트를 기반으로 한 새로운 적 호드 생성.
    /// </summary>
    /// <param name="battleEvent"></param>
    public void BattleStart(BattleEvent battleEvent)
    {
        currentHordAgainst = new List<int>();

        int randomNum = Random.Range(0, battleEvent.hordCount-1);

        string[] hordData = battleEvent.hordData[randomNum].Split();
        for(int i = 0; i < hordData.Length; i++)
        {
            currentHordAgainst.Add(int.Parse(hordData[i]));
        }
        enemyHord.SpawnHord(currentHordAgainst);
    }

    public void BattleStart()
    {
        if(currentHordAgainst != null)
        {
            enemyHord.SpawnHord(currentHordAgainst);
        }
        else
        {
            Debug.Log("정상적이지 않은 호드데이터를 가진 채로 배틀매니저가 적 데이터를 로드합니다.");
        }
    }


}
