using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 �̺�Ʈ �Ŵ����� �̺�Ʈ�� ���õ� �������� �۾��� �����Ѵ�.
 �̺�Ʈ�� ü������ �̷���� ���� �̺�Ʈ���� �����ϰ� �����ϸ�, ���� �ý��۵�� ����ǰ� �ȴ�.
 
- ���̾�α� �ý���.
- ������
- ���� �ý���
- ���� �ý��� (�κ��丮)
     
    �̺�Ʈ ������ ���� Ŭ������ �������� ������, �� ȣ���� ���� �̺�Ʈ�� ������ ���� ���𰡸� �ϸ�ȴ�.
    
*/


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

public class EventManager : MonoBehaviour
{
    /// <summary>
    /// �̺�Ʈ �Ŵ�����. ��� ������ �����ϰ� ������. �ļ����� �̺�Ʈ �Ŵ����� �ֵ� �ű��.
    /// ������ ��� ���������� ������ ���� �����ٰ�.
    /// </summary>
    public static EventManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    public DialogEvents DialogEventList = new DialogEvents();
    public EventType[] EventToEventType = new EventType[10000];

    public void LoadEvent(int eventCode)
    {
        Debug.Log(eventCode + "�� ���� �̺�Ʈ ȣ�� ��û �߻�.");
        switch (EventToEventType[eventCode])
        {
            case EventType.DIALOG:
                Dialog.instance.Appear(DialogEventList.FindEvent(eventCode));
                break;
            case EventType.SELECTION:
                break;
        }
    }
}
