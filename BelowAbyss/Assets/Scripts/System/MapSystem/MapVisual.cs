using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapVisual : MonoBehaviour
{

    public MapData mapdata;

    
    public GameObject mapFront;
    public GameObject mapBack;
    public RectTransform mapFrontPos;
    public RectTransform mapBackPos;
    public GameObject FadeInOutObject;
    public PlayerStatGauge playerStatWarningSound;

    public bool moveTrigger = false;
    public bool isMoving = false;
    public bool isLastRoom = false;
    public bool isSwitchingStage = false;
    public bool isStageFadeInOutRunning = false;

    private float out_min_PosX = -1000;
    private float fade_start_PosX = -570;
    private float lock_min_PosX = -90;
    
    private float lock_max_PosX = 840;
    private float out_max_PosX = 1080;


    private float fadeObjectMinPosX = -1850;
    private float fadeObjectMidPosX = 0;
    private float fadeObjectMaxPosX = 1850;
    private float fadeObjectTime = 0.3f;
    private float fadeObjectWaitingTime = 1.0f;

    public int step = 5;
    private float per_step;
    public float beforeMarker;
    public float afterMarker;


    private void Start()
    {
        mapFrontPos = mapFront.GetComponent<RectTransform>();
        mapBackPos = mapBack.GetComponent<RectTransform>();

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
    
    private void Update()
    {
        if(moveTrigger && !isLastRoom)
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
            Debug.Log(mapFrontPos.localPosition.x + " /// " + fade_start_PosX);
            if(mapFrontPos.localPosition.x < fade_start_PosX && !isStageFadeInOutRunning)
            {
                Debug.Log("OOO");
                isStageFadeInOutRunning = true;
                if (!FadeInOutObject.activeInHierarchy)
                {
                    StartCoroutine(FadeAndResetMap());
                }
            }
        }
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
            Debug.Log("??" + f);
            yield return null;
        }

        yield return new WaitForSeconds(fadeObjectWaitingTime);
        isStageFadeInOutRunning = false;
        // 맵 첫 위치로 돌리기.
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
            Debug.Log("!!" + f);
            yield return null;
        }
        FadeInOutObject.transform.localPosition = new Vector3(fadeObjectMaxPosX, FadeInOutObject.transform.localPosition.y, FadeInOutObject.transform.localPosition.z);
        FadeInOutObject.SetActive(false);
        PlayerAnimation.instance.animator.speed = 1.0f;


    }
}
