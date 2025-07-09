using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    DIALOG = 5,
    SELECTION = 6,
    LOOTING = 4,
}

public class Parser : MonoBehaviour
{
    public static Parser instance;
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



    /// <summary>
    /// Singletone
    /// </summary>

    public TextAsset textJSON;
    public TextAsset textDialogData;
    public TextAsset textSelectionData;
    public TextAsset textLootingData;

    public TextAsset skillData;

    public TextAsset OtherItems;
    public TextAsset WeaponItems;
    public TextAsset ConsumeItems;

    public TextAsset RecipeDatas;

    public TextAsset stageThemeDatas;
    

    private void Start()
    {
        if (!GameManager.instance.isFirstGame)
        {
            ParseAllData();
        }
    }

    public void ParseAllData()
    {
        // 다이얼로그 이벤트 데이터 로드.
        EventManager.instance.DialogEventList = JsonUtility.FromJson<DialogEvents>(textDialogData.text);
        for (int i = 0; i < EventManager.instance.DialogEventList.dialogEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.DialogEventList.dialogEvents[i].eventCode] = EventType.DIALOG; // 다이얼로그가 0...
        }

        // 선택지 이벤트 데이터 로드.
        EventManager.instance.SelectionEventList = JsonUtility.FromJson<SelectionEvents>(textSelectionData.text);
        for (int i = 0; i < EventManager.instance.SelectionEventList.selectionEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.SelectionEventList.selectionEvents[i].eventCode] = EventType.SELECTION;
        }

        // 루팅 데이터 로드.
        EventManager.instance.LootingEventList = JsonUtility.FromJson<LootingEvents>(textLootingData.text);
        for(int i = 0; i < EventManager.instance.LootingEventList.lootingEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.LootingEventList.lootingEvents[i].eventCode] = EventType.LOOTING;
        }

        // 스킬 데이터 로드.
        SkillDataBase.instance.skillDatas = JsonUtility.FromJson<SkillDatas>(skillData.text);


        // 테마 데이터 로드.
        ThemeDataBase.instance.stageThemeDatas = JsonUtility.FromJson<StageThemeDatas>(stageThemeDatas.text);

        ItemDataBase itemData = ItemDataBase.instance;

        // 기타 아이템 데이터 로드.
        itemData.otherItemList = JsonUtility.FromJson<OtherItems>(OtherItems.text);
        for (int i = 0; i < itemData.otherItemList.otherItems.Length; i++)
        {
            itemData.codeToItemType[itemData.otherItemList.otherItems[i].itemCode] = ItemType.OTHERS;
        }

        itemData.consumeItemList = JsonUtility.FromJson<ConsumeItems>(ConsumeItems.text);
        for(int i = 0; i < itemData.consumeItemList.consumeItems.Length; i++)
        {
            itemData.codeToItemType[itemData.consumeItemList.consumeItems[i].itemCode] = ItemType.CONSUMPTION;
        }


        // 조합 아이템 데이터 로드.
        itemData.recipeDatas = JsonUtility.FromJson<RecipeDatas>(RecipeDatas.text);

        // 아이템에 대한 모든 데이터 로드가 완료되었을 때.
        itemData.LoadStableStringData();

    }

}
