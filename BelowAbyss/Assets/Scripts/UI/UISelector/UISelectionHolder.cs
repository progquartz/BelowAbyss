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
    public static UISelectionHolder instance;

    [SerializeField]
    private UIHolderList currentUIOpened = UIHolderList.Inventory;

    [SerializeField]
    private Sprite[] iconsUnselected;
    [SerializeField]
    private Sprite[] iconSelected;
    [SerializeField]
    private GameObject[] uiList;
    [SerializeField]
    private UISelectionButtonToggler[] itemToggledList;

    private GameObject[] iconSelectedBar = new GameObject[5];
    private Image[] iconButtonImage = new Image[5];


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < 5;  i++)
        {
            iconSelectedBar[i] = transform.GetChild(1).GetChild(i).gameObject;
            iconButtonImage[i] = transform.GetChild(2).GetChild(i).GetComponent<Image>();
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
            itemToggledList[uiHolder].ToggleOff();
            iconButtonImage[(int)currentUIOpened].sprite = iconsUnselected[(int)currentUIOpened];
            iconButtonImage[(int)uiHolder].sprite = iconSelected[(int)uiHolder];
            iconSelectedBar[(int)currentUIOpened].SetActive(false);
            iconSelectedBar[(int)uiHolder].SetActive(true);


            for(int i = 0; i < uiList[(int)currentUIOpened].transform.childCount; i++)
            {
                uiList[(int)currentUIOpened].transform.GetChild(i).gameObject.SetActive(false);
            }

            for(int i = 0; i < uiList[(int)uiHolder].transform.childCount; i++)
            {
                uiList[(int)uiHolder].transform.GetChild(i).gameObject.SetActive(true);
            }
               
            currentUIOpened = (UIHolderList)uiHolder;
        }
    }

    /// <summary>
    /// 0 = setting
    /// 1 = Inventory
    /// 2 = dialog
    /// 3 = skill
    /// 4 = trait
    /// </summary>
    /// <param name="uiHolder"></param>
    public void NewToggleUI(int uiHolder)
    {
        itemToggledList[uiHolder].ToggleOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
