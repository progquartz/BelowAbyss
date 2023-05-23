using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTouchHotKeyIcon : MonoBehaviour
{
    // 버튼 일반 클릭
    public void ButtonClick()
    {
        Inventory.instance.PressHotIcon(transform.parent.GetSiblingIndex());
    }

    private float clickTime; // 클릭 중인 시간
    public float minClickTime = 1; // 최소 클릭시간
    private bool isClick; // 클릭 중인지 판단 

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        isClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        if (clickTime < minClickTime && isClick == true)
        {
            ButtonClick();
        }
        isClick = false;
    }

    private void Update()
    {
        // 클릭 중이라면
        if (isClick)
        {
            // 클릭시간 측정
            clickTime += Time.deltaTime;
            if (clickTime >= minClickTime)
            {
                Inventory.instance.LongPressHotIcon(transform.parent.GetSiblingIndex());
                isClick = false;
            }
        }
        // 클릭 중이 아니라면
        else
        {
            // 클릭시간 초기화
            clickTime = 0;
        }
    }
}
