using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum UIHolderList
{
    Setting = 0,
    Inventory = 1,
    Dialog = 2,
    Skill = 3,
    Trait = 4
}

public class UISelectionHolder : MonoBehaviour
{
    [SerializeField]
    private UIHolderList currentUIOpened = UIHolderList.Inventory;

    [SerializeField]
    private Sprite[] iconsUnselected;
    [SerializeField]
    private Sprite[] iconSelected;
    [SerializeField]
    private GameObject[] uiList;

    private GameObject[] iconSelectedBar = new GameObject[5];
    private Image[] iconButtonImage = new Image[5];


    private void Awake()
    {
        for(int i = 0; i < 5;  i++)
        {
            iconSelectedBar[i] = transform.GetChild(0).GetChild(i).gameObject;
            iconButtonImage[i] = transform.GetChild(1).GetChild(i).GetComponent<Image>();
        }
        OpenUI(1);
    }

    public void OpenUI(int uiHolder)
    {
        if(((UIHolderList)uiHolder == currentUIOpened))
        {
            Debug.Log("현재와 같은 UI를 열려고 합니다.");
        }
        else
        {
            iconButtonImage[(int)currentUIOpened].sprite = iconsUnselected[(int)currentUIOpened];
            iconButtonImage[(int)uiHolder].sprite = iconSelected[(int)uiHolder];
            iconSelectedBar[(int)currentUIOpened].SetActive(false);
            iconSelectedBar[(int)uiHolder].SetActive(true);
            currentUIOpened = (UIHolderList)uiHolder;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
