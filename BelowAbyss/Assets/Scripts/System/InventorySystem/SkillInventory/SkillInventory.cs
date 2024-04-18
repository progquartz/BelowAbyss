using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventory : MonoBehaviour
{
    public static SkillInventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public List<SkillData> unusedSkillDB; // 전체 인벤 데이터.
    public List<int> usingSkillDB; // 사용 스킬 인벤 데이터.
    public List<SkillSlot> usingIngameSkillUI;
    public List<SkillSlot> usingSkillUI;
    public List<ToggleNewImageSlot> toggleImages;

    public int unusedSlotCount = 32;
    public int usingSlotCount = 8;

    public int skillDBCount = 80;
    public int lastSkillAvailableIndex = 0;
    public int visualSlotYIndex = 0;

    [SerializeField]
    private List<GameObject> unusedSlot;
    [SerializeField]
    private List<GameObject> usingInvenSlot;

    [SerializeField]
    private GameObject upButton;
    [SerializeField]
    private GameObject downButton;
    private List<int> ToggledSkillList = new List<int>();

    public void ToggleInventory()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        FirstSetup();
    }
    /// <summary>
    ///  이 함수는 GameManager에서 실행되고 있음.
    /// </summary>
    public void FirstSetup()
    {
        unusedSkillDB = new List<SkillData>();
        usingIngameSkillUI = new List<SkillSlot>();

        for (int i = 0; i < unusedSlotCount; i++)
        {
            unusedSkillDB.Add(new SkillData());
            unusedSlot.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).gameObject);
            toggleImages.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(6).GetComponent<ToggleNewImageSlot>());
        }
        GameObject usingSkillSlotParent = GameObject.Find("SkillSlots");
        for(int i = 0; i < usingSlotCount; i++)
        {
            usingInvenSlot.Add(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
            usingSkillUI.Add(usingSkillSlotParent.transform.GetChild(i).GetComponent<SkillSlot>());
            usingIngameSkillUI.Add(usingSkillSlotParent.transform.GetChild(i).GetComponent<SkillSlot>());
            usingSkillDB.Add(-1);
        }
        for(int i = 0; i < skillDBCount; i++)
        {
            unusedSkillDB.Add(new SkillData());
        }

        

    }

    public void PressPlusButton(int buttonIndex)
    {
        int slotIndex = GetEmptySlot();
        usingSkillDB[slotIndex] = buttonIndex;
        usingSkillUI[slotIndex].SetupSkillData(unusedSkillDB[buttonIndex]);
    }

    public void PressMinusButton(int buttonIndex)
    {
        for(int i = 0; i < 8; i++)
        {
            if(usingSkillDB[i] == buttonIndex)
            {
                usingSkillDB[i] = -1;
                usingSkillUI[i].DeleteSkillData();
            }
        }
    }


    public void EnqueueToggle(int skillCode)
    {
        int newIndex = -1;
        for (int i = 0; i < skillDBCount; i++) // 모든 슬롯카운터에 한해.
        {
            if (unusedSkillDB[i].skillCode == skillCode) // 해당 슬롯에 아이템이 존재한다면.
            {
                newIndex = i;
                break;
            }
        }
        bool isItemIndexAvailable = false;
        for (int i = 0; i < ToggledSkillList.Count; i++)
        {
            if (ToggledSkillList[i] == newIndex)
            {
                isItemIndexAvailable = true;
                break;
            }
        }

        if (!isItemIndexAvailable)
        {
            ToggledSkillList.Add(newIndex);
        }
    }

    public void DequeAllSlotToggle()
    {
        for (int i = 0; i < ToggledSkillList.Count; i++)
        {
            ImageBlink(ToggledSkillList[i]);
        }
        ToggledSkillList.Clear();
    }

    public void ImageBlink(int index)
    {
        toggleImages[index].ToggleOn();
    }

    private void Update()
    {
        /// 스킬 업데이트 풀어줘야함!
        UpdateSprite();
    }


    public void DropItemSkillInSlot(int skillCode)
    {
        int skillIndex = FindSkillIndex(skillCode);
        if(skillIndex != -1)
        {
            usingSkillDB[skillIndex] = -1;
            usingIngameSkillUI[skillIndex].SetupSkillData(new SkillData());
        }
    }

    private int FindSkillIndex(int skillCode)
    {
        for(int i = 0; i < 8; i++)
        {
            if(unusedSkillDB[usingSkillDB[i]].skillCode == skillCode)
            {
                return i;
            }
        }
        return -1;
    }
    private int GetEmptySlot()
    {
        for(int i = 0; i < 8; i++)
        {
            if(usingSkillDB[i] == -1)
            {
                return i;
            }
        }
        return -1;
    }


    public bool CheckSkillAvailable(int skillCode)
    {
        for(int i = 0; i < skillDBCount; i++)
        {
            if(unusedSkillDB[i].skillCode == skillCode)
            {
                return true;
            }
        }
        return false;
    }


    private void UpdateSprite()
    {
        UpdateUnUsedSlot();
        UpdateUsingSlot();

        // 위와 아래로 가는 스프라이트 안눌리는거 표시하는거 넣어야 함.
        UpdateButtons();


    }

    private void UpdateUsingSlot()
    {
        string path = "Sprites/Skill/";
        for (int i = 0; i < usingSlotCount; i++)
        {
            if (usingSkillDB[i] != -1)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + unusedSkillDB[usingSkillDB[i]].skillIconCode.ToString());
                usingInvenSlot[i].transform.GetChild(0).gameObject.SetActive(true);
                usingInvenSlot[i].transform.GetChild(0).GetComponent<Image>().sprite = image;
            }
            else
            {
                usingInvenSlot[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


    private void UpdateUnUsedSlot()
    {
        string path = "Sprites/Skill/";
        for (int i = 0; i < unusedSlotCount; i++)
        {
            if(unusedSkillDB[i].skillCode != 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + unusedSkillDB[i].skillIconCode.ToString());
                unusedSlot[i].transform.GetChild(5).gameObject.SetActive(false);
                unusedSlot[i].transform.GetChild(4).gameObject.SetActive(true);
                unusedSlot[i].transform.GetChild(4).GetComponent<Image>().sprite = image;
                unusedSlot[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    SkillDataBase.instance.GetSkillData(unusedSkillDB[i].skillCode).skillName.ToString();
                unusedSlot[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    SkillDataBase.instance.GetSkillData(unusedSkillDB[i].skillCode).skillLore.ToString();
            }
            else
            {
                unusedSlot[i].transform.GetChild(5).gameObject.SetActive(true);
                unusedSlot[i].transform.GetChild(4).gameObject.SetActive(false);
                unusedSlot[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                unusedSlot[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }

    }

    private void UpdateButtons()
    {
        for(int i = 0; i < unusedSlotCount; i++)
        {
            // 스킬코드중 모든 애들을 +로 뜨게 만들고...
            if(unusedSkillDB[i].skillCode != 0)
            {
                unusedSlot[i].transform.GetChild(2).gameObject.SetActive(true);
                unusedSlot[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                unusedSlot[i].transform.GetChild(1).gameObject.SetActive(false);
                unusedSlot[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < usingSlotCount; i++)
        {
            // 이러면 사용된거니까 -가 뜨게 만들기.
            if(usingSkillDB[i] != -1)
            {
                unusedSlot[usingSkillDB[i]].transform.GetChild(2).gameObject.SetActive(false);
                unusedSlot[usingSkillDB[i]].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public SkillData GetRealSkillFromIndex(int index)
    {
        return unusedSkillDB[index + visualSlotYIndex * 8];
    }

    public void GetSkill(int skillCode)
    {
        if(!CheckSkillAvailable(skillCode))
        {
            unusedSkillDB[lastSkillAvailableIndex] = SkillDataBase.instance.GetSkillData(skillCode);
            EnqueueToggle(skillCode);
            lastSkillAvailableIndex++;
        }
    }

    

}
