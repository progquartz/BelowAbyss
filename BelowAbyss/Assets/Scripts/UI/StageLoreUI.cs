using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLoreUI : MonoBehaviour
{
    public Text stageLore;
    public Text stageName;
    public Image loreBackground;

    public float appearAlpha = 0.62f;

    public float appearTime;
    public float disappearTime;

    private bool switchOn;
    private bool isAllAppeared;
    public void AppearStageLore(int _stagenum, string _stageName, string _stageLore, float _appearTime, float _disappearTime)
    {
        stageName.text = "Stage" + _stagenum.ToString() + " - " + _stageName;
        stageLore.text = _stageLore;
        appearTime = _appearTime;
        disappearTime = _disappearTime;
        stageLore.gameObject.SetActive(true);
        stageName.gameObject.SetActive(true);
        loreBackground.gameObject.SetActive(true);
        switchOn = true;
        isAllAppeared = false;
        stageName.color = new Color(1, 1, 1, 0);
        stageLore.color = new Color(1, 1, 1, 0);
        loreBackground.color = new Color(0.2f, 0.2f, 0.2f, 0);
    }

    private void Update()
    {
        if (switchOn && !isAllAppeared)
        {
            loreBackground.color = new Color(0.2f, 0.2f, 0.2f, loreBackground.color.a + (appearAlpha * Time.deltaTime / appearTime));
            stageName.color = new Color(1, 1, 1, stageLore.color.a + (Time.deltaTime / appearTime));
            stageLore.color = new Color(1, 1, 1, stageLore.color.a + (Time.deltaTime / appearTime));
            if(stageLore.color.a >= 1)
            {
                isAllAppeared = true;
            }
        }
        else if(switchOn && isAllAppeared)
        {
            loreBackground.color = new Color(0.2f, 0.2f, 0.2f, loreBackground.color.a - (appearAlpha * Time.deltaTime / appearTime));
            stageName.color = new Color(1, 1, 1, stageLore.color.a - (Time.deltaTime / appearTime));
            stageLore.color = new Color(1, 1, 1, stageLore.color.a - (Time.deltaTime / appearTime));
            if (stageLore.color.a <= 0)
            {
                stageLore.gameObject.SetActive(false);
                stageName.gameObject.SetActive(false);
                loreBackground.gameObject.SetActive(false);
                switchOn = false;
            }
        }
    }

}
