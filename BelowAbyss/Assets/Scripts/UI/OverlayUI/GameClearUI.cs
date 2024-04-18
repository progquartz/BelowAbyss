using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    public GameObject gameClearUI;
    public RectTransform textTransform;
    [SerializeField]
    private Canvas canvas;
    public float textSize;
    public float movingYPosPerSec = 500f;
    public float CanvasHeight;

    public bool isGameClearStarted = false;
    public float targetingPos;
    

    public void OnGameClear()
    {
        gameClearUI.SetActive(true);
        isGameClearStarted = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        textSize = textTransform.sizeDelta.y;
        Debug.Log("textSize = " + textSize);
        isGameClearStarted = false;
        CanvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        targetingPos = (textSize / 2) + CanvasHeight / 2;
    }

    public void OnClickRestartButton()
    {
        SceneLoader.SceneLoad("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameClearStarted)
        {
            Debug.Log("anchored = " + textTransform.anchoredPosition.y);
            Debug.Log("targeting = " + targetingPos);
            if (textTransform.anchoredPosition.y <= targetingPos)
            {
                textTransform.anchoredPosition = new Vector2(textTransform.anchoredPosition.x, textTransform.anchoredPosition.y + movingYPosPerSec * Time.deltaTime);
                Debug.Log(textTransform.anchoredPosition.y);
            }
            else
            {

            }

        }
    }
}
