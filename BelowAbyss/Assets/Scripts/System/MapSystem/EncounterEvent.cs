using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 맵매니저에서 처음에 맵을 만들때 해당 테마 폴더에서 랜덤하게 차출되며, 
/// 이벤트에 진입하면 현재 관리되는 이벤트 중에서 가져온 데이터를 기반으로 저장된 상태의 데이터 온.
/// </summary>
public class EncounterEvent : MonoBehaviour
{
    private Image encounterEventSprite;
    [Header("호출 이벤트코드")]
    public int loadingEventCode;

    private EventEndUp currentEventEndUp = EventEndUp.DEFAULT;
    private EventEndUp changingEventEndUp = EventEndUp.DEFAULT;
    private Coroutine defaultAnimCoroutine;
    public float frameDelay = 0.125f;

    [Header("기본 스프라이트")]
    public Sprite[] defaultSprite;
    [Header("전투 종료시 스프라이트")]
    public Sprite battleEndSprite;

    [Header("선택지 1 선택")]
    public Sprite Selection1Sprite;
    [Header("선택지 2 선택")]
    public Sprite Selection2Sprite;
    [Header("선택지 3 선택")]
    public Sprite Selection3Sprite;
    [Header("선택지 4 선택")]
    public Sprite Selection4Sprite;

    private void Awake()
    {
        encounterEventSprite = this.GetComponent<Image>();
    }
    // 각 상태에 맞게 스프라이트가 변화함.
    public bool ChangeEncounterSprite(EventEndUp eventEndUpWith)
    {
        if ((!(MapManager.Instance.GetStageNum() == 0 && MapManager.Instance.GetTileNum() == 0)) || MapManager.Instance.mapVisual.isInitiatingEncounting)
        {
            changingEventEndUp = eventEndUpWith;
        }
        return false;
    }

    private void Update()
    {
        if(changingEventEndUp == currentEventEndUp)
        {
            currentEventEndUp = changingEventEndUp;

            if(currentEventEndUp == EventEndUp.DEFAULT)
            {
                // 애니메이션 진행
                defaultAnimCoroutine =  StartCoroutine(PlayDefaultAnimation());  
            }
        }
        else
        {
            currentEventEndUp = changingEventEndUp;
            StopCoroutine(defaultAnimCoroutine);
            switch (currentEventEndUp)
            {
                case EventEndUp.BATTLEEND:
                    if (battleEndSprite != null)
                    {
                        encounterEventSprite.sprite = battleEndSprite;
                    }
                    break;
                case EventEndUp.SELECTION1:
                    if (Selection1Sprite != null)
                    {
                        encounterEventSprite.sprite = Selection1Sprite;
                    }
                    break;
                case EventEndUp.SELECTION2:
                    if (Selection2Sprite != null)
                    {
                        encounterEventSprite.sprite = Selection2Sprite;
                    }
                    break;
                case EventEndUp.SELECTION3:
                    if (Selection3Sprite != null)
                    {
                        encounterEventSprite.sprite = Selection3Sprite;
                    }
                    break;
                case EventEndUp.SELECTION4:
                    if (Selection4Sprite != null)
                    {
                        encounterEventSprite.sprite = Selection4Sprite;
                    }
                    break;
            }
        }

        

    }

    private IEnumerator PlayDefaultAnimation()
    {
        int frameCount = defaultSprite.Length;
        int currentFrame = 0;

        while (true)
        {
            encounterEventSprite.sprite = defaultSprite[currentFrame];
            currentFrame = (currentFrame + 1) % frameCount;
            yield return new WaitForSeconds(frameDelay);
        }
    }

    public void ChangeData(EncounterEvent data)
    {
        loadingEventCode = data.loadingEventCode;
        defaultSprite = data.defaultSprite;
        battleEndSprite = data.battleEndSprite;
        Selection1Sprite = data.Selection1Sprite;
        Selection2Sprite = data.Selection2Sprite;
        Selection3Sprite = data.Selection3Sprite;
        Selection4Sprite = data.Selection4Sprite;
    }
}

public enum EventEndUp
{
    DEFAULT,
    BATTLEEND,
    SELECTION1,
    SELECTION2,
    SELECTION3,
    SELECTION4
}