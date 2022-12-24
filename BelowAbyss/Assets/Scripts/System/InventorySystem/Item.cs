using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemcode;
    public int stack;
    public int stacklimit; // 다음 수는 나중에 데이터파서에서 받아와서 데이터 아카이브에 저장될 예정인 변수임.

    public Item(int _itemcode, int _stack)
    {
        itemcode = _itemcode;
        stack = _stack;
    }

    /// <summary>
    /// 해당 슬롯에 아이템을 집어넣음, 다른 아이템이면 -1을 리턴하며 같은 아이템이지만 더 못 쌓이는 상황이라면 못쌓는만큼의 수를 리턴.
    /// 아무런 문제가 없었다면 0을 리턴함.
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
            itemcode = _itemcode;
            stacklimit = ItemDataBase.instance.LoadStackLimit(itemcode);

            if (_stack + stack <= stacklimit)
            {
                stack += _stack;
                return 0;
            }
            else
            {
                int left = (_stack + stack) - stacklimit;
                stack = stacklimit;
                return left;
            }
        }
        // item관련 DB가 있어야 할 예정.
    }

    
}
