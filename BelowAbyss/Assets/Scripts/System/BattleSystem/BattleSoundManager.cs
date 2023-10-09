using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSoundManager : MonoBehaviour
{
    public static BattleSoundManager instance;
    [SerializeField]
    private GameObject battleSoundSouceOriginal;

    public List<BattleSoundSource> battleSoundPool = new List<BattleSoundSource>();
    public Queue<int> soundPoolAvailable = new Queue<int>();
    public int queueCount;

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
    public void PlaySound(int soundCode, float volume)
    {
        if(soundCode != 0)
        {
            if (soundPoolAvailable.Count <= 0)
            {
                GameObject tmpSound = Instantiate(battleSoundSouceOriginal, transform);
                tmpSound.transform.SetParent(this.transform);
                BattleSoundSource source = tmpSound.GetComponent<BattleSoundSource>();

                source.SetAudioSource(soundCode, volume, battleSoundPool.Count);
                battleSoundPool.Add(source);
            }
            else
            {
                int leftPool = soundPoolAvailable.Dequeue();
                battleSoundPool[leftPool].SetAudioSource(soundCode, volume, leftPool);
            }
        }
    }

    public void PlaySound(int soundCode)
    {
        if (soundPoolAvailable.Count <= 0)
        {
            GameObject tmpSound = Instantiate(battleSoundSouceOriginal, transform);
            tmpSound.transform.SetParent(this.transform);
            BattleSoundSource source = tmpSound.GetComponent<BattleSoundSource>();

            source.SetAudioSource(soundCode, 1f, battleSoundPool.Count);
            battleSoundPool.Add(source);
        }
        else
        {
            int leftPool = soundPoolAvailable.Dequeue();
            battleSoundPool[leftPool].SetAudioSource(soundCode, 1f, leftPool);
        }
    }

    private void Update()
    {
        queueCount = soundPoolAvailable.Count;
    }
}
