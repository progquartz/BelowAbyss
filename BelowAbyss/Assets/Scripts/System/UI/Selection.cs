using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : Dialog
{
    public new static Selection instance; // 기본 멤버인 Dialog 숨기기.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Text[] selectionDialog = new Text[4];
    public GameObject[] selectionPanel = new GameObject[4];
    public int[] selectionEvent = new int[4];

    public void Appear(SelectionEvent data)
    {
        dialogUI.SetActive(true);
        if (LoadEventCode(data))
        {
            Debug.Log("정상적으로 " + data.eventCode + " 번의 이벤트 로드 완료.");
        }
        else
        {
            Debug.Log(data.eventCode + " 번의 이벤트 로드 중 문제 발생.");
        }
    }

    private void Start()
    {
        dialogUI = transform.GetChild(1).gameObject;
        paragraphText = transform.GetChild(1).GetChild(0).GetComponentInChildren<Text>();
        dialog = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        artworkImage = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        for(int i = 0; i < 4; i++)
        {
            selectionPanel[i] = transform.GetChild(1).GetChild(3).GetChild(i).gameObject;
            selectionDialog[i] = selectionPanel[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
        }

    }

    public bool LoadEventCode(SelectionEvent data)
    {
        paragraphText.text = data.paragraphText;
        dialog.text = data.dialog;
        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;
        artworkImage.sprite = LoadImageBaseOnCode(data.artworkpath);

        for (int i = 0; i < data.selectionDialog.Length; i++)
        {
            selectionDialog[i].text = data.selectionDialog[i]; // 이벤트 데이터가 주어진 놈은 하고, 아닌 경우는 지워버리기.
            AppearPanel(i);
        }
        for (int i = data.selectionDialog.Length; i < 4; i++)
        {
            DisappearPanel(i);
        }
        selectionEvent = data.selectionEvent;
        // 이미지 및 여러 추가사항들 존재.
        return true;
    }

    public void AppearPanel(int index)
    {
        selectionPanel[index].SetActive(true);
    }

    public void DisappearPanel(int index)
    {
        selectionPanel[index].SetActive(false);
    }

    public void SelectionPressed(int index)
    {
        Disappear();
        backgroundImage.gameObject.SetActive(false);
        EventManager.instance.LoadEvent(selectionEvent[index]);
    }

}
