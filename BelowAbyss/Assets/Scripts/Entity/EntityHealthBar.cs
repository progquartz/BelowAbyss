using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthBar : MonoBehaviour
{
    GameObject target;
    RectTransform currentHealthBar;

    private void Start()
    {
        currentHealthBar = transform.GetChild(1).GetComponent<RectTransform>();
    }

    
}
