using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    /// <summary>
    /// MapData�� �⺻������ ���� �����͸� �����ϵ��� ������� Ŭ�����̴�.
    /// 
    /// ����Ǿ�� �� ��ϵ��� �Ϲ������� ������ ����.
    /// - ������ �� ���� �����͵� (��� ����, �̺�Ʈ ����, ���� �� ����)
    /// - �ʿ� �߰������� ����Ǿ�� �ϴ� ��Ƽ�ö��̾��. // bool������ ����Ǿ�, �̵��� MapManager���� �����Ͽ� ó���� ����.
    /// - �� �̵�(���, �̺�Ʈ)���� �Һ�Ǿ�� �� ��ġ.
    /// - �÷��̾��� ��ġ.
    /// 
    /// </summary>

    // valid�ϴ��� �Ź� üũ��.
    private bool IsMapdataValid = false;

    // �� �������� �÷��̾� ������.
    // ��ο� �̺�Ʈ ����, �׸��� ����� �ؼ� �� ���ϴ� ������ ����.
    // ù ���� 0 -> 0
    // ��δ� 1,2, 4,5, 7,8, 10,11, 13,14 -> 3���� ���� �� �������� 1,2�� ��.
    // �̺�Ʈ�� 3,6,9,12 -> 3���� �������� ��. (0����)
    // ������ ���� 15 -> 15.
    [SerializeField]
    private int playerPos;
    // �� ���� ������.
    private List<bool> pathsVisited; // ���߿� ��ȹ�ڷ� ����� �����ϸ� int��� enum���� �����ϱ�. paths�� Ȥ�� ���� 

    private List<EncounterType> events;
    private List<bool> eventVisited;

    private EncounterType room; // ����ϴ� ���� �浵 �� �� �ִٸ�, List<int>�� ���̰�, �ƴϸ� �׳� int�ϰ�.
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
        // Paths�� ���ο� ������ �Է�. ���߿� ����� ������ ������ �ٸ� �Լ��� �и��� ���� ����.
        pathsVisited = new List<bool>();
        for (int i = 0; i < 10; i++)
        {
            pathsVisited.Add(false);
        }

        // Events�� ���ο� ������ �Է�.���߿� ����� ������ ������ �ٸ� �Լ��� �и��� ���� ����.
        events = new List<EncounterType>();
        eventVisited = new List<bool>();
        for (int i = 0; i < 4; i++)
        {
            randomnum = 0; // random�� �ڵ尡 �� ����. 
            events.Add(EncounterType.NORMAL);
            eventVisited.Add(false);
        }

        randomnum = 0;
        room = EncounterType.BATTLE;
        roomVisited = false;

    }

    /// <summary>
    /// ���� �ִ� ���� �̺�Ʈ �ڵ带 �ҷ��� MapManager���� �˸��� �Լ�.
    /// </summary>
    /// <returns></returns>
    public EncounterType Encounter()
    {
        // ���� ���� �̺�Ʈ ȣ��.
        if(playerPos == 15)
        {
            return room;
        }
        // ���� �ִ� �̺�Ʈ��ġ�� �̺�Ʈ ȣ��.
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
            // �̺�Ʈ�� 3,6,9,12 -> 3���� �������� ��. (0����)
            return eventVisited[(playerPos / 3) - 1];   
        }
        else
        {
            // ��δ� 1,2, 4,5, 7,8, 10,11, 13,14 -> 3���� ���� �� �������� 1,2�� ��.
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
    /// ���缺 ���� üũ. �̻��� ��ġ ������ �˾Ƽ� �ɷ�����!
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
        // �̺�Ʈ�� ���.
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

