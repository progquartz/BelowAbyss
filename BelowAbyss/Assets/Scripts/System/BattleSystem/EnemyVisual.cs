using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField]
    [Header("애니메이션 사용")]
    private bool isUsingAnimation = false;
    private Animator animator;
    [SerializeField]
    private SpriteLibrary spriteLibrary;
    [SerializeField]
    private EnemyVisualOnNoneAnimation noneMovingVisual;
    [SerializeField]
    private EnemyMovingComponent movingVisual;
    

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    public void AttackAnimationOn()
    {
        animator.SetTrigger("Attack");
        movingVisual.AttackState();
    }

    public void HurtAnimationOn()
    {
        movingVisual.HurtState();
    }

    public void DeathAnimation()
    {
        animator.SetBool("IsDead", true);
    }

    public void DeathReset()
    {
        noneMovingVisual.ChangeColorToOriginal();
    }

    public void ChangeSpriteLibrary(string data)
    {
        string path = "_Animations/Enemies/" + data;
        Debug.Log(path + transform.parent.name);
        spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>(path);
    }

}
