using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIHolder : MonoBehaviour
{
    [SerializeField]
    private bool isHoldingSkill;
    [SerializeField]
    private int holdingSkillCode;


    /// <summary>
    /// 아이템 슬롯에 대한 좌클릭을 했을 경우 호출되는 함수.
    /// </summary>
    /// <param name="index"></param>
    public void OnClickButton(int index)
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        Debug.Log(clickObj.name + "을 클릭했습니다.");

        string getter = clickObj.transform.parent.name;

        
        if (!isHoldingSkill)
        {
            if (getter == "UnusedSkillInventorySlots") // 사용안하는 곳이라면.
            {
                if (GetDataFromUnUsed(index))
                {
                    isHoldingSkill = true;
                }
            }
            else if(getter == "UsedSkillInventorySlots")
            {
                if (GetDataFromUsing(index))
                {
                    isHoldingSkill = true;
                    SkillInventory.instance.usingSkillUI[GetSkillSlot(holdingSkillCode)].SetupSkillData(new SkillData());
                    SkillInventory.instance.usingSkillDB[GetSkillSlot(holdingSkillCode)] = new SkillData();

                }
            }
        }
        else // 만약 아이템을 쥐고 있다면.
        {
            if (getter == "UnusedSkillInventorySlots") // 사용안하는 곳이라면.
            {
                SkillData skillChanging = SkillInventory.instance.GetRealSkillFromIndex(index);

                if (skillChanging.skillCode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    isHoldingSkill = false;
                    holdingSkillCode = 0;
                }
                else if (skillChanging.skillCode == holdingSkillCode)
                {
                    // 아무일도 일어나지 않음.
                }
                else
                {
                    // 만약에 다른 스킬이면 들고 있는 스킬이 바뀜.
                    SwapHoldingByInventory(index);
                }
            }
            else if (getter == "UsedSkillInventorySlots") // 사용하는 스킬일 경우.
            {
                SkillData skillChanging = SkillInventory.instance.usingSkillDB[index];

                if (skillChanging.skillCode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    if (CheckWhetherSkillSlotAvailable(holdingSkillCode))
                    {
                        SkillInventory.instance.usingSkillDB[index] = SkillDataBase.instance.FindSkillData(holdingSkillCode);
                        SkillInventory.instance.usingSkillUI[index].SetupSkillData(SkillDataBase.instance.FindSkillData(holdingSkillCode));
                        isHoldingSkill = false;
                        holdingSkillCode = 0;
                    }
                    else
                    {
                        SkillInventory.instance.usingSkillUI[GetSkillSlot(holdingSkillCode)].SetupSkillData(new SkillData());
                        SkillInventory.instance.usingSkillUI[index].SetupSkillData(SkillDataBase.instance.FindSkillData(holdingSkillCode));
                        SkillInventory.instance.usingSkillDB[GetSkillSlot(holdingSkillCode)] = new SkillData();
                        SkillInventory.instance.usingSkillDB[index] = SkillDataBase.instance.FindSkillData(holdingSkillCode);
                        isHoldingSkill = false;
                        holdingSkillCode = 0;
                    }
                }
                else if (!(skillChanging.skillCode == holdingSkillCode))
                {
                    if (CheckWhetherSkillSlotAvailable(holdingSkillCode))
                    {
                        SkillInventory.instance.usingSkillDB[index] = SkillDataBase.instance.FindSkillData(holdingSkillCode);
                        SkillInventory.instance.usingSkillUI[index].SetupSkillData(SkillDataBase.instance.FindSkillData(holdingSkillCode));
                        isHoldingSkill = false;
                        holdingSkillCode = 0;
                    }
                    else
                    {
                        // 같은 스킬이 있다면 사용할 수 없습니다.
                        Debug.Log("같은 스킬은 중첩해서 사용할 수 없습니다.");
                        isHoldingSkill = false;
                        holdingSkillCode = 0;
                    }
                }
            }
        }
    }

    public bool CheckWhetherSkillSlotAvailable(int _skillCode)
    {
        if (_skillCode != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                if(_skillCode == SkillInventory.instance.usingSkillDB[i].skillCode)
                {
                    return false;
                }
                
            }
            return true;
        }
        return false;
    }

    public int GetSkillSlot(int _skillCode)
    {
         for (int i = 0; i < 8; i++)
         {
             if (_skillCode == SkillInventory.instance.usingSkillDB[i].skillCode)
             {
                 return i;
             }
         
         }
         return -1;
    }
    public bool GetDataFromUnUsed(int index)
    {
        SkillData skillGetting = SkillInventory.instance.GetRealSkillFromIndex(index);
        if(skillGetting.skillCode != 0)
        {
            holdingSkillCode = skillGetting.skillCode;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetDataFromUsing(int index)
    {
        SkillData skillGetting = SkillInventory.instance.usingSkillDB[index];
        
        if (skillGetting.skillCode != 0)
        {
            holdingSkillCode = skillGetting.skillCode;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwapHoldingByInventory(int index)
    {
        holdingSkillCode = SkillInventory.instance.GetRealSkillFromIndex(index).skillCode;
    }

    public int GetSkillHolding()
    {
        return holdingSkillCode;
    }

    public bool IsHoldingSkill()
    {
        return isHoldingSkill;
    }


}
