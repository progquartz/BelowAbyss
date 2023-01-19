using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPlayerDeath()
    {

    }

    public void OnBattleEnd()
    {

    }

    public void OnBattleStart()
    {

    }
}
