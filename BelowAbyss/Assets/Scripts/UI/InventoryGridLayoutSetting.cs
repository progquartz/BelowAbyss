using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridLayoutSetting : MonoBehaviour
{
    private GridLayoutGroup glg;
    private float lastWidth;
    private float lastHeight;
    private float cellAspectRatio;

    private float cellPhoneWidth;
    private float cellPhoneHeight;

    void Start()
    {
       
        glg = GetComponent<GridLayoutGroup>();
        if (glg == null)
        {
            Debug.LogError("UIFlexibleGridController couldn't find a RectTransform or a GridLayoutGroup");
            return;
        }
        lastWidth = glg.cellSize.x;
        lastHeight = glg.cellSize.y;
        cellAspectRatio = lastWidth / lastHeight;
        cellPhoneHeight = Screen.height;
        cellPhoneWidth = Screen.width;

        glg.cellSize = new Vector2(cellPhoneWidth, lastHeight * Screen.width / lastWidth);

    }

}

