using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public SpriteRenderer target;
    public Enemy enemy;
    [SerializeField]
    private RectTransform canvasRect;
    [SerializeField]
    private RectTransform currentHealthBar;
    [SerializeField]
    private RectTransform healthBarBackground;
    [SerializeField]
    private RectTransform healthBarUpper;
    private Transform healthBarHolder;

    int maxSize = 141;

    private void Start()
    {
        healthBarHolder = transform;
        healthBarBackground = transform.GetChild(0).GetComponent<RectTransform>();
        currentHealthBar = transform.GetChild(1).GetComponent<RectTransform>();
        healthBarUpper = transform.GetChild(2).GetComponent<RectTransform>();
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
                healthBarUpper.gameObject.SetActive(false);
            }
            else
            {
                healthBarBackground.gameObject.SetActive(true);
                currentHealthBar.gameObject.SetActive(true);
                healthBarUpper.gameObject.SetActive(true);
            }
        }
        else
        {
            healthBarBackground.gameObject.SetActive(false);
            currentHealthBar.gameObject.SetActive(false);
            healthBarUpper.gameObject.SetActive(false);
        }
        //this.GetComponent<RectTransform>().position =  Camera.main.WorldToScreenPoint(target.transform.position);
        //this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(target.transform.position);

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector3 WorldObject_ScreenPosition = new Vector3(
                ((ViewportPosition.x *  canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f) - (canvasRect.sizeDelta.y * 0.325f)),
                1);
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position );
        //Vector3 uiPos = new Vector3(screenPos.x - Screen.width + 538.4f, screenPos.y + 401f - Screen.height , 1);
        this.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }


}