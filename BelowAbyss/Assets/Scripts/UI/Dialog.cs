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
    protected TextMeshProUGUI dialog;
    
    protected GameObject backgroundImage;
    public int nextEvent;
    public bool isNextEventExist;

    // 당장 안함.
    private AudioClip backgroundMusic;

    private void Start()
    {
        paragraphText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        subparagraphText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        dialog = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
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

        return true;
    }

    public void NextButtonPressed()
    {
        if (isNextEventExist)
        {
            EventManager.instance.LoadEvent(nextEvent);
        }
    }
}
