using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundEffect : MonoBehaviour
{
    public static UISoundEffect instance;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void EquipUnEquipSound()
    {
        audioSource.clip = audioClips[0];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void ButtonClickSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void ItemPickUpSound()
    {
        audioSource.clip = audioClips[2];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void UseItemSound()
    {
        audioSource.clip = audioClips[3];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }



}
