using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatGaugeUI : MonoBehaviour
{
    public Transform[] gaugeUITransform;
    private int MaxMinPos = -228;
    private void Update()
    {
        PlayerStat statData = Player.instance.stat;
        float pos = (1 - (float)statData.currentVitality / (float)statData.maxVitality) * MaxMinPos;
        gaugeUITransform[0].localPosition = new Vector3(0, pos);

        pos = (1 - (float)statData.currentSanity / (float)statData.maxSanity) * MaxMinPos;
        gaugeUITransform[1].localPosition = new Vector3(0, pos);

        pos = (1 - (float)statData.currentSatur / (float)statData.maxSatur) * MaxMinPos;
        gaugeUITransform[2].localPosition = new Vector3(0, pos);

        pos = (1 - (float)statData.currentThirst / (float)statData.maxThirst) * MaxMinPos;
        gaugeUITransform[3].localPosition = new Vector3(0, pos);
    }
}
