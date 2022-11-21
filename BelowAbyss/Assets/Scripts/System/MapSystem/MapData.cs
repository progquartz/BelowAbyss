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

    // 맵 내에서의 플레이어 데이터.
    // 경로와 이벤트 유형, 그리고 방까지 해서 다 합하는 것으로 진행.
    // 첫 방은 0 -> 0
    // 경로는 1,2, 4,5, 7,8, 10,11, 13,14 -> 3으로 나눌 시 나머지가 1,2인 수.
    // 이벤트는 3,6,9,12 -> 3으로 나눠지는 수. (0제외)
    // 마지막 방은 15 -> 15.
    [SerializeField]
    private int playerPos;
    // 맵 내의 데이터.
    private List<bool> pathsVisited; // 나중에 기획자료 제대로 도착하면 int대신 enum으로 수정하기. paths는 혹시 몰라서 

    private List<EncounterType> events;
    private List<bool> eventVisited;

    private EncounterType room; // 출발하는 이전 방도 들어갈 수 있다면, List<int>일 것이고, 아니면 그냥 int일것.
    private bool roomVisited;

    public enum EncounterType
    {
        NORMAL,
        SUPPLY,
        TRAIT,
        MOVEMENT,
        BATTLE
    }

    public void MapFirstSetup(int stagenum)
    {

        int randomnum;
        // Paths에 새로운 데이터 입력. 나중에 계수와 수식이 붙으면 다른 함수로 분리될 수도 있음.
        pathsVisited = new List<bool>();
        for (int i = 0; i < 10; i++)
        {
            pathsVisited.Add(false);
        }

        // Events에 새로운 데이터 입력.나중에 계수와 수식이 붙으면 다른 함수로 분리될 수도 있음.
        events = new List<EncounterType>();
        eventVisited = new List<bool>();
        for (int i = 0; i < 4; i++)
        {
            randomnum = 0; // random한 코드가 들어갈 예정. 
            events.Add(EncounterType.NORMAL);
            eventVisited.Add(false);
        }

        randomnum = 0;
        room = EncounterType.BATTLE;
        roomVisited = false;

    }

    /// <summary>
    /// 현재 있는 방의 이벤트 코드를 불러와 MapManager에게 알리는 함수.
    /// </summary>
    /// <returns></returns>
    public EncounterType Encounter()
    {
        // 다음 방의 이벤트 호출.
        if(playerPos == 15)
        {
            return room;
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

    public EncounterType GetEvent(int index)
    {
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

    public EncounterType GetRoom()
    {
        return room;
    }

}

