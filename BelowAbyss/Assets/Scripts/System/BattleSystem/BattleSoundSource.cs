using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSoundSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private int poolingIndex = -1;
    private bool isUsing = false;

    /// <summary>
    /// 전투중에 사용되는 효과음의 오디오 소스를 코드와 볼륨에 맞게 변경합니다.
    /// </summary>
    /// <param name="soundCode">사운드 코드</param>
    /// <param name="volume">볼륨 (0~1)</param>
    public void SetAudioSource(int soundCode, float volume, int poolIndex)
    {
        poolingIndex = poolIndex;
        string path = "Audios/BattleAudios/" + soundCode.ToString();
        audioSource.clip = Resources.Load<AudioClip>(path);
        audioSource.volume = volume;
        Play();
    }

    public void Play()
    {
        audioSource.Play();
        isUsing = true;
    }

    public void GetBackToPull()
    {
        Debug.Log(BattleSoundManager.instance.queueCount);
        BattleSoundManager.instance.soundPoolAvailable.Enqueue(poolingIndex);
        audioSource.clip = null;
        poolingIndex = -1;
    }
    // Update is called once per frame
    void Update()
    {
        if(isUsing && !audioSource.isPlaying)
        {
            isUsing = false;
            GetBackToPull();
        }
    }
}
