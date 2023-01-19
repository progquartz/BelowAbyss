using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 이벤트 매니저는 이벤트와 관련된 전반적인 작업을 수행한다.
 이벤트의 체인으로 이루어진 여러 이벤트들을 진행하고 수행하며, 다음 시스템들과 연결되게 된다.
 
- 다이얼로그 시스템.
- 선택지
- 전투 시스템
- 루팅 시스템 (인벤토리)
     
    이벤트 유형에 따라 클래스가 나눠지기 때문에, 콜 호출을 받은 이벤트의 유형에 따라 무언가를 하면된다.
    
*/

[System.Serializable]
public class BattleEvents
{
    public BattleEvent[] battleEvents;

    public BattleEvent FindEvent(int eventcode)
    {
        for(int i = 0; i < battleEvents.Length; i++)
        {
            if(battleEvents[i].eventCode == eventcode)
            {
                return battleEvents[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class DialogEvents
{
    public DialogEvent[] dialogEvents;

    public DialogEvent FindEvent(int eventcode)
    {
        for (int i = 0; i < dialogEvents.Length; i++)
        {
            if (dialogEvents[i].eventCode == eventcode)
            {
                return dialogEvents[i];
            }
        }
        return null;
    }
}


[System.Serializable]
public class SelectionEvents
{
    public SelectionEvent[] selectionEvents;

    public SelectionEvent FindEvent(int eventcode)
    {
        for (int i = 0; i < selectionEvents.Length; i++)
        {
            if (selectionEvents[i].eventCode == eventcode)
            {
                return selectionEvents[i];
            }
        }
        return null;
    }
}


public class EventManager : MonoBehaviour
{
    /// <summary>
    /// 이벤트 매니저임. 모든 데이터 저장하고 관리함. 파서에서 이벤트 매니저로 애들 옮길것.
    /// 유형을 어떻게 관리할지는 내일의 내가 정해줄것.
    /// </summary>
    public static EventManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // 이벤트  리스트를 가져오는 매니저.
    public DialogEvents DialogEventList = new DialogEvents();
    public SelectionEvents SelectionEventList = new SelectionEvents();
    public BattleEvents BattleEventList = new BattleEvents();

    public EventType[] EventToEventType = new EventType[10000];

    public void LoadEvent(int eventCode)
    {
        Debug.Log(eventCode + "에 대한 이벤트 호출 요청 발생.");
        switch (EventToEventType[eventCode])
        {
            case EventType.DIALOG:
                Dialog.instance.Appear(DialogEventList.FindEvent(eventCode));
                break;
            case EventType.SELECTION:
                Selection.instance.Appear(SelectionEventList.FindEvent(eventCode));
                break;
            case EventType.BATTLE:
                BattleManager.instance.BattleStart(BattleEventList.FindEvent(eventCode));
                break;
        }
    }
}
