using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DdayUI : MonoBehaviour
{
    private string gameEndLevel = "GameEndLevel";
    public TMP_Text DdayText;
    private string ddayTextOriginal = "D-Day ";
    private int endingStage = -1;

    private void Update()
    {
        if (DdayText != null)
        {
            int stageNum = MapManager.Instance.GetCurrentStageNum();
            if(endingStage == -1)
            {
                endingStage = PlayerPrefs.GetInt(gameEndLevel, 15);
            }
            int stageLeft = endingStage - stageNum;
            DdayText.text = ddayTextOriginal + stageLeft;
        }
        
    }
}
