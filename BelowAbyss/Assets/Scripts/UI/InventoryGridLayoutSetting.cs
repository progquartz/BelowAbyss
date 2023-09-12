using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridLayoutSetting : MonoBehaviour
{
    private GridLayoutGroup glg;
    [SerializeField]
    private Canvas mainCanvas;
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

        Debug.Log("해상도 " + mainCanvas.pixelRect.width + " * " + mainCanvas.pixelRect.height);
        cellPhoneWidth = mainCanvas.pixelRect.width;
        cellPhoneHeight = mainCanvas.pixelRect.height;
        //cellPhoneHeight = Screen.height;
        //cellPhoneWidth = Screen.width;

        glg.cellSize = new Vector2(cellPhoneWidth, lastHeight * Screen.width / lastWidth);

    }

    private void Update()
    {
        

        
    }

}

