using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    public static PlayerSoundEffect instance;

    public AudioSource[] audioSources;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSources = GetComponents<AudioSource>();
    }

    public void WalkSound()
    {
        audioSources[0].clip = audioClips[0];
        audioSources[0].volume = 0.3f;
        audioSources[0].Play();
    }

}
