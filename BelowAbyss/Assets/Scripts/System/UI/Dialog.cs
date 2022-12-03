using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public static Dialog instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    protected Text paragraphText;
    protected Text dialog;
    protected Image backgroundImage;
    protected Image artworkpath;
    public GameObject dialogUI;
    public int nextEvent;
    public bool isNextEventExist;

    // 당장 안함.
    private AudioClip backgroundMusic;

    private void Start()
    {
        dialogUI = transform.GetChild(0).gameObject;
        paragraphText = transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        dialog = transform.GetChild(0).GetChild(2).GetComponent<Text>();
        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        artworkpath = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void Appear()
    {
        dialogUI.SetActive(true);
    }

    public void Appear(DialogEvent data)
    {
        dialogUI.SetActive(true);
        if (LoadEventCode(data))
        {
            Debug.Log("정상적으로 " + data.eventCode + " 번의 이벤트 로드 완료.");
        }
        else
        {
            Debug.Log( data.eventCode + " 번의 이벤트 로드 중 문제 발생.");
        }
    }

    public void Disappear()
    {
        dialogUI.SetActive(false);
    }

    /// <summary>
    /// 이벤트를 로드하는 함수. 만약에 정상적으로 로드되지 않았을 경우, false값을 내보냄.
    /// </summary>
    /// <param name="eventCode"></param>
    /// <returns></returns>
    public bool LoadEventCode(DialogEvent data)
    {
        paragraphText.text = data.paragraphText;
        dialog.text = data.dialog;
        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;
        
        // 이미지 및 여러 추가사항들 존재.
        return true;
    }

    public Image LoadImageBaseOnCode(int imageCode)
    {
        return null;
    }

    public Image LoadBackgroundImageBaseOnCode(int imageCode)
    {
        return null;
    }

    public void NextButtonPressed()
    {
        Disappear();
        if (isNextEventExist)
        {
            EventManager.instance.LoadEvent(nextEvent);
        }
    }
}
