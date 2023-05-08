using System.Collections;
using System.Collections.Generic;
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
    public List<SkillData> usingSkillDB; // 사용 스킬 인벤 데이터.
    public List<SkillSlot> usingSkillUI;

    public int unusedSlotCount = 32;
    public int usingSlotCount = 8;

    public int skillDBCount = 80;
    public int lastSkillAvailableIndex = 0;
    public int visualSlotYIndex = 0;

    [SerializeField]
    private List<GameObject> unusedSlot;
    [SerializeField]
    private List<GameObject> usingSlot;

    [SerializeField]
    private GameObject upButton;
    [SerializeField]
    private GameObject downButton;

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
    /// <summary>
    ///  이 함수는 GameManager에서 실행되고 있음.
    /// </summary>
    public void FirstSetup()
    {
        unusedSkillDB = new List<SkillData>();
        usingSkillUI = new List<SkillSlot>();

        GameObject skillSlotUI = GameObject.Find("MainUI").transform.GetChild(0).gameObject;
        for (int i = 0; i < unusedSlotCount; i++)
        {
            unusedSlot.Add(transform.GetChild(0).GetChild(5).GetChild(1).GetChild(i).gameObject);
        }
        for(int i = 0; i < usingSlotCount; i++)
        {
            usingSlot.Add(transform.GetChild(0).GetChild(5).GetChild(0).GetChild(i).gameObject);
            usingSkillUI.Add(skillSlotUI.transform.GetChild(i).GetComponent<SkillSlot>());
            usingSkillDB.Add(new SkillData());
        }
        for(int i = 0; i < skillDBCount; i++)
        {
            unusedSkillDB.Add(new SkillData());
        }

        

    }

    private void Update()
    {
        /// 스킬 업데이트 풀어줘야함!
        UpdateSprite();
    }

    public void PutItemSkillInSlot(int skillCode)
    {
        int slotsToPut;

        slotsToPut = GetEmptySlot();
        if(slotsToPut != -1)
        {
            usingSkillDB[slotsToPut] = SkillDataBase.instance.GetSkillData(skillCode);
            usingSkillUI[slotsToPut].SetupSkillData(SkillDataBase.instance.GetSkillData(skillCode));
        }
        else
        {
            slotsToPut = GetNoneItemSkillSlot();
            if(slotsToPut != -1)
            {
                usingSkillDB[slotsToPut] =  SkillDataBase.instance.GetSkillData(skillCode);
                usingSkillUI[slotsToPut].SetupSkillData(SkillDataBase.instance.GetSkillData(skillCode));
            }
            else
            {
                // 모든 슬롯이 아이템 전용 슬롯으로 가득 차 있다.
                Debug.Log("모든 스킬 슬롯이 아이템 스킬 전용 슬롯으로 되어 설정이 불가능합니다!");
            }
        }
    }

    public void DropItemSkillInSlot(int skillCode)
    {
        int skillIndex = FindSkillIndex(skillCode);
        if(skillIndex != -1)
        {
            usingSkillDB[skillIndex] = new SkillData();
            usingSkillUI[skillIndex].SetupSkillData(new SkillData());
        }
    }

    private int FindSkillIndex(int skillCode)
    {
        for(int i = 0; i < 8; i++)
        {
            if(usingSkillDB[i].skillCode == skillCode)
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
            if(usingSkillDB[i].skillCode == 0)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetNoneItemSkillSlot()
    {
        for(int i = 0; i < 8; i++)
        {
            if (!usingSkillDB[i].isItemSkill)
            {
                return i;
            }
        }
        return -1;
    }

    private bool CheckSkillAvailable(int skillCode)
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
        // 위와 아래로 가는 스프라이트 안눌리는거 표시하는거 넣어야 함.
        UpdateButtons();

        UpdateUnUsedSlot();
        UpdateUsingSlot();
    }

    private void UpdateUsingSlot()
    {
        string path = "Sprites/Skill/";
        for (int i = 0; i < usingSlotCount; i++)
        {
            if (usingSkillDB[i].skillIconCode > 0 && usingSkillDB[i].skillCode > 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + usingSkillDB[i].skillIconCode.ToString());
                usingSlot[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                usingSlot[i].transform.GetComponent<Image>().sprite = image;
            }
            else
            {
                usingSlot[i].transform.GetComponent<Image>().sprite = null;
                usingSlot[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }

    }

    private void UpdateUnUsedSlot()
    {
        string path = "Sprites/Skill/";
        for (int i = 0; i < unusedSlotCount; i++)
        {
            int visualYIndex = visualSlotYIndex * 8 + i;
            if (unusedSkillDB[visualYIndex].skillIconCode > 0 && unusedSkillDB[visualYIndex].skillCode > 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + unusedSkillDB[visualYIndex].skillIconCode.ToString());
                unusedSlot[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                unusedSlot[i].transform.GetComponent<Image>().sprite = image;
            }
            else
            {
                unusedSlot[i].transform.GetComponent<Image>().sprite = null;
                unusedSlot[i].transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void UpdateButtons()
    {
        if (visualSlotYIndex == 0)
        {
            upButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        else
        {
            upButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (visualSlotYIndex == 6)
        {
            downButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        else
        {
            downButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void PressUpButton()
    {
        if(visualSlotYIndex > 0)
        {
            visualSlotYIndex--;
        }
    }

    public void PressDownButton()
    {
        if(visualSlotYIndex < (skillDBCount / 8) - 4)
        {
            visualSlotYIndex++;
        }
    }

    public SkillData GetRealSkillFromIndex(int index)
    {
        return unusedSkillDB[index + visualSlotYIndex * 8];
    }

    public void LearnSkill(int skillCode)
    {
        if(!CheckSkillAvailable(skillCode))
        {
            unusedSkillDB[lastSkillAvailableIndex] = SkillDataBase.instance.GetSkillData(skillCode);
            lastSkillAvailableIndex++;
        }
    }

    

}
