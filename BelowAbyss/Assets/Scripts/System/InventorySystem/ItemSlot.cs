using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ItemHolder holder;
    [SerializeField]
    private bool isCursorEntered = false;
    [SerializeField]
    private bool isUIOpened = false;
    [SerializeField]
    private float cursorOnTime = 0.0f;
    [SerializeField]
    private DescriptionUI descriptionUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            holder.OnClickButton(transform.GetSiblingIndex());
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            holder.OnRightClickButton(transform.GetSiblingIndex(), this.transform);
        }
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
        descriptionUI.Disappear();
    }

    private void Update()
    {
        if(isCursorEntered && !isUIOpened)
        {
            cursorOnTime += Time.deltaTime;
            if(cursorOnTime > 1.3f)
            {
                isUIOpened = true;
                //여기에서 불러오면됨.
                descriptionUI.Appear(transform.GetSiblingIndex(), transform);
            }
        } 
    }

}
