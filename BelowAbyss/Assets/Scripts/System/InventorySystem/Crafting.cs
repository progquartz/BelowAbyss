using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public static Crafting instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<Item> craftingDB;
    public int slotCount = 9;

    public List<GameObject> slots;

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
    }

    private void Update()
    {
        UpdateSprite();
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
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(craftingDB[i].stack.ToString());
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
            }
        }
    }



}
