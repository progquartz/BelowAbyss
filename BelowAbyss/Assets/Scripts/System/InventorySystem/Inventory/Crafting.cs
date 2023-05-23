using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public static Crafting instance;
    public int[] tempTable;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<Item> craftingDB;
    public List<int> craftingDBItemIndex;
    public int slotCount = 2;

    public Item craftedDB;

    public List<GameObject> slots;
    public GameObject craftedSlot;
    public int currentCraftableItem = 0;


    private void Start()
    {
        FirstSetup();
    }

    private void FirstSetup()
    {
        craftingDB = new List<Item>();
        for (int i = 0; i < slotCount; i++)
        {
            craftingDB.Add(new Item(0, 0));
            craftingDBItemIndex.Add(-1);
            craftingDB[i].stacklimit = 999;
            slots.Add(transform.GetChild(0).GetChild(i).gameObject);
        }
        craftedDB = new Item(0,0);
        craftedSlot = transform.GetChild(0).GetChild(2).gameObject;

    }

    private void Update()
    {
        UpdateSprite();
        CheckCrafting();
    }


    private void UpdateSprite()
    {
        string path = "Sprites/Item/";
        for (int i = 0; i < slotCount; i++)
        {
            if (craftingDB[i].itemcode != 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + craftingDB[i].itemcode.ToString());
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
                slots[i].transform.GetChild(1).gameObject.SetActive(false);
                slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = image;
                slots[i].transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(craftingDB[i].stack.ToString());
            }
            else
            {
                slots[i].transform.GetChild(0).gameObject.SetActive(false);
                slots[i].transform.GetChild(1).gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
            }
        }

        if (craftedDB.itemcode != 0)
        {
            Sprite image;
            image = Resources.Load<Sprite>(path + craftedDB.itemcode.ToString());
            craftedSlot.transform.GetChild(0).gameObject.SetActive(true);
            craftedSlot.transform.GetChild(1).gameObject.SetActive(false);
            craftedSlot.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = image;
            craftedSlot.transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(craftedDB.stack.ToString());
        }
        else
        {
            craftedSlot.transform.GetChild(0).gameObject.SetActive(false);
            craftedSlot.transform.GetChild(1).gameObject.SetActive(true);
            craftedSlot.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = null;
            craftedSlot.transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
        }

    }

    /// <summary>
    /// 인벤토리에서 줄어드는건 crafting에서 처리해야함.
    /// </summary>
    /// <param name="itemCode"></param>
    /// <param name="slot"></param>
    /// <returns></returns>
    public void PutItemInCraftingSlot(int itemCode, int slot)
    {
        craftingDB[slot].itemcode = itemCode;
        craftingDB[slot].stack++;
    }

    public void MinusItemInCraftingSlot(int slot)
    {
        craftingDB[slot].stack--;
        if(craftingDB[slot].stack <= 0)
        {
            craftingDB[slot].itemcode = 0;
        }
    }


    public void CraftItem()
    {
        if(currentCraftableItem != 0)
        {
            MinusStackIndex(0, craftedDB.stack);
            MinusStackIndex(1, craftedDB.stack);
            Inventory.instance.GetItem(currentCraftableItem, craftedDB.stack);
        }
    }

    public bool MinusStackIndex(int index, int stack)
    {
        print("빼짐!");
        if(craftingDB[index].stack > 0)
        {
            craftingDB[index].stack -= stack;
            Inventory.instance.itemDB[craftingDBItemIndex[index]].stack -= stack;
            if (craftingDB[index].stack <= 0)
            {
                craftingDB[index].itemcode = 0;
                craftingDB[index].stack = 0;
            }
            if(Inventory.instance.itemDB[craftingDBItemIndex[index]].stack <= 0)
            {
                Inventory.instance.itemDB[craftingDBItemIndex[index]].stack = 0;
                Inventory.instance.itemDB[craftingDBItemIndex[index]].itemcode = 0;
                craftingDBItemIndex[index] = -1;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 아이템 조합이 가능한지를 확인하는 여부.
    /// </summary>
    private void CheckCraftingRecipeAvailable(int item1 , int item2, int count)
    {
        int itemcode = ItemDataBase.instance.SearchRecipe(item1, item2);
        if(itemcode != -1)
        {
            currentCraftableItem = itemcode;
            craftedDB.itemcode = itemcode;
            craftedDB.stack = count;
        }
        else
        {
            currentCraftableItem = 0;
            craftedDB.itemcode = 0;
            craftedDB.stack = 0;
        }
    }

    

    /// <summary>
    /// 조합창에 아이템이 올라왔는지, 올라왔다면 그 크기가 어떻게 되는지. 만약 아이템이 올라왔다면 조합을 시작함.
    /// </summary>
    /// <returns></returns>
    private void CheckCrafting()
    {
        int item1 = craftingDB[0].itemcode;
        int item2 = craftingDB[1].itemcode;
        int count = craftingDB[0].stack > craftingDB[1].stack ? craftingDB[0].stack : craftingDB[1].stack;
        CheckCraftingRecipeAvailable(item1, item2, count);

    }



}
