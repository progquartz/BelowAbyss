using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemcode;
    public int stack;
    public int stacklimit = 10; // ���� ���� ���߿� �������ļ����� �޾ƿͼ� ������ ��ī�̺꿡 ����� ������ ������.

    public Item(int _itemcode, int _stack)
    {
        itemcode = _itemcode;
        stack = _stack;
    }

    /// <summary>
    /// �ش� ���Կ� �������� �������, �ٸ� �������̸� -1�� �����ϸ� ���� ������������ �� �� ���̴� ��Ȳ�̶�� ���״¸�ŭ�� ���� ����.
    /// �ƹ��� ������ �����ٸ� 0�� ������.
    /// </summary>
    /// <param name="_itemcode"></param>
    /// <param name="_stack"></param>
    /// <returns></returns>
    public int AddStack(int _itemcode, int _stack)
    {
        if(_itemcode != itemcode && itemcode != 0)
        {
            return -1;
        }
        else
        {
            if (_stack + stack <= stacklimit)
            {
                itemcode = _itemcode;
                stack += _stack;
                return 0;
            }
            else
            {
                itemcode = _itemcode;
                int left = (_stack + stack) - stacklimit;
                stack = stacklimit;
                return left;
            }
        }
        // item���� DB�� �־�� �� ����.
    }
}
