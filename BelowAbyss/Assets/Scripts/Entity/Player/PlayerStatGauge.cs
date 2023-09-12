using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatGauge : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    public RectTransform[] statGauges = new RectTransform[4];
    public float maxSize = 126;

    [SerializeField]
    private PlayerStat stat;

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

        statGauges[0].localScale = new Vector3(1, (float)stat.currentHp/ (float)stat.maxHp, 1);
        statGauges[1].localScale = new Vector3(1, (float)stat.currentSanity / (float)stat.maxSanity, 1);
        statGauges[2].localScale = new Vector3(1, (float)stat.currentSatur / (float)stat.maxSatur, 1);
        statGauges[3].localScale = new Vector3(1, (float)stat.currentThirst / (float)stat.maxThirst, 1);
    }

    public void IsStatInDanger()
    {
        if (stat == null)
        {
            stat = Player.instance.stat;
        }
        if (stat.currentHp <= 25 || stat.currentHp <= 25 || stat.currentSanity <= 25 ||
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
