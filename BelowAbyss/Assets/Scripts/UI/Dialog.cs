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
    [SerializeField]
    protected Image artworkImage;
    public GameObject dialogUI;
    public int nextEvent;
    public bool isNextEventExist;

    // 당장 안함.
    private AudioClip backgroundMusic;

    private void Start()
    {
        dialogUI = transform.GetChild(1).gameObject;
        paragraphText = transform.GetChild(1).GetChild(0).GetComponentInChildren<Text>();
        dialog = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        artworkImage = transform.GetChild(1).GetChild(1).GetComponent<Image>();
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
        
        LoadImageBaseOnCode(data.artworkpath);
        if (LoadBackgroundImageBaseOnCode(data.backgroundImage) == null)
        {
            backgroundImage.gameObject.SetActive(false);
        }
        else
        {
            backgroundImage.gameObject.SetActive(true);
        }
            //LoadImageBaseOnCode(data.artworkpath);
            // 이미지 및 여러 추가사항들 존재.
            return true;
    }


    public Sprite LoadImageBaseOnCode(int imageCode)
    {
        Sprite image;
        if(imageCode == 0)
        {
            return null;
        }
        string path = "Sprites/DialogArtwork/" + imageCode;
        image = Resources.Load<Sprite>(path);
        artworkImage.sprite = image;
        //Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity);
        return image;
    }

    public Sprite LoadBackgroundImageBaseOnCode(int imageCode)
    {
        Sprite image;
        if(imageCode == 0)
        {
            return null;
        }
        string path = "Textures/Background/" + imageCode;
        image = Resources.Load<Sprite>(path);
        backgroundImage.sprite = image;
        return image;
    }

    public void NextButtonPressed()
    {
        Disappear();
        backgroundImage.gameObject.SetActive(false);
        if (isNextEventExist)
        {
            EventManager.instance.LoadEvent(nextEvent);
        }
    }
}
