using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    DIALOG = 5,
    SELECTION = 6,
    BATTLE = 2
}

public class Parser : MonoBehaviour
{
    public static Parser instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
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

    public TextAsset textBattleData;
    public TextAsset skillData;

    public TextAsset OtherItems;
    public TextAsset RecipeDatas;

    public TextAsset EnemyDatas;

    public TextAsset ThemeDatas;
    public TextAsset stageThemeDatas;

    private void Start()
    {
        //ParseAllData();
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

        // 스킬 데이터 로드.
        SkillDataBase.instance.skillDatas = JsonUtility.FromJson<SkillDatas>(skillData.text);

        // 전투 이벤트 데이터 로드.
        EventManager.instance.BattleEventList = JsonUtility.FromJson<BattleEvents>(textBattleData.text);
        for (int i = 0; i < EventManager.instance.BattleEventList.battleEvents.Length; i++)
        {
            EventManager.instance.EventToEventType[EventManager.instance.BattleEventList.battleEvents[i].eventCode] = EventType.BATTLE;
        }

        // 테마 데이터 로드.
        ThemeDataBase.instance.themes = JsonUtility.FromJson<Themes>(ThemeDatas.text);
        ThemeDataBase.instance.stageThemeDatas = JsonUtility.FromJson<StageThemeDatas>(stageThemeDatas.text);

        // 기타 아이템 데이터 로드.
        ItemDataBase.instance.otherItemList = JsonUtility.FromJson<OtherItems>(OtherItems.text);
        for (int i = 0; i < ItemDataBase.instance.otherItemList.otherItems.Length; i++)
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
