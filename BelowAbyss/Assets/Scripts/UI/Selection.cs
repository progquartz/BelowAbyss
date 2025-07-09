using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Selection : Dialog
{
    public new static Selection instance; // 기본 멤버인 Dialog 숨기기.
    private bool isSelectionEventloaded = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform noImageSelectionParent;
    public Transform withImageSelectionParent;
    public TextMeshProUGUI[] selectionNoImageDialog = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] selectionWithImageDialog = new TextMeshProUGUI[4];
    public GameObject[] selectionNoImagePanel = new GameObject[4];
    public GameObject[] selectionWithImagePanel = new GameObject[4];

    public int[] selectionEvent = new int[4];

    private readonly string imagesPath = "Textures/Events/";

    public void Appear(SelectionEvent data)
    {
        if (LoadEventCode(data))
        {
            Debug.Log("정상적으로 " + data.eventCode + " 번의 이벤트 로드 완료.");
        }
        else
        {
            Debug.Log(data.eventCode + " 번의 이벤트 로드 중 문제 발생.");
        }
    }

    public void Appear(DialogEvent data)
    {
        if (LoadEventCode(data))
        {
            Debug.Log("정상적으로 " + data.eventCode + " 번의 이벤트 로드 완료.");
        }
        else
        {
            Debug.Log(data.eventCode + " 번의 이벤트 로드 중 문제 발생.");
        }
    }

    protected override void Start()
    {
        base.Start();
        noImageSelectionParent = transform.GetChild(0).GetChild(2);
        withImageSelectionParent = transform.GetChild(0).GetChild(3);
        for (int i = 0; i < 4; i++)
        {
            selectionNoImagePanel[i] = transform.GetChild(0).GetChild(2).GetChild(i).gameObject;
            selectionNoImageDialog[i] = selectionNoImagePanel[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            selectionWithImagePanel[i] = transform.GetChild(0).GetChild(3).GetChild(i).gameObject;
            selectionWithImageDialog[i] = selectionWithImagePanel[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        }

    }

    public new bool LoadEventCode(DialogEvent data)
    {
        Debug.Log("다이얼로그 이벤트 로드됨.");
        isSelectionEventloaded = false;
        paragraphText.text = data.paragraphText;

        string additionalLootDialog = GetAdditionalEventCode(data);

        if (additionalLootDialog != null)
        {
            if (data.backgroundImageKey == null)
                dialogNoImage.text = data.dialog + "\n" + additionalLootDialog;
            else
                dialogWithImage.text = data.dialog + "\n" + additionalLootDialog;
        }
        else
        {
            if (data.backgroundImageKey == null)
                dialogNoImage.text = data.dialog;
            else
                dialogWithImage.text = data.dialog;
        }

        if (data.backgroundImageKey != null)
        {
            LoadImage(data.backgroundImageKey);
        }

        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;

        for (int i = 0; i < 1; i++)
        {
            if(data.backgroundImageKey == null)
            {
                selectionNoImageDialog[i].text = "계속"; // 이벤트 데이터가 주어진 놈은 하고, 아닌 경우는 지워버리기.
                AppearPanel(i, false);
            }
            else
            {
                selectionWithImageDialog[i].text = "계속";
                AppearPanel(i, true);
            }
        }
        for (int i = 1; i < 4; i++)
        {
            if (data.backgroundImageKey == null)
            {
                DisappearPanel(i, false);
            }
            else
            {
                DisappearPanel(i, true);
            }
        }

        return true;
    }

    public bool LoadEventCode(SelectionEvent data)
    {
        Debug.Log("선택 이벤트 로드됨.");
        isSelectionEventloaded = true;
        paragraphText.text = data.paragraphText;
        string additionalLootDialog = GetAdditionalEventCode(data);

        if (additionalLootDialog != null)
        {
            if (data.backgroundImageKey == null)
                dialogNoImage.text = data.dialog + "\n" + additionalLootDialog;
            else
                dialogWithImage.text = data.dialog + "\n" + additionalLootDialog;
        }
        else
        {
            if (data.backgroundImageKey == null)
                dialogNoImage.text = data.dialog;
            else
                dialogWithImage.text = data.dialog;
        }

        if(data.backgroundImageKey != null)
        {
            LoadImage(data.backgroundImageKey);
        }


        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;

        for (int i = 0; i < data.selectionDialog.Length; i++)
        {
            if (data.backgroundImageKey == null)
            {
                selectionNoImageDialog[i].text = data.selectionDialog[i]; // 이벤트 데이터가 주어진 놈은 하고, 아닌 경우는 지워버리기.
                AppearPanel(i, false);
            }
            else
            {
                Debug.Log(data.selectionDialog[i]);
                selectionWithImageDialog[i].text = data.selectionDialog[i];
                AppearPanel(i, true);
            }
        }
        for (int i = data.selectionDialog.Length; i < 4; i++)
        {
            if(data.backgroundImageKey == null)
            {
                DisappearPanel(i, false);
            }
            else
            {
                DisappearPanel(i, true);
            }
            
        }
        selectionEvent = data.selectionEvent;
        // 이미지 및 여러 추가사항들 존재.
        return true;
    }

    private void LoadImage(string key)
    {
        Sprite sprite = Resources.Load<Sprite>(imagesPath + key);
        if(sprite == null)
        {
            Debug.LogWarning("이미지가 비정상적으로 로드되었습니다.");
            image.gameObject.SetActive(false);
        }
        image.sprite = sprite;
    }

    private string GetAdditionalEventCode(Event data)
    {
        if (!data.isAdditionalEvent)
            return null;

        if (EventManager.instance.EventToEventType[data.additionalEventCode] == EventType.LOOTING)
        {
            string lootResult = EventManager.instance.LoadLootingEvent(data.additionalEventCode);
            return lootResult;
        }
        return null;
    }




    public void AppearPanel(int index, bool isImageIncluded)
    {
        if(!isImageIncluded)
        {
            dialogNoImage.gameObject.SetActive(true);
            dialogWithImage.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            noImageSelectionParent.gameObject.SetActive(true);
            withImageSelectionParent.gameObject.SetActive(false);
            selectionNoImagePanel[index].SetActive(true);
            selectionWithImagePanel[index].SetActive(false);
        }
        else
        {
            dialogNoImage.gameObject.SetActive(false);
            dialogWithImage.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            noImageSelectionParent.gameObject.SetActive(false);
            withImageSelectionParent.gameObject.SetActive(true);
            selectionWithImagePanel[index].SetActive(true);
            selectionNoImagePanel[index].SetActive(false);
        }
        
    }

    public void DisappearPanel(int index, bool isImageIncluded)
    {
        if(!isImageIncluded)
        {
            selectionNoImagePanel[index].SetActive(false);
        }
        else
        {
            selectionWithImagePanel[index].SetActive(false);
        }
        
    }

    private void CleanUp()
    {
        paragraphText.text = "";
        subparagraphText.text = "";
        dialogNoImage.text = "";

        for (int i = 0; i < 4; i++)
        {
            DisappearPanel(i, true);
            DisappearPanel(i, false);
        }
    }

    public void SelectionPressed(int index)
    {
        CleanUp();
        if(isSelectionEventloaded)
        {
            MapManager.Instance.mapVisual.EncounterEventToggleSelection(index);
            EventManager.instance.LoadEvent(selectionEvent[index]);
        }
        else
        {
            Debug.Log("버튼 누름");
            MapManager.Instance.mapVisual.EncounterEventToggleSelection(index);
            MapManager.Instance.MoveFront();
        }
        
    }

}
