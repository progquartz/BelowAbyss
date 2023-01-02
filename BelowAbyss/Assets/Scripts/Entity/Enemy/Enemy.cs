using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStat stat;

    public SpriteRenderer sprite;

    public void UpdateSprite()
    {
        if (stat.enemySpriteCode == null)
        {
            sprite.sprite = null;
        }
        else
        {
            string path = "Sprites/Enemy/";
            Sprite image;
            image = Resources.Load<Sprite>((path + stat.enemySpriteCode + "_idle").ToString());
            sprite.sprite = image;
        }
    }

    private void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    
}
