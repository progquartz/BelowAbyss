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

    public void LootTableOpen(LootingData data)
    {
        UISoundEffect.instance.ItemPickUpSound();
        SetTableItemDataOpen(data);
        SetTableEffectDataOpen(data);
        if(data.isAdditionalEvent)
        {
            MapManager.Instance.MoveFront();
        }
        else
        {
            EventManager.instance.LoadEvent(data.additionalEventCode);
        }
    }

    private void SetTableItemDataOpen(LootingData data)
    {
        for (int i = 0; i < data.rootingItem.Length; i++)
        {
            UISelectionHolder.instance.NewToggleUI(1); // UI 선택창에서 새로 깜박이게 만듬.
            Inventory.instance.GetItem(data.rootingItem[i], Random.Range(data.rootingMin[i], data.rootingMax[i]));
        }
    }

    private void SetTableEffectDataOpen(LootingData data)
    {
        for(int i = 0; i < data.rootingEffect1.Length; i++)
        {
            EffectData tmp = new EffectData(data.rootingEffect1[i], data.rootingEffect2[i], data.rootingEffect3[i]);
            EffectManager.instance.AmplifyEffect(tmp);
        }
    }

}
