using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootingSystem : MonoBehaviour
{
    public static LootingSystem instance;
    private List<GameObject> lootingSlots = new List<GameObject>();
    public List<Item> lootingDB = new List<Item>();
    private int slotCount = 16;

    [SerializeField]
    private GameObject lootingUIObject;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        for(int i = 0; i < 16; i++)
        {
            lootingSlots.Add(lootingUIObject.transform.GetChild(0).GetChild(i).gameObject);
            lootingDB.Add(new Item(0,0));
        }
    }

    public void Test1()
    {
        EventManager.instance.LoadEvent(41);
    }

    public void LootTableOpen(LootingData data)
    {
        if (!lootingUIObject.activeInHierarchy)
        {
            lootingUIObject.SetActive(true);
        }
        SetTableDataOpen(data);
    }

    private void SetTableDataOpen(LootingData data)
    {
        for (int i = 0; i < data.rootingItem.Length; i++)
        {
            SetLootingItem(i, data.rootingItem[i], Random.Range(data.rootingMin[i], data.rootingMax[i]));
        }
    }

    private void SetLootingItem(int index, int itemcode, int count)
    {
        lootingDB[index].itemcode = itemcode;
        lootingDB[index].stack = count;

    }

    public Item GetIndexData(int index)
    {
        return lootingDB[index];
    }

    private void Update()
    {
        string path = "Sprites/Item/";
        for (int i = 0; i < slotCount; i++)
        {
            if (lootingDB[i].itemcode != 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + lootingDB[i].itemcode.ToString());
                lootingSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = image;
                lootingSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                lootingSlots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(lootingDB[i].stack.ToString());
            }
            else
            {
                lootingSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                lootingSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                lootingSlots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
            }
        }
    }
}
