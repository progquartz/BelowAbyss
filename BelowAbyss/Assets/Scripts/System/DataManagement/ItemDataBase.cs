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
    public int FindItemRecipies(int item1, int item2)
    {
        for(int i = 0; i < recipeDatas.Length; i++) // 복잡도 이슈 있을수도 있음...
        {
            if((recipeDatas[i].craftingRecipe[0] == item1 && recipeDatas[i].craftingRecipe[1] == item2) ||
               (recipeDatas[i].craftingRecipe[1] == item2 && recipeDatas[i].craftingRecipe[0] == item1))
            {
                return recipeDatas[i].craftingItem;
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

[System.Serializable]
public class WeaponItems
{
    public WeaponItemData[] weaponItems;

    public WeaponItemData Finditem(int itemCode)
    {
        for (int i = 0; i < weaponItems.Length; i++)
        {
            if (weaponItems[i].itemCode == itemCode)
            {
                return weaponItems[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class ConsumeItems
{
    public ConsumeItemData[] consumeItems;

    public ConsumeItemData FindItem(int itemCode)
    {
        for(int i = 0; i < consumeItems.Length; i++)
        {
            if(consumeItems[i].itemCode == itemCode)
            {
                return consumeItems[i];
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
    public WeaponItems weaponItemList = new WeaponItems();
    public ConsumeItems consumeItemList = new ConsumeItems();


    public string[] itemNameList = new string[10000];
    public string[] itemCategoryList = new string[10000];
    public string[] itemLoreList = new string[10000];
    public int[] itemStackLimitList = new int[10000];
    

    public ItemType[] codeToItemType = new ItemType[10000];


    /// <summary>
    /// 스트링데이터 읽어오는 부분. 매번 아이템 유형이 추가될때마다 해당 스크립트에 로드하는 함수를 추가해줘야만 함.
    /// </summary>
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

        for(int i = 0; i < weaponItemList.weaponItems.Length; i++)
        {
            int itemcode = weaponItemList.weaponItems[i].itemCode;
            itemNameList[itemcode] = weaponItemList.weaponItems[i].itemName;
            itemLoreList[itemcode] = weaponItemList.weaponItems[i].itemLore;
            itemCategoryList[itemcode] = weaponItemList.weaponItems[i].itemCategory;
            itemStackLimitList[itemcode] = weaponItemList.weaponItems[i].itemStackLimit;
        }

        for(int i  = 0; i < consumeItemList.consumeItems.Length; i++)
        {
            ConsumeItemData itemData = consumeItemList.consumeItems[i];
            itemNameList[itemData.itemCode] = itemData.itemName;
            itemLoreList[itemData.itemCode] = itemData.itemLore;
            itemCategoryList[itemData.itemCode] = itemData.itemCategory;
            itemStackLimitList[itemData.itemCode] = itemData.itemStackLimit;
        }

    }

    public OtherItemData LoadItemData(int itemCode)
    {
        ItemType itemType = GetType(itemCode);
        if(itemType == ItemType.WEAPON)
        {
            return weaponItemList.Finditem(itemCode);
        }
        else if(itemType == ItemType.OTHERS)
        {
            return otherItemList.FindItem(itemCode);
        }
        else if(itemType == ItemType.CONSUMPTION)
        {
            return consumeItemList.FindItem(itemCode);
        }
        else
        {
            return null;
        }
    }

    public int[] GetAdditionalSkillCode(int itemCode)
    {
        if(GetType(itemCode) == ItemType.WEAPON)
        {
            return weaponItemList.Finditem(itemCode).additionalSkills;
        }
        else
        {
            // 아직 완성안됨.
            return new int[1] { 0 };
        }
    }

    
    public ItemType GetType(int itemCode)
    {
        return codeToItemType[itemCode];
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

    public int SearchRecipe(int item1, int item2)
    {
        return recipeDatas.FindItemRecipies(item1, item2);
    }
}
