using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    public Animator animator;
    public Animator effectAnimator;
    public AudioSource effectAudioSource;

    [SerializeField]
    private AudioClip[] effectaudioClips;


    void Awake()
    {
        // Singletone
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMove(bool isWalk)
    {
        animator.SetBool("isMoving", isWalk);
    }

    public void SetBattle(bool isBattling)
    {
        animator.SetBool("isBattle", isBattling);
    }

    public void Smash()
    {
        animator.SetTrigger("Smash");
    }

    public void EventSoundEffect()
    {
        effectAnimator.SetTrigger("HasEvent");
        effectAudioSource.clip = effectaudioClips[0];
        effectAudioSource.volume = 0.1f;
        effectAudioSource.Play();
    }
    
}
