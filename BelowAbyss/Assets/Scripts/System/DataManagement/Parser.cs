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

    
    
    private void Start()
    {
        EventManager.instance.DialogEventList = JsonUtility.FromJson<DialogEvents>(textJSON2.text);
        for(int i = 0; i < EventManager.instance.DialogEventList.dialogEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.DialogEventList.dialogEvents[i].eventCode] = EventType.DIALOG; // ���̾�αװ� 0...
        }
    }

}