using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillInventorySlot : MonoBehaviour
{
    [SerializeField]
    private SkillUIHolder holder;
    [SerializeField]
    private bool isCursorEntered = false;
    [SerializeField]
    private bool isUIOpened = false;
    [SerializeField]
    private float cursorOnTime = 0.0f;
    [SerializeField]
    private GameObject descriptionUI;

    public void OnClick()
    {
        holder.OnClickButton(transform.GetSiblingIndex());
    }

    public void CursorEnter()
    {
        isCursorEntered = true;

    }

    public void CursorExit()
    {
        isUIOpened = false;
        isCursorEntered = false;
        cursorOnTime = 0.0f;
        descriptionUI.SetActive(false);
    }

    private void Update()
    {
        if (isCursorEntered && !isUIOpened)
        {
            cursorOnTime += Time.deltaTime;
            if (cursorOnTime > 1.3f)
            {
                isUIOpened = true;
                //여기에서 불러오면됨.
                descriptionUI.SetActive(true);
            }
        }
    }
}
