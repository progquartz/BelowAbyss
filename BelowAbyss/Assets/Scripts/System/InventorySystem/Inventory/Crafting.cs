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
    public int slotCount = 9;

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
            slots.Add(transform.GetChild(0).GetChild(i).gameObject);
        }
        craftedDB = new Item(0,0);
        craftedSlot = transform.GetChild(1).GetChild(0).gameObject;

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
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = image;
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(craftingDB[i].stack.ToString());
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
            }
        }

        if (craftedDB.itemcode != 0)
        {
            Sprite image;
            image = Resources.Load<Sprite>(path + craftedDB.itemcode.ToString());
            craftedSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = image;
            craftedSlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            //craftedSlot.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(craftedDB.stack.ToString());
        }
        else
        {
            craftedSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            craftedSlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            //craftedSlot.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
        }

    }


    public void CraftOneItem()
    {
        if(currentCraftableItem != 0)
        {
            for (int i = 0; i < slotCount; i++)
            {
                if(craftingDB[i].itemcode != 0)
                {
                    MinusStackIndex(i);
                }
            }
        }
        
    }

    public bool MinusStackIndex(int i)
    {
        print("빼짐!");
        if(craftingDB[i].stack > 0)
        {
            craftingDB[i].stack--;
            if (craftingDB[i].stack == 0)
            {
                craftingDB[i].itemcode = 0;
                craftingDB[i].stacklimit = 0;
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
    /// <param name="sizeX"></param>
    /// <param name="sizeY"></param>
    /// <param name="crafting"></param>
    private void CheckCraftingRecipeAvailable(int sizeX, int sizeY, List<int> crafting)
    {
        int[] table = crafting.ToArray();
        tempTable = crafting.ToArray();

        int itemcode = ItemDataBase.instance.SearchRecipe(sizeX, sizeY, table);
        if(itemcode != -1)
        {
            print("예수!");
            currentCraftableItem = itemcode;
            craftedDB.itemcode = itemcode;
        }
        else
        {
            currentCraftableItem = 0;
            craftedDB.itemcode = 0;
        }
    }

    

    /// <summary>
    /// 조합창에 아이템이 올라왔는지, 올라왔다면 그 크기가 어떻게 되는지. 만약 아이템이 올라왔다면 조합을 시작함.
    /// </summary>
    /// <returns></returns>
    private void CheckCrafting()
    {
        int minX = 3;
        int minY = 3;
        int maxX = -1;
        int maxY = -1;

        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                if(craftingDB[y*3 + x].itemcode != 0)
                {
                    if (x > maxX)
                        maxX = x;
                    if (x < minX)
                        minX = x;
                    if (y > maxY)
                        maxY = y;
                    if (y < minY)
                        minY = y;
                }
            }
        }

        if(minX != 3)
        {
            List<int> craftingTable = new List<int>();
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    craftingTable.Add(craftingDB[y * 3 + x].itemcode);
                }
            }
            CheckCraftingRecipeAvailable(maxX - minX + 1 , maxY - minY + 1 , craftingTable);
        }
        else
        {
            currentCraftableItem = 0;
            craftedDB.stack = 0;
            craftedDB.itemcode = 0;
        }
    }



}
