using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyVisualOnNoneAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private float hitColorChangeTime = 0.1f;
    [SerializeField]
    private bool isHitColorTimeRunning = false;
    [SerializeField]
    private bool isNewHitTriggered = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void ChangeColorToOriginal()
    {
        spriteRenderer.color = Color.white;
        isNewHitTriggered = false;
        isHitColorTimeRunning = false;
    }

    public void HitSpriteColorControl()
    {
        StartCoroutine(IE_SpriteColorChangeOnHit());
    }

    IEnumerator IE_SpriteColorChangeOnHit()
    {
        spriteRenderer.color = new Color(1, 0.5f, 0.5f);
        if(isHitColorTimeRunning)
        {
            isNewHitTriggered = true;
        }
        else
        {
            isNewHitTriggered = false;
        }
        isHitColorTimeRunning = true;
        yield return new WaitForSeconds(hitColorChangeTime);
        if(isNewHitTriggered)
        {
            isNewHitTriggered = false;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
        isNewHitTriggered = false;
        isHitColorTimeRunning = false;
        yield return null;
    }
}
