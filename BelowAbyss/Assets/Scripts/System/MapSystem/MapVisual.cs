using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapVisual : MonoBehaviour
{

    public MapData mapdata;

    
    public GameObject mapFrontOfCharacter;
    public GameObject mapBehindOfCharacter;
    public RectTransform mapFrontPos;
    public RectTransform mapBackPos;
    public GameObject FadeInOutObject;
    public PlayerStatGauge playerStatWarningSound;

    public EncounterEvent currentEncounteringEventVisual;
    public int curEncountEventIndex = 0;
    public bool isInitiatingEncounting = true;
    public GameObject encounterEventVisualsParent;

    public Sprite testimage;

    public bool moveTrigger = false;
    public bool isMoving = false;
    public bool isLastRoom = false;
    public bool isSwitchingStage = false;
    public bool isStageFadeInOutRunning = false;

    private float out_min_PosX = -1575; // -1150;  
    private float fade_start_PosX = -1200; // -780; 
    private float lock_min_PosX = -625; // -230; 

    private float lock_max_PosX = 1350; // 1170; 
    private float out_max_PosX = 1625; // 1450; 


    private float fadeObjectMinPosX = -2610; // -2200;
    private float fadeObjectMidPosX = -538; // -150;
    private float fadeObjectMaxPosX = 2380; // 2200; 

    private float fadeObjectTime = 0.3f;
    private float fadeObjectWaitingTime = 1.0f;

    public int step = 5;
    private float per_step;
    public float beforeMarker;
    public float afterMarker;
    

    private void Start()
    {
        mapFrontPos = mapFrontOfCharacter.GetComponent<RectTransform>();
        mapBackPos = mapBehindOfCharacter.GetComponent<RectTransform>();

        per_step = Mathf.Abs((lock_max_PosX - lock_min_PosX) / step);
        // 점점 줄어들어가게 됨. 맵은 뒤로 움직여야 플레이어는 앞으로 움직임.
        beforeMarker = lock_max_PosX;
        afterMarker = beforeMarker - per_step;
    }
    public void MoveFront()
    {
        playerStatWarningSound.IsStatInDanger();
        moveTrigger = true;
        isMoving = true;
    }

    public void SwitchStage()
    {
        moveTrigger = true;
        isMoving = true;
        isLastRoom = true;
    }

    public void EncounterEventToggleSelection(int index)
    {
        Debug.Log(index.ToString() + "번째 버튼 누름");
        if(index == 0)
        {
            currentEncounteringEventVisual.ChangeEncounterSprite(EventEndUp.SELECTION1);
        }
        else if(index == 1)
        {
            currentEncounteringEventVisual.ChangeEncounterSprite(EventEndUp.SELECTION2);
        }
        else if(index == 2)
        {
            currentEncounteringEventVisual.ChangeEncounterSprite(EventEndUp.SELECTION3);
        }
        else if(index == 3)
        {
            currentEncounteringEventVisual.ChangeEncounterSprite(EventEndUp.SELECTION4);
        }
    }
    // 맵의 스프라이트를 테마에 맞게 교체
    public void ChangeMapSpriteTheme()
    {
        // 여기 이미지 로딩이 정상적으로 되지 않음.
        string mapThemePath = "Textures/UI/Map/" + MapManager.Instance.GetCurrentMapTheme();
        Debug.Log("Textures/UI/Map/" + MapManager.Instance.GetCurrentMapTheme() + "/MapFrontOfCharacter");
        mapFrontOfCharacter.GetComponent<Image>().sprite = IMG2Sprite.LoadNewSprite(mapThemePath + "/MapFrontOfCharacter") as Sprite;
        mapBehindOfCharacter.GetComponent<Image>().sprite = IMG2Sprite.LoadNewSprite(mapThemePath + "/MapBehindOfCharacter") as Sprite;
    }

    // 초기로 돌아오면서, 해당 맵의 초기 이벤트를 지정.
    public void ChangeMapEncounterEvent()
    {
        ChangeMapNormalEncounterEvent();
        ChangeMapLastEncounterEvent();
        curEncountEventIndex = 0;
        isInitiatingEncounting = true;
        currentEncounteringEventVisual = GetEncounterEventTransform(curEncountEventIndex).GetComponent<EncounterEvent>();
        // 이 부분을 로딩할때만 하게 만들어야함.
        if(MapManager.Instance.GetStageNum() == 0)
        {
            // 나머지 encounterevent의 visual이 변하는 부분은 fadein / fade out 작업 중에 할 것임.
            ChangeEncounterEventVisual();
        }
        isInitiatingEncounting = false;
    }

    private void ChangeEncounterEventVisual()
    {
        for(int i = 0; i < 5; i++)
        {
            GetEncounterEventTransform(i).GetComponent<EncounterEvent>().ChangeEncounterSprite(EventEndUp.DEFAULT);
        }
    }
    
    public void HeadToNextEvent()
    {
        if(curEncountEventIndex < 0)
        {
            curEncountEventIndex = 0;
        }
        Transform data = GetEncounterEventTransform(curEncountEventIndex);
        currentEncounteringEventVisual = data.GetComponent<EncounterEvent>();
        curEncountEventIndex++;
    }

    public int GetEncounterEventNormal(int index)
    {
        return GetEncounterEventTransform(index).GetComponent<EncounterEvent>().loadingEventCode;
    }

    public int GetEncounterEventLast()
    {
        return GetEncounterEventTransform(4).GetComponent<EncounterEvent>().loadingEventCode;
    }

    /// <summary>
    /// 일반 encounter event를 테마 데이터로부터 생성.
    /// </summary>
    private void ChangeMapNormalEncounterEvent()
    {
        string mapThemePathExAsset = "Textures/UI/Map/" + MapManager.Instance.GetCurrentMapTheme();

        int encounterNormalEventCounter = 0;
        Object[] data = Resources.LoadAll(mapThemePathExAsset + "/VisualEvent/NormalRoom/");
        Debug.Log(MapManager.Instance.GetCurrentMapTheme()+ " 에서 일반 이벤트 중 "+ data.Length+1 + "개의 이벤트 로드됨.");
        List<EncounterEvent> themeLoadedNormalEvents = new List<EncounterEvent>();
        for (int i = 0; i < data.Length; i++)
        {
            GameObject dataobj = data[i] as GameObject;
            //Debug.Log("일반 " + encounterNormalEventCounter + " 번째 이벤트 로드 (" + data[i].name + ")");
            themeLoadedNormalEvents.Add(dataobj.GetComponent<EncounterEvent>());
            encounterNormalEventCounter++;
        }

        int randomNum;
        // encountercounter를 기준으로 List
        for (int i = 0; i < 4; i++)
        {
            randomNum = Random.Range(0, encounterNormalEventCounter);
            //Debug.Log(i + "번째 방에" + themeLoadedNormalEvents[randomNum].name + "이벤트 로드됨");
            GetEncounterEventTransform(i).gameObject.GetComponent<EncounterEvent>().ChangeData(themeLoadedNormalEvents[randomNum]);
        }
    }

    /// <summary>
    /// 마지막 방의 encounter event를 테마 데이터로부터 생성.
    /// </summary>
    private void ChangeMapLastEncounterEvent()
    {
        string mapThemePath = "Assets/Resources/Textures/UI/Map/" + MapManager.Instance.GetCurrentMapTheme();
        string mapThemePathExAsset = "Textures/UI/Map/" + MapManager.Instance.GetCurrentMapTheme();

        int encounterLastEventCounter = 0;
        Object[] data = Resources.LoadAll(mapThemePathExAsset + "/VisualEvent/LastRoom/");
        Debug.Log(MapManager.Instance.GetCurrentMapTheme() + "의 마지막 방에서 " +data.Length+1 + "개의 이벤트 로드.");
        List<EncounterEvent> themeLoadedLastEvents = new List<EncounterEvent>();
        for (int i = 0; i < data.Length; i++)
        {
            GameObject dataobj = data[i] as GameObject;
            //Debug.Log("보스" + encounterLastEventCounter + " 번째 이벤트 로드 (" +  data[i].name + ")");
            themeLoadedLastEvents.Add(dataobj.GetComponent<EncounterEvent>());
            encounterLastEventCounter++;
        }

        int randomNum = Random.Range(0, encounterLastEventCounter);
        GetEncounterEventTransform(4).gameObject.GetComponent<EncounterEvent>().ChangeData(themeLoadedLastEvents[randomNum]);
    }

    private Transform GetEncounterEventTransform(int index)
    {
        return encounterEventVisualsParent.transform.GetChild(index);
    }

    private void Update()
    {
        // 일반적인 전진의 경우.
        if (moveTrigger && !isLastRoom)
        {
            if(mapFrontPos.localPosition.x > afterMarker)
            {
                isMoving = true;
                mapFrontPos.localPosition = new Vector3(mapFrontPos.localPosition.x - (per_step * 0.5f * Time.deltaTime) , mapFrontPos.localPosition.y, 0);
                mapBackPos.localPosition = new Vector3(mapBackPos.localPosition.x - (per_step * 0.5f * Time.deltaTime), mapBackPos.localPosition.y, 0);
            }
            else
            {
                isMoving = false;
                moveTrigger = false;
                beforeMarker = afterMarker;
                afterMarker = beforeMarker - per_step;
                if(afterMarker < lock_min_PosX)
                {
                    afterMarker = lock_min_PosX;
                }
            }
        }
        // 가장 마지막의 맵의 전진 (맵 교체)의 경우. (달려나가기)
        else if(moveTrigger && isLastRoom)
        {
            afterMarker = out_min_PosX;
            if (mapFrontPos.localPosition.x > afterMarker)
            {
                isMoving = true;
                mapFrontPos.localPosition = new Vector3(mapFrontPos.localPosition.x - (per_step * 3.0f * Time.deltaTime), mapFrontPos.localPosition.y, 0);
                mapBackPos.localPosition = new Vector3(mapBackPos.localPosition.x - (per_step * 3.0f * Time.deltaTime), mapBackPos.localPosition.y, 0);
                PlayerAnimation.instance.animator.speed = 3.0f;
            }
            else
            {
                isSwitchingStage = true;
            }
            // 페이드아웃 실행.
            if(mapFrontPos.localPosition.x < fade_start_PosX && !isStageFadeInOutRunning)
            {
                isStageFadeInOutRunning = true;
                if (!FadeInOutObject.activeInHierarchy)
                {
                    StartCoroutine(FadeAndResetMap());
                }
            }
        }
        // 페이드아웃 
        else if(moveTrigger && isSwitchingStage && !isStageFadeInOutRunning)
        {
            if (mapFrontPos.localPosition.x > afterMarker)
            {
                isMoving = true;
                mapFrontPos.localPosition = new Vector3(mapFrontPos.localPosition.x - (per_step * 0.5f * Time.deltaTime), mapFrontPos.localPosition.y, 0);
                mapBackPos.localPosition = new Vector3(mapBackPos.localPosition.x - (per_step * 0.5f * Time.deltaTime), mapBackPos.localPosition.y, 0);
                PlayerAnimation.instance.animator.speed = 1.0f;
            }
            else
            {
                isMoving = false;
                moveTrigger = false;
                beforeMarker = afterMarker;
                afterMarker = beforeMarker - per_step;
                if (afterMarker < lock_min_PosX)
                {
                    afterMarker = lock_min_PosX;
                }
            }
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void TestFadeOut()
    {
        StartCoroutine(FadeAndResetMap());
    }
    /// <summary>
    /// 페이드인을 하고, 맵을 리셋하는 ienumerator.
    /// 이는 encounter eventvisual 또한 교체함.
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeAndResetMap()
    {
        FadeInOutObject.SetActive(true);
        
        Vector3 pos = FadeInOutObject.transform.localPosition;
        for (float f = fadeObjectMaxPosX; f > fadeObjectMidPosX;)
        {
            FadeInOutObject.transform.localPosition = new Vector3(f, pos.y, pos.z);
            f -= Mathf.Abs(fadeObjectMaxPosX - fadeObjectMidPosX) * Time.deltaTime / fadeObjectTime;
            if(f <= fadeObjectMidPosX)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(fadeObjectWaitingTime);
        isStageFadeInOutRunning = false;
        // 맵 첫 위치로 돌리기.

        // 마지막 방 스프라이트의 교체 전에 맵 로딩이 되어서 encounter eventvisual 교체가 되면 안되기 때문에 레이드인 도중에 설정.
        ChangeEncounterEventVisual();
        ChangeMapSpriteTheme();
        mapFrontPos.localPosition = new Vector3(out_max_PosX, mapFrontPos.localPosition.y, 0);
        mapBackPos.localPosition = new Vector3(out_max_PosX, mapBackPos.localPosition.y, 0);
        beforeMarker = out_max_PosX;
        afterMarker = lock_max_PosX;
        isLastRoom = false;
        for (float f = fadeObjectMidPosX; f > fadeObjectMinPosX;)
        {
            FadeInOutObject.transform.localPosition = new Vector3(f, pos.y, pos.z);
            f -= Mathf.Abs(fadeObjectMidPosX - fadeObjectMinPosX) * Time.deltaTime / fadeObjectTime;
            if (f <= fadeObjectMinPosX)
            {
                break;
            }
            yield return null;
        }
        FadeInOutObject.transform.localPosition = new Vector3(fadeObjectMaxPosX, FadeInOutObject.transform.localPosition.y, FadeInOutObject.transform.localPosition.z);
        FadeInOutObject.SetActive(false);
        PlayerAnimation.instance.animator.speed = 1.0f;


    }
}
