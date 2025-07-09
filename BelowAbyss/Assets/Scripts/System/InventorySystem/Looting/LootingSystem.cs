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

    public string LootTableOpen(LootingData data)
    {
        UISoundEffect.instance.ItemPickUpSound();
        string itemLoot = SetTableItemDataOpen(data);
        string effectLoot = SetTableEffectDataOpen(data);
        if(data.isAdditionalEvent)
        {
            EventManager.instance.LoadEvent(data.additionalEventCode);
        }
        else
        {
            MapManager.Instance.MoveFront();
        }
        return itemLoot + "\n" + effectLoot;
    }

    private string SetTableItemDataOpen(LootingData data)
    {
        string itemLoots = "";
        bool isItemExist = false;
        for (int i = 0; i < data.rootingItem.Length; i++)
        {
            UISelectionHolder.instance.NewToggleUI(1); // UI 선택창에서 새로 깜박이게 만듬.
            int rootingCount = Random.Range(data.rootingMin[i], data.rootingMax[i]);
            if(rootingCount > 0)
            {
                if(!isItemExist)
                {
                    isItemExist = true;
                    itemLoots += "+ ";
                }
                bool isItemGotten = Inventory.instance.GetItem(data.rootingItem[i], rootingCount);
                if (isItemGotten)
                {
                    itemLoots += $"[{ItemDataBase.instance.LoadItemData(data.rootingItem[i]).itemName}x{rootingCount}]";
                }
            }
        }
        return itemLoots;
    }

    private string SetTableEffectDataOpen(LootingData data)
    {
        string effectLoots = "";
        bool isitemExist = false;
        for (int i = 0; i < data.rootingEffect1.Length; i++)
        {
            EffectData tmp = new EffectData(data.rootingEffect1[i], data.rootingEffect2[i], data.rootingEffect3[i]);
            EffectManager.instance.AmplifyEffect(tmp);
        }
        return effectLoots;
    }

}
