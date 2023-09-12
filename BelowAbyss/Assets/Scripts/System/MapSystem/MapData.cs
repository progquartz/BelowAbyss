using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    /// <summary>
    /// MapData는 기본적으로 맵의 데이터를 저장하도록 만들어진 클래스이다.
    /// 
    /// 저장되어야 할 목록들은 일반적으로 다음과 같다.
    /// - 정해진 맵 내의 데이터들 (경로 유형, 이벤트 유형, 다음 방 유형)
    /// - 맵에 추가적으로 적용되어야 하는 멀티플라이어들. // bool값으로 저장되어, 이들을 MapManager에서 관리하여 처리될 예정.
    /// - 매 이동(경로, 이벤트)에서 소비되어야 할 수치.
    /// - 플레이어의 위치.
    /// 
    /// </summary>

    // valid하는지 매번 체크함.
    private bool IsMapdataValid = false;

    [SerializeField]
    private int stageNum;
    [SerializeField]
    private int playerPos;
    // 맵 내의 데이터.
    private List<bool> pathsVisited; 

    private List<int> events;
    public List<bool> eventVisited;
    public string themeSelected;

    private BackupMapData backupMapData = new BackupMapData();

    private int bossEvent; // 출발하는 이전 방도 들어갈 수 있다면, List<int>일 것이고, 아니면 그냥 int일것.
    public bool roomVisited;

    public enum EncounterType
    {
        NORMAL,
        SUPPLY,
        TRAIT,
        MOVEMENT,
        BATTLE
    }

    /// <summary>
    /// 테마 선택, 테마를 맵 setup보다 빨리하는 이유는 mapvisual의 테마 변경 때문임.
    /// </summary>
    /// <param name="stagenum"></param>
    /// <param name="stageLoreUI"></param>
    public void MapThemeSelection(int stagenum)
    {
        stageNum = stagenum;
        // 테마 선택.
        int randomNum = Random.Range(0, ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData.Length);

        themeSelected = ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData[randomNum];

        ThemeData themeData = ThemeDataBase.instance.FindThemes(themeSelected);
        backupMapData.mapTheme = themeSelected;

    }

    /// <summary>
    /// 맵을 초기설정하는 함수.
    /// 랜덤값으로 이벤트들이 선정되며, 이들의 데이터가 파일 형식으로 남게 됨.
    /// </summary>
    public void MapFirstSetup(int stagenum)
    {
        stageNum = stagenum;

        // 테마를 기반으로 Paths에 새로운 데이터 입력. 나중에 계수와 수식이 붙으면 다른 함수로 분리될 수도 있음.
        pathsVisited = new List<bool>();
        for (int i = 0; i < 10; i++)
        {
            pathsVisited.Add(false);
        }

        // 테마를 기반으로 Events에 새로운 데이터 입력.나중에 계수와 수식이 붙으면 다른 함수로 분리될 수도 있음.
        events = new List<int>();
        eventVisited = new List<bool>();
        for (int i = 0; i < 4; i++)
        {
            // 맵비주얼에서 관리하는 encounterevent객체들에서부터 데이터 받아오기.
            int eventData = MapManager.Instance.mapVisual.GetEncounterEventNormal(i);
            events.Add(eventData);
            backupMapData.eventList[i] = eventData;
            eventVisited.Add(false);
        }


        // 가장 마지막 보스파이트 데이터.
        bossEvent = MapManager.Instance.mapVisual.GetEncounterEventLast();
        roomVisited = false;

    }


    /// <summary>
    /// 정당성 여부 체크. 이상한 수치 있으면 알아서 걸러내기!
    /// </summary>
    /// <returns></returns>
    public bool CheckValidation()
    {
        return IsMapdataValid;
    }

    public int GetPosition()
    {
        return playerPos;
    }

    public void SetPosition(int position)
    {
        if (position > 15)
        {
            position = 15;
        }
        if (position < 0)
        {
            position = 0;
        }
        playerPos = position;
        VisitPosition(position);
    }

    public void VisitPosition(int position)
    {
        if(position == 0)
        {
            return;
        }
        if (position == 15)
        {
            roomVisited = true;
            return;
        }
        // 이벤트의 경우.
        if (position % 3 == 0)
        {
            eventVisited[position / 3 - 1] = true;
            return;
        }
        if (position % 3 == 1)
        {
            pathsVisited[(2 * (position / 3))] = true;
            return;
        }
        if (position % 3 == 2)
        {
            pathsVisited[(2 * (position / 3)) + 1] = true;
            return;
        }
    }

    public int GetEvent(int index)
    {
        return events[index];
    }

    public int GetBossEvent()
    {
        return bossEvent;
    }

}

