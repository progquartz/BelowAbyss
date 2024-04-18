using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleNewImageSlot : MonoBehaviour
{
    [SerializeField]
    private Image togglingSlot;
    
    // Start is called before the first frame update
    void Start()
    {
        togglingSlot = GetComponent<Image>();
    }


    [SerializeField]
    private bool isToggling = false;
    private float toggleInterval = 0.9f;
    private bool isIncreasing = false;

    public void ToggleOn()
    {
        isToggling = true;
        isIncreasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = togglingSlot.color.a;
        if (isToggling)
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
                isToggling = false;
            }
            togglingSlot.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            isIncreasing = false;
            alpha = 0;
            togglingSlot.color = new Color(1, 1, 1, 0);
        }

    }
}
