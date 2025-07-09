using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    protected TextMeshProUGUI paragraphText;
    [SerializeField]
    protected TextMeshProUGUI subparagraphText;
    [SerializeField]
    protected TextMeshProUGUI dialogNoImage;
    [SerializeField]
    protected TextMeshProUGUI dialogWithImage;
    [SerializeField]
    protected Image image;
    

    protected GameObject backgroundImage;
    public int nextEvent;
    public bool isNextEventExist;

    // 당장 안함.
    private AudioClip backgroundMusic;

    protected virtual void Start()
    {
        paragraphText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        subparagraphText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        dialogNoImage = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogWithImage = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        image = transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
    }


    /// <summary>
    /// 이벤트를 로드하는 함수. 만약에 정상적으로 로드되지 않았을 경우, false값을 내보냄.
    /// </summary>
    /// <param name="eventCode"></param>
    /// <returns></returns>
    public bool LoadEventCode(DialogEvent data)
    {
        paragraphText.text = data.paragraphText;
        dialogNoImage.text = data.dialog;
        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;

        return true;
    }

    [Obsolete]
    public void NextButtonPressed()
    {

        if (isNextEventExist)
        {
            // 조건이 맞나 체크 후 다시 없앨 것.
            if(false)
            {
                EventManager.instance.LoadEvent(nextEvent);
            }
            
        }
    }
}
