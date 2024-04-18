using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private bool isHoldingItem;
    [SerializeField]
    private int holdingItemCode;
    [SerializeField]
    private int holdingItemStack;
    [SerializeField]
    private int holdingItemStackLimit;
    [SerializeField]
    private Transform equipSlots;



    
    private void Start()
    {
        equipSlots = GameObject.Find("EquipSlots").transform;

    }

    /*[상호작용 알림.]
    
    아무것도 집지 않은 상태에서 아이템이 존재하는 슬롯을 클릭할 경우에 전부가 집어짐.
    아무것도 집지 않은 상태에서 아이템이 존재하는 슬롯을 우클릭할 경우에 절반이 집어짐.

    아이템을 집고 있는 상태에서 바깥에다가 넣고 아이템을 클릭할 경우, 아이템을 버릴것이냐는 질문이 나옴.

    아이템을 집고 있는 상태에서 슬롯에다가 대고 클릭을 할 경우,다음의 경우로 나뉨.
    만약에 같은 아이템 / 부족한 슬롯 수일 경우 아이템이 해당 슬롯에 합쳐짐.
    만약에 같은 아이템 / 더하면 한세트 이상일 경우 아이템이 한세트로 되면서 손에 남은 양이 남음.
    만약에 다른 아이템일 경우, 서로 쥔 아이템이 바뀜.

    아이템을 집고 있는 상태에서 슬롯에다가 대고 우클릭을 할 경우, 다음의 경우로 나뉨.
    만약에 같은 아이템 / 부족한 슬롯 수일 경우, 아이템이 해당 슬롯에 하나가 추가됨.
    만약에 같은 아이템 / 한 세트가 가득 차 있을 경우, 안더해짐.
    만약에 다른 아이템일 경우, 서로 바뀜. 
    */

    /// <summary>
    /// 아이템 슬롯에 대한 좌클릭을 했을 경우 호출되는 함수.
    /// </summary>
    /// <param name="index"></param>
    public void OnClickButton(int index)
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        Debug.Log(clickObj.name + "을 클릭했습니다.");

        string getter = clickObj.transform.parent.name;
        if (!isHoldingItem) // 만약 아이템을 쥐고 있지 않을 경우.
        {
            
            if (getter == "InventoryContents") // 인벤토리일 경우.
            {
                if (GetDataFromInventory(index))
                {
                    isHoldingItem = true;
                }
            }
            else if(getter == "CraftingContents") // 조합대일 경우.
            {
                if (GetDataFromCrafting(index))
                {
                    isHoldingItem = true;
                }
            }
            else if(getter == "CraftingFinishedContents") // 아이템일 경우.
            {
                if (GetOneFromCrafted())
                {
                    isHoldingItem = true;
                    Crafting.instance.CraftItem();
                }

            }
        }
        else // 만약 아이템을 쥐고 있다면.
        {
            if (getter == "InventoryContents") // 인벤토리일 경우.
            {
                Item itemChanging = Inventory.instance.itemDB[index];

                if (itemChanging.itemcode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    itemChanging.itemcode = holdingItemCode;
                    itemChanging.stack = holdingItemStack;
                    itemChanging.stacklimit = holdingItemStackLimit;
                    DropItemHolding();
                }
                else if(itemChanging.itemcode == holdingItemCode)  
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByInventory(index);
                    }
                    else
                    {
                        // 만약에 같은 아이템 / 더하면 한세트 이상일 경우 아이템이 한세트로 되면서 손에 남은 양이 남음.
                        int stackleft = itemChanging.AddStack(holdingItemCode, holdingItemStack);
                        if (stackleft == 0)
                        {
                            DropItemHolding();
                        }
                        else if (stackleft > 0)
                        {
                            holdingItemStack = stackleft;
                        }
                    }
                }
                else 
                {
                    // 만약에 다른 아이템일 경우, 서로 쥔 아이템이 바뀜.
                    SwapHoldingByInventory(index);
                }
            }
            else if (getter == "CraftingContents") // 조합대일 경우.
            {
                Item itemChanging = Crafting.instance.craftingDB[index];

                if (itemChanging.itemcode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    itemChanging.itemcode = holdingItemCode;
                    itemChanging.stack = holdingItemStack;
                    itemChanging.stacklimit = holdingItemStackLimit;
                    DropItemHolding();
                }
                else if (itemChanging.itemcode == holdingItemCode)
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByCrafting(index);
                    }
                    else
                    {
                        // 만약에 같은 아이템 / 더하면 한세트 이상일 경우 아이템이 한세트로 되면서 손에 남은 양이 남음.
                        int stackleft = itemChanging.AddStack(holdingItemCode, holdingItemStack);
                        if (stackleft == 0)
                        {
                            DropItemHolding();
                        }
                        else if (stackleft > 0)
                        {
                            holdingItemStack = stackleft;
                        }
                    }
                }
                else
                {
                    // 만약에 다른 아이템일 경우, 서로 쥔 아이템이 바뀜.
                    SwapHoldingByCrafting(index);
                }
            }
            else if (getter == "CraftingFinishedContents") // 아이템일 경우.
            {
                Item itemChanging = Crafting.instance.craftedDB;
                if (itemChanging.itemcode == holdingItemCode)
                {
                    if(holdingItemStack != itemChanging.stacklimit)
                    {
                        if (GetOneFromCrafted())
                        {
                            Crafting.instance.CraftItem();
                        }
                    }
                }
            }
        }

    }

    public void DropItemHolding()
    {
        holdingItemStack = 0;
        holdingItemCode = 0;
        holdingItemStackLimit = 0;
        isHoldingItem = false;
    }

    /// <summary>
    /// 아이템 슬롯에 대한 우클릭을 할 경우에 호출되는 함수.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="slotTransform"></param>
    public void OnRightClickButton(int index, Transform slotTransform)
    {
        GameObject clickObj = slotTransform.gameObject;

        Debug.Log(clickObj.name +  "을 우클릭함.");
        string getter = clickObj.transform.parent.name;
        if (!isHoldingItem) // 만약 아이템을 쥐고 있지 않을 경우.
        {
            
            if (getter == "InventoryContents") // 인벤토리일 경우.
            {
                if (GetHalfFromInventory(index))
                {
                    isHoldingItem = true;
                }
            }
            else if (getter == "CraftingContents") // 조합대일 경우.
            {
                if (GetHalfFromCrafting(index))
                {
                    isHoldingItem = true;
                }
            }
            else if (getter == "CraftingFinishedContents") // 아이템일 경우.
            {

            }
        }
        else
        {

            if (getter == "InventoryContents") // 인벤토리일 경우.
            {
                // 만약에 같은 아이템 / 부족한 슬롯 수일 경우, 아이템이 해당 슬롯에 하나가 추가됨.
                // 만약에 같은 아이템 / 한 세트가 가득 차 있을 경우, 안더해짐.
                // 만약에 다른 아이템일 경우, 서로 바뀜. 
                Item itemChanging = Inventory.instance.itemDB[index];
                if (itemChanging.itemcode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    itemChanging.itemcode = holdingItemCode;
                    itemChanging.stack = 1;
                    holdingItemStack--;
                    if(holdingItemStack == 0)
                    {
                        holdingItemCode = 0;
                        isHoldingItem = false;
                    }
                }
                else if (itemChanging.itemcode == holdingItemCode) // 같은 아이템일 경우.
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByInventory(index);
                    }
                    else
                    {
                        // 만약에 같은 아이템 / 부족한 슬롯 수일 경우, 아이템이 해당 슬롯에 하나가 추가됨.
                        itemChanging.AddStack(holdingItemCode, 1);
                        holdingItemStack--;
                        if(holdingItemStack == 0)
                        {
                            holdingItemCode = 0;
                            isHoldingItem = false;
                        }
                    }
                }
                else
                {
                    // 만약에 다른 아이템일 경우, 서로 쥔 아이템이 바뀜.
                    SwapHoldingByInventory(index);
                }
            }
            else if (getter == "CraftingContents") // 조합대일 경우.
            {
                // 만약에 같은 아이템 / 부족한 슬롯 수일 경우, 아이템이 해당 슬롯에 하나가 추가됨.
                // 만약에 같은 아이템 / 한 세트가 가득 차 있을 경우, 안더해짐.
                // 만약에 다른 아이템일 경우, 서로 바뀜. 
                Item itemChanging = Crafting.instance.craftingDB[index];
                if (itemChanging.itemcode == 0) // 만약에 해당 슬롯이 빈 경우.
                {
                    itemChanging.itemcode = holdingItemCode;
                    itemChanging.stack = 1;
                    holdingItemStack--;
                    if (holdingItemStack == 0)
                    {
                        holdingItemCode = 0;
                        isHoldingItem = false;
                    }
                }
                else if (itemChanging.itemcode == holdingItemCode) // 같은 아이템일 경우.
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByCrafting(index);
                    }
                    else
                    {
                        // 만약에 같은 아이템 / 부족한 슬롯 수일 경우, 아이템이 해당 슬롯에 하나가 추가됨.
                        itemChanging.AddStack(holdingItemCode, 1);
                        holdingItemStack--;
                        if (holdingItemStack == 0)
                        {
                            holdingItemCode = 0;
                            isHoldingItem = false;
                        }
                    }
                }
                else
                {
                    // 만약에 다른 아이템일 경우, 서로 쥔 아이템이 바뀜.
                    SwapHoldingByCrafting(index);
                }
            }
            else if (getter == "CraftingFinishedContents") // 아이템일 경우.
            {

            }
        }
    }

    /// <summary>
    /// 현재 쥐고 있는 아이템을 인벤토리의 인덱스와 바꿈.
    /// </summary>
    /// <param name="index"></param>
    public void SwapHoldingByInventory(int index)
    {
        int tempItemCode = holdingItemCode;
        int tempItemStack = holdingItemStack;
        int tempItemStackLimit = holdingItemStackLimit;
        holdingItemCode = Inventory.instance.itemDB[index].itemcode;
        holdingItemStack = Inventory.instance.itemDB[index].stack;
        holdingItemStackLimit = Inventory.instance.itemDB[index].stacklimit;
        Inventory.instance.itemDB[index].itemcode = tempItemCode;
        Inventory.instance.itemDB[index].stack = tempItemStack;
        Inventory.instance.itemDB[index].stacklimit = tempItemStackLimit;
    }

    public void SwapHoldingByCrafting(int index)
    {
        int tempItemCode = holdingItemCode;
        int tempItemStack = holdingItemStack;
        int tempItemStackLimit = holdingItemStackLimit;
        holdingItemCode = Crafting.instance.craftingDB[index].itemcode;
        holdingItemStack = Crafting.instance.craftingDB[index].stack;
        holdingItemStackLimit = Crafting.instance.craftingDB[index].stacklimit;
        Crafting.instance.craftingDB[index].itemcode = tempItemCode;
        Crafting.instance.craftingDB[index].stack = tempItemStack;
        Crafting.instance.craftingDB[index].stacklimit = tempItemStackLimit;
    }


    public bool GetHalfFromCrafted()
    {
        Item itemGetting = Crafting.instance.craftedDB;
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack / 2;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.stack -= holdingItemStack;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetDataFromCrafted()
    {
        Item itemGetting = Crafting.instance.craftedDB;
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.itemcode = 0;
            itemGetting.stack = 0;
            itemGetting.stacklimit = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetOneFromCrafted()
    {
        Item itemGetting = Crafting.instance.craftedDB;
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack++;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.stack -= holdingItemStack;
            if(itemGetting.stack == 0)
            {
                itemGetting.itemcode = 0;
                itemGetting.stacklimit = 0;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetHalfFromCrafting(int index)
    {
        Item itemGetting = Crafting.instance.craftingDB[index];
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack / 2;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.stack -= holdingItemStack;
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool GetDataFromCrafting(int index)
    {
        Item itemGetting = Crafting.instance.craftingDB[index];
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.itemcode = 0;
            itemGetting.stack = 0;
            itemGetting.stacklimit = 0;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 인벤토리에서 해당 인덱스의 아이템 데이터들을 가지고 오는 함수. 정상적으로 가져올 경우  true를, 아닐 경우 false값을 출력한다.
    /// </summary>
    /// <returns></returns>
    public bool GetDataFromInventory(int index)
    {
        Item itemGetting = Inventory.instance.itemDB[index];
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack;
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.itemcode = 0;
            itemGetting.stack = 0;
            itemGetting.stacklimit = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHalfFromInventory(int index)
    {
        Item itemGetting = Inventory.instance.itemDB[index];
        if (itemGetting.itemcode != 0)
        {
            holdingItemCode = itemGetting.itemcode;
            holdingItemStack = itemGetting.stack / 2;
            if(holdingItemStack == 0)
            {
                return false;
            }
            holdingItemStackLimit = itemGetting.stacklimit;
            itemGetting.stack -= holdingItemStack;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetItemHolding()
    {
        if (isHoldingItem)
        {
            return holdingItemCode;
        }
        else
        {
            return -1;
        }

    }

    public int GetStackHolding()
    {
        return holdingItemStack;
    }
}
