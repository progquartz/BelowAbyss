using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthBar : MonoBehaviour
{
    public SpriteRenderer target;
    public Player player;
    [SerializeField]
    private RectTransform currentHealthBar;
    [SerializeField]
    private RectTransform healthBarHolder;

    [SerializeField]
    private Vector3 healthPosOffset = new Vector3(); 

    int maxSize = 190;

    private void Start()
    {
        healthBarHolder = transform.GetChild(0).GetComponent<RectTransform>();
        currentHealthBar = transform.GetChild(1).GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        currentHealthBar.sizeDelta = new Vector2(((float)player.stat.currentHp / (float)player.stat.maxHp) * maxSize, 13);
    }
}