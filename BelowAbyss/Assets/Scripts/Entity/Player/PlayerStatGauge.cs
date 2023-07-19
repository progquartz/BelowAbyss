using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatGauge : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    public RectTransform[] statGauges = new RectTransform[4];
    public float maxSize = 126;

    PlayerStat stat;

    private void Start()
    {
        stat = Player.instance.stat;
    }
    public float getPercentSize(float percent)
    {
        return (1 - percent) * maxSize;
    }
    private void Update()
    {
        statGauges[0].offsetMax = new Vector2(statGauges[0].offsetMax.x, -getPercentSize((float)stat.currentVital/ (float)stat.maxVital));
        statGauges[1].offsetMax = new Vector2(statGauges[1].offsetMax.x, -getPercentSize((float)stat.currentSanity / (float)stat.maxSanity));
        statGauges[2].offsetMax = new Vector2(statGauges[2].offsetMax.x, -getPercentSize((float)stat.currentSatur / (float)stat.maxSatur));
        statGauges[3].offsetMax = new Vector2(statGauges[3].offsetMax.x, -getPercentSize((float)stat.currentThirst / (float)stat.maxThirst));
    }

    public void IsStatInDanger()
    {
        if(stat.currentVital <= 25 || stat.currentHp <= 25 || stat.currentSanity <= 25 ||
           stat.currentSatur <= 25 || stat.currentThirst <= 25)
        {
            StartCoroutine(WarningSound());
        }
        else
        {
            return;
        }
    }

    IEnumerator WarningSound()
    {
        for(int i = 0; i < 3; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
