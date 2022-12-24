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
        }
    }


    /* 마크 UI 작동방식에 대하여.
     마크 일반 ui (ui간 아이템 이동 가능), 조합 기능
     인벤토리 크기는 32개, 스택단위는 아이템마다 정해져있음.
     조합 4x4 였음.

     인벤토리에서 실제로 작용하는것.

    아이템을 누르면 아이템이 들어올려짐.
    아이템이 들어올려진 상태에서는 인벤토리 밖으로 이를 버릴 경우 아이템을 버릴 수 있음.
    아이템이 들여올려진 상태에서는 아이템을 옮길 수 있음.
    아이템이 들여올려진 상태에서 우클릭을 하면 해당 위치에 1개가 떨어짐. 만약 같은 아이템이라면 1개가 늘어남.
    
    아이템을 우클릭하면 해당 위치의 아이템이 절반으로 나뉜 채로 들여올려짐. 1개면 그냥 들여올려짐.

    아이템이 들여올려진 상태에서 클릭/ 우클릭을 놓으면 들여올려진것이 취소됨.
    들여올려진것이 취소 된 상태에서 해당 위치에 같은아이템이 가득 찼거나, 다른 아이템이 놓여져있다면 이를 가장 가까운 곳에 배치함. 
    만약에 가장 가까운 빈 칸이 없다면, 가장 가까운 같은 아이템이 빈 공간에 집어넣음.
    */

    // 획득과 버려짐, 옮김부터 구현하기.
    public List<Item> itemDB;
    public int slotCount = 32;

    public List<GameObject> slots;

    // holdingitem해서 작업하기.

    
    private void Start()
    {
        FirstSetup();
    }

    private void FirstSetup()
    {
        itemDB = new List<Item>();
        for (int i = 0; i < slotCount; i++)
        {
            itemDB.Add(new Item(0, 0));
            slots.Add(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
        }

    }

    public void Test()
    {
        itemDB[0] = new Item(1, 1);
        itemDB[1] = new Item(2, 10);
    }

    public void Test2()
    {
        GetItem(1, 3);
        GetItem(2, 15);
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
            if(itemDB[i].itemcode != 0)
            {
                Sprite image;
                image = Resources.Load<Sprite>(path + itemDB[i].itemcode.ToString());
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = image;
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText(itemDB[i].stack.ToString());
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slots[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().SetText("");
            }
        }
    }

    /// <summary>
    /// 아이템을 획득하는 코드. 만약에 아이템창이 가득 찬 상태라면, 불가능한 false값을 내보냄.
    /// </summary>
    /// <param name="itemcode"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    private bool GetItem(int itemcode, int stack)
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

        //Debug.Log("아이템을 넣는데 같은 아이템을 찾지 못함!");

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

}
