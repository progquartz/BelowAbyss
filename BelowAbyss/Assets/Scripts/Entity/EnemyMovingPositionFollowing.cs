using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingPositionFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
    }
}
