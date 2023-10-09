using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGMSource : MonoBehaviour
{
    public static BGMSource instance;
    [SerializeField]
    private AudioSource bgmAudio;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Pause()
    {
        bgmAudio.Pause();
    }

    public void Play()
    {
        bgmAudio.Play();
    }

    public void SetVolume(float volume)
    {
        bgmAudio.volume = volume;
    }

    public void ChangeTheme(int themeCode)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
