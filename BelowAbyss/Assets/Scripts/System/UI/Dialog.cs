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

    // ���� ����.
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
            Debug.Log("���������� " + data.eventCode + " ���� �̺�Ʈ �ε� �Ϸ�.");
        }
        else
        {
            Debug.Log( data.eventCode + " ���� �̺�Ʈ �ε� �� ���� �߻�.");
        }
    }

    public void Disappear()
    {
        dialogUI.SetActive(false);
    }

    /// <summary>
    /// �̺�Ʈ�� �ε��ϴ� �Լ�. ���࿡ ���������� �ε���� �ʾ��� ���, false���� ������.
    /// </summary>
    /// <param name="eventCode"></param>
    /// <returns></returns>
    public bool LoadEventCode(DialogEvent data)
    {
        paragraphText.text = data.paragraphText;
        dialog.text = data.dialog;
        nextEvent = data.additionalEventCode;
        isNextEventExist = data.isAdditionalEvent;
        
        // �̹��� �� ���� �߰����׵� ����.
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
