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

            }
            else if(getter == "CraftingFinishedContents") // �������� ���.
            {

            }
        }
        else // ���� �������� ��� �ִٸ�.
        {
            if (getter == "InventoryContents") // �κ��丮�� ���.
            {
                Item itemChanging = Inventory.instance.ItemDB[index];

                if (itemChanging.itemcode == 0) // ���࿡ �ش� ������ �� ���.
                {
                    itemChanging.itemcode = holdingItemCode;
                    itemChanging.stack = holdingItemStack;
                    holdingItemCode = 0;
                    holdingItemStack = 0;
                    isHoldingItem = false;
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
                            holdingItemStack = 0;
                            holdingItemCode = 0;
                            isHoldingItem = false;
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

            }
            else if (getter == "CraftingFinishedContents") // �������� ���.
            {

            }
        }

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
                Item itemChanging = Inventory.instance.ItemDB[index];
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
                        int stackleft = itemChanging.AddStack(holdingItemCode, 1);
                        if (stackleft == 0)
                        {
                            holdingItemStack = 0;
                            holdingItemCode = 0;
                            isHoldingItem = false;
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
        holdingItemCode = Inventory.instance.ItemDB[index].itemcode;
        holdingItemStack = Inventory.instance.ItemDB[index].stack;
        Inventory.instance.ItemDB[index].itemcode = tempItemCode;
        Inventory.instance.ItemDB[index].stack = tempItemStack;
    }


    /// <summary>
    /// �κ��丮���� �ش� �ε����� ������ �����͵��� ������ ���� �Լ�. ���������� ������ ���  true��, �ƴ� ��� false���� ����Ѵ�.
    /// </summary>
    /// <returns></returns>
    public bool GetDataFromInventory(int index)
    {
        if (Inventory.instance.ItemDB[index].itemcode != 0)
        {
            holdingItemCode = Inventory.instance.ItemDB[index].itemcode;
            holdingItemStack = Inventory.instance.ItemDB[index].stack;
            Inventory.instance.ItemDB[index].itemcode = 0;
            Inventory.instance.ItemDB[index].stack = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHalfFromInventory(int index)
    {
        if (Inventory.instance.ItemDB[index].itemcode != 0)
        {
            holdingItemCode = Inventory.instance.ItemDB[index].itemcode;
            holdingItemStack = Inventory.instance.ItemDB[index].stack / 2;
            Inventory.instance.ItemDB[index].stack -= holdingItemStack;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TestClickObject()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
