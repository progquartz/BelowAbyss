using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ItemHolder holder;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            holder.OnClickButton(transform.GetSiblingIndex());
        else if (eventData.button == PointerEventData.InputButton.Right)
            holder.OnRightClickButton(transform.GetSiblingIndex(), this.transform);
    }
}
