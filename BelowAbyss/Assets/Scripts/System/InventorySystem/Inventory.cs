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


    /* ��ũ UI �۵���Ŀ� ���Ͽ�.
     ��ũ �Ϲ� ui (ui�� ������ �̵� ����), ���� ���
     �κ��丮 ũ��� 32��, ���ô����� �����۸��� ����������.
     ���� 4x4 ����.

     �κ��丮���� ������ �ۿ��ϴ°�.

    �������� ������ �������� ���÷���.
    �������� ���÷��� ���¿����� �κ��丮 ������ �̸� ���� ��� �������� ���� �� ����.
    �������� �鿩�÷��� ���¿����� �������� �ű� �� ����.
    �������� �鿩�÷��� ���¿��� ��Ŭ���� �ϸ� �ش� ��ġ�� 1���� ������. ���� ���� �������̶�� 1���� �þ.
    
    �������� ��Ŭ���ϸ� �ش� ��ġ�� �������� �������� ���� ä�� �鿩�÷���. 1���� �׳� �鿩�÷���.

    �������� �鿩�÷��� ���¿��� Ŭ��/ ��Ŭ���� ������ �鿩�÷������� ��ҵ�.
    �鿩�÷������� ��� �� ���¿��� �ش� ��ġ�� ������������ ���� á�ų�, �ٸ� �������� �������ִٸ� �̸� ���� ����� ���� ��ġ��. 
    ���࿡ ���� ����� �� ĭ�� ���ٸ�, ���� ����� ���� �������� �� ������ �������.
    */

    // ȹ��� ������, �ű���� �����ϱ�.
    public List<Item> itemDB;
    public int slotCount = 32;

    public List<GameObject> slots;

    // holdingitem�ؼ� �۾��ϱ�.

    
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
    /// �������� ȹ���ϴ� �ڵ�. ���࿡ ������â�� ���� �� ���¶��, �Ұ����� false���� ������.
    /// </summary>
    /// <param name="itemcode"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    private bool GetItem(int itemcode, int stack)
    {
        // ���࿡  sameitemexist���������� item���� �Ұ��� ������ ������ ���� �������� �ѱ�� �̸� int������ ����.
        // 
        int isNotStackable = stack;

        for (int i = 0; i < slotCount; i++) // ��� ����ī���Ϳ� ����.
        {
            if (itemDB[i].itemcode == itemcode) // �ش� ���Կ� �������� �����Ѵٸ�.
            {
                // ���� ���Կ� �����Ͱ� ���µ� ������ ���Ѵٸ� isnotstackable�� �������. ������ ������ 0��.
                isNotStackable = itemDB[i].AddStack(itemcode, isNotStackable);

                if(isNotStackable == 0) // ��� �����۵��� ���������� ���ٸ� return true;
                {
                    return true;
                }
            }
        }

        //Debug.Log("�������� �ִµ� ���� �������� ã�� ����!");

        // ������� ���� ��� ���� ������ ���Կ��� �ش� �����۵��� ���ٴ� ���� ��.
        for (int emp = 0; emp < slotCount; emp++) // �� ������ ã��.
        {
            if (itemDB[emp].itemcode == 0)
            {
                isNotStackable = itemDB[emp].AddStack(itemcode, isNotStackable);

                if(isNotStackable == 0) // ���࿡ �� ä�����ٸ�.
                {
                    Debug.Log(emp + "���� �� ������" + itemcode + "���� �������� ����.");
                    return true;
                }
            }
        }

        // ������� ���� ��� ������ ������ ���� �ʾҰ� ���� ������ ���Կ��� �ش� �����۵��� ���� ������.
        Debug.Log(itemcode + "���� ��������" + isNotStackable + "��ŭ �κ��丮�� ������� ����.");
        return false;

       
       
    }

}
