using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OtherItems
{
    public OtherItemData[] otherItems;

    public OtherItemData FindItem(int itemCode)
    {
        for (int i = 0; i < otherItems.Length; i++)
        {
            if (otherItems[i].itemCode == itemCode)
            {
                return otherItems[i];
            }
        }
        return null;
    }
}


public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public OtherItems otherItemList = new OtherItems();

    public string[] itemNameList = new string[10000];
    public string[] itemCategoryList = new string[10000];
    public string[] itemLoreList = new string[10000];
    public int[] itemStackLimitList = new int[10000];
    

    public ItemType[] codeToItemType = new ItemType[10000];


    public void LoadStableStringData()
    {

        for(int i = 0; i < otherItemList.otherItems.Length; i++)
        {
            int itemcode = otherItemList.otherItems[i].itemCode;
            itemNameList[itemcode] = otherItemList.otherItems[i].itemName;
            itemLoreList[itemcode] = otherItemList.otherItems[i].itemLore;
            itemCategoryList[itemcode] = otherItemList.otherItems[i].itemCategory;
            itemStackLimitList[itemcode] = otherItemList.otherItems[i].itemStackLimit;
        }

    }

    public string LoadLore(int itemCode)
    {
        return itemLoreList[itemCode];
    }

    public string LoadCategory(int itemCode)
    {
        return itemCategoryList[itemCode];
    }

    public string LoadName(int itemCode)
    {
        return itemNameList[itemCode];
    }

    public int LoadStackLimit(int itemCode)
    {
        return itemStackLimitList[itemCode];
    }
}
