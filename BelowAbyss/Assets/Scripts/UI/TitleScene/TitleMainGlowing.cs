using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMainGlowing : MonoBehaviour
{
    [SerializeField]
    private Image glowingImage;
    private float glowingPeriod = 4.5f;
    float alpha = 0.0f;
    bool isAlphaIncreasing = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlphaIncreasing)
        {
            if (alpha < 1.0f)
            {
                alpha += (1.0f * Time.deltaTime / (glowingPeriod / 2));
                glowingImage.color = new Color(1, 1, 1, alpha);
            }
            else
            {
                isAlphaIncreasing = false;
            }
        }
        else
        {
            if (alpha > 0.0f)
            {
                alpha -= (1.0f * Time.deltaTime / (glowingPeriod / 2));
                glowingImage.color = new Color(1, 1, 1, alpha);
            }
            else
            {
                isAlphaIncreasing = true;
            }
        }
        

    }
}
