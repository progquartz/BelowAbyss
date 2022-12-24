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


    // ��Ŭ������ ����.
    // �ƹ��͵� ���� ���� ���¿��� �������� �����ϴ� ������ Ŭ���� ��쿡 ���ΰ� ������.
    // �ƹ��͵� ���� ���� ���¿��� �������� �����ϴ� ������ ��Ŭ���� ��쿡 ������ ������.

    // �������� ���� �ִ� ���¿��� �ٱ����ٰ� �ְ� �������� Ŭ���� ���, �������� �������̳Ĵ� ������ ����.

    // �������� ���� �ִ� ���¿��� ���Կ��ٰ� ��� Ŭ���� �� ���,������ ���� ����.
    // ���࿡ ���� ������ / ������ ���� ���� ��� �������� �ش� ���Կ� ������.
    // ���࿡ ���� ������ / ���ϸ� �Ѽ�Ʈ �̻��� ��� �������� �Ѽ�Ʈ�� �Ǹ鼭 �տ� ���� ���� ����.
    // ���࿡ �ٸ� �������� ���, ���� �� �������� �ٲ�.

    // �������� ���� �ִ� ���¿��� ���Կ��ٰ� ��� ��Ŭ���� �� ���, ������ ���� ����.
    // ���࿡ ���� ������ / ������ ���� ���� ���, �������� �ش� ���Կ� �ϳ��� �߰���.
    // ���࿡ ���� ������ / �� ��Ʈ�� ���� �� ���� ���, �ȴ�����.
    // ���࿡ �ٸ� �������� ���, ���� �ٲ�. 

    /// <summary>
    /// ������ ���Կ� ���� ��Ŭ���� ���� ��� ȣ��Ǵ� �Լ�.
    /// </summary>
    /// <param name="index"></param>
    public void OnClickButton(int index)
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;

        Debug.Log(clickObj.name + "�� Ŭ���߽��ϴ�.");

        string getter = clickObj.transform.parent.name;
        if (!isHoldingItem) // ���� �������� ��� ���� ���� ���.
        {
            
            if (getter == "InventoryContents") // �κ��丮�� ���.
            {
                if (GetDataFromInventory(index))
                {
                    isHoldingItem = true;
                }
            }
            else if(getter == "CraftingContents") // ���մ��� ���.
            {
                if (GetDataFromCrafting(index))
                {
                    isHoldingItem = true;
                }
            }
            else if(getter == "CraftingFinishedContents") // �������� ���.
            {

            }
        }
        else // ���� �������� ��� �ִٸ�.
        {
            if (getter == "InventoryContents") // �κ��丮�� ���.
            {
                Item itemChanging = Inventory.instance.itemDB[index];

                if (itemChanging.itemcode == 0) // ���࿡ �ش� ������ �� ���.
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
                        // ���࿡ ���� ������ / ���ϸ� �Ѽ�Ʈ �̻��� ��� �������� �Ѽ�Ʈ�� �Ǹ鼭 �տ� ���� ���� ����.
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
                    // ���࿡ �ٸ� �������� ���, ���� �� �������� �ٲ�.
                    SwapHoldingByInventory(index);
                }
            }
            else if (getter == "CraftingContents") // ���մ��� ���.
            {
                Item itemChanging = Crafting.instance.craftingDB[index];

                if (itemChanging.itemcode == 0) // ���࿡ �ش� ������ �� ���.
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
                        // ���࿡ ���� ������ / ���ϸ� �Ѽ�Ʈ �̻��� ��� �������� �Ѽ�Ʈ�� �Ǹ鼭 �տ� ���� ���� ����.
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
                    // ���࿡ �ٸ� �������� ���, ���� �� �������� �ٲ�.
                    SwapHoldingByCrafting(index);
                }
            }
            else if (getter == "CraftingFinishedContents") // �������� ���.
            {

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
    /// ������ ���Կ� ���� ��Ŭ���� �� ��쿡 ȣ��Ǵ� �Լ�.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="slotTransform"></param>
    public void OnRightClickButton(int index, Transform slotTransform)
    {
        GameObject clickObj = slotTransform.gameObject;

        Debug.Log(clickObj.name +  "�� ��Ŭ����.");
        string getter = clickObj.transform.parent.name;
        if (!isHoldingItem) // ���� �������� ��� ���� ���� ���.
        {
            
            if (getter == "InventoryContents") // �κ��丮�� ���.
            {
                if (GetHalfFromInventory(index))
                {
                    isHoldingItem = true;
                }
            }
            else if (getter == "CraftingContents") // ���մ��� ���.
            {
                if (GetHalfFromCrafting(index))
                {
                    isHoldingItem = true;
                }
            }
            else if (getter == "CraftingFinishedContents") // �������� ���.
            {

            }
        }
        else
        {

            if (getter == "InventoryContents") // �κ��丮�� ���.
            {
                // ���࿡ ���� ������ / ������ ���� ���� ���, �������� �ش� ���Կ� �ϳ��� �߰���.
                // ���࿡ ���� ������ / �� ��Ʈ�� ���� �� ���� ���, �ȴ�����.
                // ���࿡ �ٸ� �������� ���, ���� �ٲ�. 
                Item itemChanging = Inventory.instance.itemDB[index];
                if (itemChanging.itemcode == 0) // ���࿡ �ش� ������ �� ���.
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
                else if (itemChanging.itemcode == holdingItemCode) // ���� �������� ���.
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByInventory(index);
                    }
                    else
                    {
                        // ���࿡ ���� ������ / ������ ���� ���� ���, �������� �ش� ���Կ� �ϳ��� �߰���.
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
                    // ���࿡ �ٸ� �������� ���, ���� �� �������� �ٲ�.
                    SwapHoldingByInventory(index);
                }
            }
            else if (getter == "CraftingContents") // ���մ��� ���.
            {
                // ���࿡ ���� ������ / ������ ���� ���� ���, �������� �ش� ���Կ� �ϳ��� �߰���.
                // ���࿡ ���� ������ / �� ��Ʈ�� ���� �� ���� ���, �ȴ�����.
                // ���࿡ �ٸ� �������� ���, ���� �ٲ�. 
                Item itemChanging = Crafting.instance.craftingDB[index];
                if (itemChanging.itemcode == 0) // ���࿡ �ش� ������ �� ���.
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
                else if (itemChanging.itemcode == holdingItemCode) // ���� �������� ���.
                {
                    if (itemChanging.stack == itemChanging.stacklimit || holdingItemStack == itemChanging.stacklimit)
                    {
                        SwapHoldingByCrafting(index);
                    }
                    else
                    {
                        // ���࿡ ���� ������ / ������ ���� ���� ���, �������� �ش� ���Կ� �ϳ��� �߰���.
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
                    // ���࿡ �ٸ� �������� ���, ���� �� �������� �ٲ�.
                    SwapHoldingByCrafting(index);
                }
            }
            else if (getter == "CraftingFinishedContents") // �������� ���.
            {

            }
        }
    }

    /// <summary>
    /// ���� ��� �ִ� �������� �κ��丮�� �ε����� �ٲ�.
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
    /// �κ��丮���� �ش� �ε����� ������ �����͵��� ������ ���� �Լ�. ���������� ������ ���  true��, �ƴ� ��� false���� ����Ѵ�.
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
