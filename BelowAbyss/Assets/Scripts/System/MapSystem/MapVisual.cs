using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapVisual : MonoBehaviour
{

    public MapData mapdata;

    
    public GameObject mapFront;
    public GameObject mapBack;
    public RectTransform mapFrontPos;
    public RectTransform mapBackPos;

    public bool moveTrigger = false;
    public bool isMoving = false;

    private float min_PosX = -1100;
    private float max_PosX = 1100;
    public int step = 7;
    private float per_step;
    public float beforeMarker;
    public float afterMarker;


    private void Start()
    {
        mapFrontPos = mapFront.GetComponent<RectTransform>();
        mapBackPos = mapBack.GetComponent<RectTransform>();

        per_step = Mathf.Abs((max_PosX - min_PosX) / step);
        // 점점 줄어들어가게 됨. 맵은 뒤로 움직여야 플레이어는 앞으로 움직임.
        beforeMarker = max_PosX;
        afterMarker = beforeMarker - per_step;
    }
    public void MoveFront()
    {
        moveTrigger = true;
    }
    
    private void Update()
    {
        if(moveTrigger)
        {
            print("아이구야");
            if(mapFrontPos.localPosition.x > afterMarker)
            {
                isMoving = true;
                mapFrontPos.localPosition = new Vector3(mapFrontPos.localPosition.x - (per_step * 0.5f * Time.deltaTime) , mapFrontPos.localPosition.y, 0);
                mapBackPos.localPosition = new Vector3(mapBackPos.localPosition.x - (per_step * 0.5f * Time.deltaTime), mapBackPos.localPosition.y, 0);
            }
            else
            {
                isMoving = false;
                moveTrigger = false;
                beforeMarker = afterMarker;
                afterMarker = beforeMarker - per_step;
                if(afterMarker < min_PosX)
                {
                    afterMarker = min_PosX;
                }
            }
        }
        
    }
}
