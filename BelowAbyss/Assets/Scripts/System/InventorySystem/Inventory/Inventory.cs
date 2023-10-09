using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType
{
    OTHERS = 9,
    CONSUMPTION = 3,
    WEAPON = 1,
    ARMOUR = 2,
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

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

    // 획득과 버려짐, 옮김부터 구현하기.
    public List<Item> itemDB;
    public List<int> hotSlotDB;
    public int slotCount = 32;

    public List<GameObject> slots;
    public List<GameObject> hotSlots;
    // holdingitem해서 작업하기.

    [SerializeField]
    private List<Button> plusButtons;
    [SerializeField]
    private List<Button> minusButtons;

    private void Start()
    {
        FirstSetup();
    }

    private void FirstSetup()
    {
        slots = new List<GameObject>();
        itemDB = new List<Item>();
        int a = -1;
        for (int i = 0; i < slotCount; i++)
        {
            itemDB.Add(new Item(0, 0));
            slots.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).gameObject);
            plusButtons.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(4).GetComponent<Button>());
            minusButtons.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(5).GetComponent<Button>());
        }
        for(int i = 0; i < 2; i++)
        {
            hotSlotDB.Add(-1);
        }
    }

    public void Test()
    {
        GetItem(201, 1);
        GetItem(202, 10);
    }

    public void Test2()
    {
        GetItem(101, 3);
        GetItem(102, 2);
        GetItem(103, 1);
    }

    public void Test3()
    {
        GetItem(201, 3);
        GetItem(202, 2);
        GetItem(203, 1);
    }

    private void Update()
    {
        UpdateSprite();
    }

    public void PressHotIcon(int index)
    {
        if(hotSlotDB[index] != -1)
        {
            PressItemIcon(hotSlotDB[index]);
        }
    }

    public void LongPressHotIcon(int index)
    {
        UISoundEffect.instance.EquipUnEquipSound();
        hotSlotDB[index] = -1;
    }

    public void PressItemIcon(int index)
    {
        UISoundEffect.instance.UseItemSound();
        Debug.Log(index + "번째 슬롯의 아이템을 사용합니다.");
        if(itemDB[index].itemcode != 0)
        {
            Debug.Log("통과");
            UseItem(index);
            if (itemDB[index].stack <= 0)
            {
                if (hotSlotDB[0] == index)
                {
                    hotSlotDB[0] = -1;
                }
                if (hotSlotDB[1] == index)
                {
                    hotSlotDB[1] = -1;
                }
            }

        }
    }

    public void LongPressItemIcon(int index)
    {
        if(itemDB[index].itemcode != 0)
        {
            if (hotSlotDB[0] != -1 && hotSlotDB[1] != -1)
            {
                Debug.Log("핫 슬롯 등록할 곳 없음!");
                return;
            }
            else if (hotSlotDB[0] == -1 && hotSlotDB[1] != index)
            {
                hotSlotDB[0] = index;
            }
            else if (hotSlotDB[1] == -1 && hotSlotDB[0] != index)
            {
                hotSlotDB[1] = index;
            }
            else if (hotSlotDB[0] == -1 && hotSlotDB[1] == -1)
            {
                hotSlotDB[0] = index;
            }
            UISoundEffect.instance.EquipUnEquipSound();

        }
    }

    public void HotSlotUnEquip(int itemCode)
    {
        
        if(hotSlotDB[0] != -1 &&  itemDB[hotSlotDB[0]].itemcode == itemCode)
        {
            hotSlotDB[0] = -1;
        }
        if (hotSlotDB[1] != -1 && itemDB[hotSlotDB[1]].itemcode == itemCode)
        {
            hotSlotDB[1] = -1;
        }
    }

    public void PressPlusButton(int buttonIndex)
    {
        Debug.Log(buttonIndex + "번째 +버튼을 눌렀습니다.");

        if (Crafting.instance.craftingDB[0].itemcode == itemDB[buttonIndex].itemcode)
        {
            Crafting.instance.craftingDB[0].stack++;
            return;
        }
        else if (Crafting.instance.craftingDB[1].itemcode == itemDB[buttonIndex].itemcode)
        {
            Crafting.instance.craftingDB[1].stack++;
            return;
        }

        if (Crafting.instance.craftingDB[0].itemcode == 0)
        {
            Crafting.instance.craftingDB[0].itemcode = itemDB[buttonIndex].itemcode;
            Crafting.instance.craftingDBItemIndex[0] = buttonIndex;
            Crafting.instance.craftingDB[0].stack++;
        }
        else if (Crafting.instance.craftingDB[1].itemcode == 0)
        {
            Crafting.instance.craftingDB[1].itemcode = itemDB[buttonIndex].itemcode;
            Crafting.instance.craftingDBItemIndex[1] = buttonIndex;
            Crafting.instance.craftingDB[1].stack++;
        }
        else
        {
            Debug.Log("버튼 눌렀지만 생기지 않음");
        }

    }

    public void PressMinusButton(int buttonIndex)
    {
        Debug.Log(buttonIndex + "번째 -버튼을 눌렀습니다.");

        if (Crafting.instance.craftingDB[0].itemcode == itemDB[buttonIndex].itemcode)
        {
            Crafting.instance.craftingDB[0].stack--;
            if (Crafting.instance.craftingDB[0].stack == 0)
            {
                Crafting.instance.craftingDB[0].itemcode = 0;
                Crafting.instance.craftingDBItemIndex[0] = -1;
            }
            return;
        }
        else if (Crafting.instance.craftingDB[1].itemcode == itemDB[buttonIndex].itemcode)
        {
            Crafting.instance.craftingDB[1].stack--;
            if (Crafting.instance.craftingDB[1].stack == 0)
            {
                Crafting.instance.craftingDB[1].itemcode = 0;
                Crafting.instance.craftingDBItemIndex[1] = -1;
            }
            return;
        }
        else
        {
            Debug.Log("버튼 눌렀지만 생기지 않음");
        }
    }


    private void UpdateSprite()
    {
        string path = "Sprites/Item/"; 
        for (int i = 0; i < slotCount; i++)
        {
            if(itemDB[i].itemcode != 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + itemDB[i].itemcode.ToString());
                slots[i].transform.GetChild(1).gameObject.SetActive(true);
                slots[i].transform.GetChild(2).gameObject.SetActive(false);
                slots[i].transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite = image;
                slots[i].transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(itemDB[i].stack.ToString());
                slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ItemDataBase.instance.LoadItemData(itemDB[i].itemcode).itemName);
                slots[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(ItemDataBase.instance.LoadItemData(itemDB[i].itemcode).itemLore);


                if ((Crafting.instance.craftingDB[0].itemcode == itemDB[i].itemcode) )
                {
                    if(Crafting.instance.craftingDB[0].stack < itemDB[i].stack)
                    {
                        slots[i].transform.GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {
                        slots[i].transform.GetChild(4).gameObject.SetActive(false);
                    }
                    
                }
                else if ((Crafting.instance.craftingDB[1].itemcode == itemDB[i].itemcode))
                {
                    if (Crafting.instance.craftingDB[1].stack < itemDB[i].stack)
                    {
                        slots[i].transform.GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {
                        slots[i].transform.GetChild(4).gameObject.SetActive(false);
                    }
                }
                else if ((Crafting.instance.craftingDB[0].itemcode == 0 || Crafting.instance.craftingDB[1].itemcode == 0))
                {
                    slots[i].transform.GetChild(4).gameObject.SetActive(true);
                }
                else
                {
                    slots[i].transform.GetChild(4).gameObject.SetActive(false);
                }

                if(itemDB[i].itemcode == Crafting.instance.craftingDB[0].itemcode || itemDB[i].itemcode == Crafting.instance.craftingDB[1].itemcode)
                {
                    slots[i].transform.GetChild(5).gameObject.SetActive(true);
                }
                else
                {
                    slots[i].transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else
            {
                slots[i].transform.GetChild(1).gameObject.SetActive(false);
                slots[i].transform.GetChild(2).gameObject.SetActive(true);
                slots[i].transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slots[i].transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
                slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
                slots[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().SetText("");
                slots[i].transform.GetChild(4).gameObject.SetActive(false);
                slots[i].transform.GetChild(5).gameObject.SetActive(false);
            }
        }


        for(int i = 0; i < 2; i++)
        {
            if(hotSlotDB[i] != -1)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + itemDB[hotSlotDB[i]].itemcode.ToString());
                hotSlots[i].transform.GetChild(1).gameObject.SetActive(true);
                hotSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = image;
                hotSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemDB[hotSlotDB[i]].stack.ToString();
            }
            else
            {
                hotSlots[i].transform.GetChild(1).gameObject.SetActive(false);
                hotSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void UseItem(int index)
    {
        int itemcode = 0;
        itemcode = itemDB[index].itemcode;
        Debug.Log("아이템코드는" + itemcode);
        if (ItemDataBase.instance.GetType(itemcode) == ItemType.CONSUMPTION)
        {
            Debug.Log(index + "번째 인덱스의 " + itemcode + "번 아이템 사용 호출");
            ConsumeItemData data = ItemDataBase.instance.LoadItemData(itemcode) as ConsumeItemData;
            for(int i = 0; i < data.itemUseCode1.Length; i++)
            {
                EffectManager.instance.AmplifyEffect(data.itemUseCode1[i], data.itemUseCode2[i], data.itemUseCode3[i]);
            }
            LossItem(index, itemcode);
        }
    }

    public void DropItem(int index, Transform cursorOn)
    {
        string getter = cursorOn.parent.name;

        int itemcode = 0;
        if (getter == "InventoryContents") // 인벤토리일 경우.
        {
            itemcode = Inventory.instance.itemDB[index].itemcode;
        }

        if (itemcode != 0)
        {
            Debug.Log(index + "번 인덱스의 아이템을 버립니다.");
            LossItem(index, itemcode);
        }
    }
    /// <summary>
    /// 아이템을 획득하는 코드. 만약에 아이템창이 가득 찬 상태라면, 불가능한 false값을 내보냄.
    /// </summary>
    /// <param name="itemcode"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    public bool GetItem(int itemcode, int stack)
    {
        // 만약에  sameitemexist상태이지만 item에서 불가능 판정을 내려서 남는 아이템을 넘기면 이를 int값으로 저장.
        // 
        int isNotStackable = stack;

        for (int i = 0; i < slotCount; i++) // 모든 슬롯카운터에 한해.
        {
            if (itemDB[i].itemcode == itemcode) // 해당 슬롯에 아이템이 존재한다면.
            {
                // 만약 슬롯에 데이터가 들어갔는데 쌓이지 못한다면 isnotstackable이 양수가됨. 문제가 없으면 0임.
                isNotStackable = itemDB[i].AddStack(itemcode, isNotStackable);

                if(isNotStackable == 0) // 모든 아이템들이 정상적으로 들어갔다면 return true;
                {
                    return true;
                }
            }
        }

        Debug.Log("아이템을 넣는데 같은 아이템을 찾지 못함!");

        // 여기까지 오면 모든 같은 아이템 슬롯에는 해당 아이템들이 없다는 뜻이 됨.
        for (int emp = 0; emp < slotCount; emp++) // 빈 공간을 찾음.
        {
            if (itemDB[emp].itemcode == 0)
            {
                isNotStackable = itemDB[emp].AddStack(itemcode, isNotStackable);

                if(isNotStackable == 0) // 만약에 다 채워졌다면.
                {
                    Debug.Log(emp + "번의 빈 공간에" + itemcode + "번의 아이템을 넣음.");
                    return true;
                }
            }
        }

        // 여기까지 오면 모든 아이템 슬롯이 비지 않았고 같은 아이템 슬롯에는 해당 아이템들이 가득 찬것임.
        Debug.Log(itemcode + "번의 아이템이" + isNotStackable + "만큼 인벤토리에 저장되지 못함.");
        return false;

       
       
    }

    private void LossItem(int index, int itemcode)
    {
        if(itemDB[index].stack > 0)
        {
            itemDB[index].stack--;
            if (itemDB[index].stack == 0)
            {
                EmptySlot(index);
            }
        }
    }

    private void EmptySlot(int index)
    {
        Item item = new Item(0, 0);
        itemDB[index] = item;
    }

    public void InventoryClickSound()
    {
        UISoundEffect.instance.ButtonClickSound();
    }
}
