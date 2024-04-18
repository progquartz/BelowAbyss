using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositionFollowing : MonoBehaviour
{
    private Vector3[] originalPos = { new Vector3(-24,413.2f,0), new Vector3(63, 413.2f, 0), new Vector3(150, 413.2f, 0) };
    private Vector3 currentTargetingPos = new Vector3();
    [SerializeField]
    private int originalIndex = 0;
    [SerializeField]
    private int currentAllocatedIndex;


    // Update is called once per frame
    void Update()
    {
        GetCloseToTargetingPos();
    }

    public void GetCloseToTargetingPos()
    {
        currentTargetingPos = originalPos[currentAllocatedIndex];
        this.transform.position = Vector3.Lerp(this.transform.position, currentTargetingPos, 0.15f);
    }

    public void GetBackToOriginalPosition()
    {
        currentAllocatedIndex = originalIndex;
        this.transform.position = originalPos[originalIndex];
    }
    public void AllocateNewPosition(int index)
    {
        currentAllocatedIndex = index;
    }

}
