using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeDatas
{
    public RecipeData[] recipeDatas;

    /// <summary>
    /// 조합법을 찾고, 만약에 찾지 못했을 경우 -1을 리턴.
    /// </summary>
    /// <param name="sizeX"></param>
    /// <param name="sizeY"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public int FindItemRecipies(int sizeX, int sizeY, int[] table)
    {
        for(int i = 0; i < recipeDatas.Length; i++) // 복잡도 이슈 있을수도 있음...
        {
            if(recipeDatas[i].sizeX == sizeX && recipeDatas[i].sizeY == sizeY)
            {
                bool stillavailable = true;
                for(int x = 0; x < table.Length; x++)
                {
                    if(recipeDatas[i].craftingRecipe[x] != table[x])
                    {
                        stillavailable = false;
                    }
                    
                }
                if(stillavailable)
                {
                    return recipeDatas[i].craftingItem;
                }
            }
        }
        return -1;
    }

}
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

    public RecipeDatas recipeDatas = new RecipeDatas();

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

    public int SearchRecipe(int sizeX, int sizeY, int[] table)
    {
        return recipeDatas.FindItemRecipies(sizeX, sizeY, table);
    }
}
