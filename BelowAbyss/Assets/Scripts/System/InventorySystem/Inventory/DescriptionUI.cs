using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    public GameObject DescriptionTable;
    public RectTransform transform_cursor;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI loreText;
    public GameObject textQWBoth;
    public GameObject textWOnly;

    // Start is called before the first frame update
    void Start()
    {
        
        DescriptionTable = transform.GetChild(0).gameObject;
        transform_cursor = DescriptionTable.GetComponent<RectTransform>();
        Init_Cursor();
        nameText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        categoryText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        loreText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        textQWBoth = transform.GetChild(1).GetChild(0).gameObject;
        textWOnly = transform.GetChild(1).GetChild(1).gameObject;
    }
    
    public void AppearUsageTable(int index, Transform cursorOn)
    {
        string getter = cursorOn.parent.name;
        int itemcode = 0;
        if (getter == "InventoryContents") // 인벤토리일 경우.
        {
            itemcode = Inventory.instance.itemDB[index].itemcode;
        }
        else if (getter == "LootingContents")
        {
            itemcode = LootingSystem.instance.lootingDB[index].itemcode;
        }

        if (itemcode != 0)
        {
            bool isConsumable = false;
            if (ItemDataBase.instance.GetType(itemcode) == ItemType.CONSUMPTION)
            {
                isConsumable = true;
            }
            
            AppearUsageTable(isConsumable);
        }
    }

    public void AppearUsageTable(bool isConsumable)
    {
        if (isConsumable)
        {
            textQWBoth.SetActive(true);
        }
        else
        {
            textWOnly.SetActive(true);
        }
        

    }

    public void DisappearUsageTable()
    {
        textQWBoth.SetActive(false);
        textWOnly.SetActive(false);
    }
    public void Appear(string name, string category, string lore)
    {
        DescriptionTable.SetActive(true);
        nameText.text = name;
        categoryText.text = category;
        loreText.text = lore;
    }

    public void Disappear()
    {
        nameText.text = "";
        categoryText.text = "";
        loreText.text = "";
        DescriptionTable.SetActive(false);
    }

    public void Appear(int index, Transform cursorOn)
    {
        string getter = cursorOn.parent.name;

        int itemcode = 0;
        if (getter == "InventoryContents") // 인벤토리일 경우.
        {
            itemcode = Inventory.instance.itemDB[index].itemcode;
        }
        else if (getter == "CraftingContents") // 조합대일 경우.
        {
            itemcode = Crafting.instance.craftingDB[index].itemcode;
        }
        else if (getter == "EquipContents")
        {
            itemcode = Inventory.instance.equipDB[index].itemcode;
        }
        else if (getter == "CraftingFinishedContents") // 아이템일 경우.
        {
            itemcode = Crafting.instance.craftedDB.itemcode;
        }
        else if (getter == "LootingContents")
        {
            itemcode = LootingSystem.instance.lootingDB[index].itemcode;

        }

        if (itemcode != 0)
        {
            Appear(ItemDataBase.instance.LoadName(itemcode), ItemDataBase.instance.LoadCategory(itemcode), ItemDataBase.instance.LoadLore(itemcode));
        }
        
    }

    private void Update()
    {
        Update_MousePosition();
    }

    private void Init_Cursor()
    {
        Cursor.visible = false;
        transform_cursor.pivot = Vector2.up;

        if (transform_cursor.GetComponent<Graphic>())
            transform_cursor.GetComponent<Graphic>().raycastTarget = false;
    }

    //CodeFinder 코드파인더
    //From https://codefinder.janndk.com/ 
    private void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        transform_cursor.position = mousePos;
        //transform_icon.position = transform_cursor.position;
    }

}
