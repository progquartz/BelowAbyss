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
    private string themeSelected;

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

    public void MapFirstSetup(int stagenum, StageLoreUI stageLoreUI)
    {
        stageNum = stagenum;
        // 테마 선택.
        int randomNum = Random.Range(0, ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData.Length);
        themeSelected = ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData[randomNum];

        ThemeData themeData = ThemeDataBase.instance.FindThemes(themeSelected);
        int themeCode = ThemeDataBase.instance.GetThemeCode(themeSelected);
        backupMapData.mapTheme = themeSelected;
        //stageLoreUI.AppearStageLore(stageNum, themeData.themeName, themeData.themeLore, 1.5f, 1.0f);

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
            randomNum = Random.Range(0, themeData.eventList.Length);
            int eventData = SetEventFromData(i);
            events.Add(eventData);
            backupMapData.eventList[i] = eventData;
            eventVisited.Add(false);
        }


        // 가장 마지막 보스파이트 데이터.
        bossEvent = SetRandomBossFromData();
        roomVisited = false;

    }

    /// <summary>
    /// 맵을 초기설정하는 함수.
    /// 랜덤값으로 이벤트들이 선정되며, 이들의 데이터가 파일 형식으로 남게 됨.
    /// </summary>
    /// <param name="stagenum"></param>
    public void MapFirstSetup(int stagenum)
    {
        stageNum = stagenum;
        // 테마 선택.
        int randomNum = Random.Range(0, ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData.Length);
        themeSelected = ThemeDataBase.instance.stageThemeDatas.stageThemeDatas[stagenum].stageThemeData[randomNum];
        backupMapData.mapTheme = themeSelected;


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
            randomNum = Random.Range(0, ThemeDataBase.instance.FindThemes(themeSelected).eventList.Length);
            int eventData = SetEventFromData(i);
            events.Add(eventData);
            backupMapData.eventList[i] = eventData;
            eventVisited.Add(false);
        }


        // 가장 마지막 보스파이트 데이터.
        bossEvent = SetRandomBossFromData();
        roomVisited = false;

    }

    private int SetEventFromData(int index)
    {
        return ThemeDataBase.instance.GetRandomEventFromTheme(themeSelected);
    }

    private int SetRandomBossFromData()
    {
        return ThemeDataBase.instance.GetRandomBossFromTheme(themeSelected);
    }

    /// <summary>
    /// 현재 있는 방의 이벤트 코드를 불러와 MapManager에게 알리는 함수.
    /// </summary>
    /// <returns></returns>
    public int Encounter()
    {
        // 다음 방의 이벤트 호출.
        if(playerPos == 15)
        {
            return bossEvent;
        }
        // 현재 있는 이벤트위치의 이벤트 호출.
        else
        {
            return events[(playerPos / 3) - 1];
        }
    }

    public bool IsFirstEncounter()
    {
        if(playerPos == 15)
        {
            return roomVisited;
        }
        if(playerPos == 0)
        {
            return true;
        }
        if(playerPos % 3 == 0)
        {
            // 이벤트는 3,6,9,12 -> 3으로 나눠지는 수. (0제외)
            return eventVisited[(playerPos / 3) - 1];   
        }
        else
        {
            // 경로는 1,2, 4,5, 7,8, 10,11, 13,14 -> 3으로 나눌 시 나머지가 1,2인 수.
            //       0,1, 2,3  4,5,  6,7    8, 9  
            int add = 0;
            if(playerPos % 3 == 1)
            {
                add = 0;
            }
            else if(playerPos % 3 == 2)
            {
                add = 1;
            }
            return pathsVisited[(2 * (playerPos / 3)) + add];
        }
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
        Debug.Log(events[index] + " 번 코드의 이벤트를 방문합니다!");
        return events[index];
    }

    public bool GetEventVisited(int index)
    {
        return eventVisited[index];
    }

    public bool GetPathVisited(int index)
    {
        return pathsVisited[index];
    }

    public bool GetRoomVisited()
    {
        return roomVisited;
    }

    public int GetBossEvent()
    {
        return bossEvent;
    }

}

