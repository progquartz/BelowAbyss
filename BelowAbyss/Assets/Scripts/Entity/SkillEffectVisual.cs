using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectVisual : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void SkillEffectStart()
    {
        animator.SetTrigger("SkillCast");
    }

    public void DeleteSelf()
    {
        Destroy(this.gameObject);
    }
}
