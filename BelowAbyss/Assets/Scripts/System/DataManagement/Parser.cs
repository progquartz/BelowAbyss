using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    DIALOG = 5,
    SELECTION = 6
}

public class Parser : MonoBehaviour
{
    /// <summary>
    /// Singletone
    /// </summary>

    public TextAsset textJSON;
    public TextAsset textJSON2;
    public TextAsset textJSON3;



    private void Start()
    {
        EventManager.instance.DialogEventList = JsonUtility.FromJson<DialogEvents>(textJSON2.text);
        for(int i = 0; i < EventManager.instance.DialogEventList.dialogEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.DialogEventList.dialogEvents[i].eventCode] = EventType.DIALOG; // 다이얼로그가 0...
        }

        EventManager.instance.SelectionEventList = JsonUtility.FromJson<SelectionEvents>(textJSON3.text);
        for(int i = 0; i < EventManager.instance.SelectionEventList.selectionEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.SelectionEventList.selectionEvents[i].eventCode] = EventType.SELECTION;
        }

    }

}
