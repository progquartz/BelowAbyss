using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositionFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target.activeInHierarchy)
        {
            this.transform.position = target.transform.position;
        }
    }
}
