using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyStat stat;

    public SpriteRenderer sprite;


    public void CheckDeath()
    {
        if(stat.currentHp <= 0)
        {
            int realindex = 0;
            for(int i = 0; i < transform.GetSiblingIndex(); i++)
            {
                if (transform.parent.GetChild(i).gameObject.activeInHierarchy)
                {
                    realindex++;
                }
            }
            transform.GetComponentInParent<EnemyHord>().Death(realindex);
            stat.enemyCode = 0;
        }
    }

    private void Update()
    {
        if(stat.enemyCode != 0)
        {
            CheckDeath();
        }
    }


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
