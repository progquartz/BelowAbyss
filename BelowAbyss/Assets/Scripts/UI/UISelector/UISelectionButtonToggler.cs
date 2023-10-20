using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectionButtonToggler : MonoBehaviour
{
    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private bool isToggling = false;
    private float toggleInterval = 0.7f;
    private bool isIncreasing = false;

    public void ToggleOn()
    {
        isToggling = true;
    }

    public void ToggleOff()
    {
        isToggling = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = buttonImage.color.a;
        if(isToggling)
        {
            if (isIncreasing)
                alpha += Time.deltaTime / toggleInterval;
            else
                alpha -= Time.deltaTime / toggleInterval;

            if (alpha > 1.0f)
            {
                alpha = 1.0f;
                isIncreasing = false;
            }
            else if (alpha < 0)
            {
                alpha = 0;
                isIncreasing = true;
            }
            buttonImage.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            isIncreasing = false;
            alpha = 0;
            buttonImage.color = new Color(1, 1, 1, 0);
        }

    }
}
