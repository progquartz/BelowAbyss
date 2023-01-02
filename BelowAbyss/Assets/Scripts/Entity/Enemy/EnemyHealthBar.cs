using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public SpriteRenderer target;
    public Enemy enemy;
    [SerializeField]
    private RectTransform currentHealthBar;
    [SerializeField]
    private Transform healthBarHolder;

    int maxSize = 200;

    private void Start()
    {
        healthBarHolder = transform;
        currentHealthBar = transform.GetChild(1).GetComponent<RectTransform>();
    }

    private void Update()
    {
        currentHealthBar.sizeDelta = new Vector2(((float)enemy.stat.currentHp / (float)enemy.stat.maxHp) * maxSize, 20);
        healthBarHolder.position = Camera.main.WorldToScreenPoint(target.transform.position + new Vector3(-100, 200, 0));
    }
}