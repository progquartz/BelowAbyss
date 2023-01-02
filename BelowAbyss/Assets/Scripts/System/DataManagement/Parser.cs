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

    public TextAsset OtherItems;
    public TextAsset RecipeDatas;

    public TextAsset EnemyDatas;



    private void Start()
    {
        // 다이얼로그 이벤트 데이터 로드.
        EventManager.instance.DialogEventList = JsonUtility.FromJson<DialogEvents>(textJSON2.text);
        for(int i = 0; i < EventManager.instance.DialogEventList.dialogEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.DialogEventList.dialogEvents[i].eventCode] = EventType.DIALOG; // 다이얼로그가 0...
        }

        // 선택지 이벤트 데이터 로드.
        EventManager.instance.SelectionEventList = JsonUtility.FromJson<SelectionEvents>(textJSON3.text);
        for(int i = 0; i < EventManager.instance.SelectionEventList.selectionEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.SelectionEventList.selectionEvents[i].eventCode] = EventType.SELECTION;
        }

        // 기타 아이템 데이터 로드.
        ItemDataBase.instance.otherItemList = JsonUtility.FromJson<OtherItems>(OtherItems.text);
        for(int i = 0; i < ItemDataBase.instance.otherItemList.otherItems.Length; i++)
        {
            ItemDataBase.instance.codeToItemType[ItemDataBase.instance.otherItemList.otherItems[i].itemCode] = ItemType.OTHERS;
        }

        // 조합 아이템 데이터 로드.
        ItemDataBase.instance.recipeDatas = JsonUtility.FromJson<RecipeDatas>(RecipeDatas.text);

        // 아이템에 대한 모든 데이터 로드가 완료되었을 때.
        ItemDataBase.instance.LoadStableStringData();

        // 적들에 대한 모든 데이터 로드.
        EnemyDataBase.instance.enemies = JsonUtility.FromJson<Enemies>(EnemyDatas.text);
    }

}
