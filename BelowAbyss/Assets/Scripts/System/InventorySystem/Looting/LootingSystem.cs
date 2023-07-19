using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootingSystem : MonoBehaviour
{
    public static LootingSystem instance;

    void Awake()
    {
        // Singletone
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void Test1()
    {
        EventManager.instance.LoadEvent(41);
    }

    public void LootTableOpen(LootingData data)
    {
        UISoundEffect.instance.ItemPickUpSound();
        SetTableDataOpen(data);
    }

    private void SetTableDataOpen(LootingData data)
    {
        for (int i = 0; i < data.rootingItem.Length; i++)
        {
            Inventory.instance.GetItem(data.rootingItem[i], Random.Range(data.rootingMin[i], data.rootingMax[i]));
        }
    }

}
