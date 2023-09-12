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
    private RectTransform healthBarBackground;
    [SerializeField]
    private Transform healthBarHolder;

    int maxSize = 100;

    private void Start()
    {
        healthBarHolder = transform;
        healthBarBackground = transform.GetChild(0).GetComponent<RectTransform>();
        currentHealthBar = transform.GetChild(1).GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(BattleManager.instance.isBattleStarted)
        {
            currentHealthBar.sizeDelta = new Vector2(((float)enemy.stat.currentHp / (float)enemy.stat.maxHp) * maxSize, 20);
            if (enemy.stat.currentHp <= 0)
            {
                healthBarBackground.gameObject.SetActive(false);
                currentHealthBar.gameObject.SetActive(false);
            }
            else
            {
                healthBarBackground.gameObject.SetActive(true);
                currentHealthBar.gameObject.SetActive(true);
            }
        }
        else
        {
            healthBarBackground.gameObject.SetActive(false);
            healthBarBackground.gameObject.SetActive(false);
        }
    }
}