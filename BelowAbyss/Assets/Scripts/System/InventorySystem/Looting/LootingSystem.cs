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
        SetTableSkillDataOpen(data);
        SetTableTraitDataOpen(data);
        SetTableEffectDataOpen(data);
        if(data.isLastEvent)
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

    private void SetTableSkillDataOpen(LootingData data)
    {
        if(data.isSkillRootRandom)
        {
            bool isAllSkillAvailable = true;
            List<int> notAvailableSkillCode = new List<int>();
            for(int i = 0; i < data.rootingSkill.Length; i++)
            {
                if(!SkillInventory.instance.CheckSkillAvailable(data.rootingSkill[i]))
                {
                    isAllSkillAvailable = false;
                    notAvailableSkillCode.Add(data.rootingSkill[i]);
                }
            }

            if(!isAllSkillAvailable)
            {
                UISelectionHolder.instance.NewToggleUI(3); // UI 선택창에서 새로 깜박이게 만듬.
                int randomNum = Random.Range(1, notAvailableSkillCode.Count) - 1;
                SkillInventory.instance.GetSkill(notAvailableSkillCode[randomNum]);
            }
        }
        else
        {
            for (int i = 0; i < data.rootingSkill.Length; i++)
            {
                UISelectionHolder.instance.NewToggleUI(3); // UI 선택창에서 새로 깜박이게 만듬.
                SkillInventory.instance.GetSkill(data.rootingSkill[i]);
            }
        }
        
    }

    private void SetTableTraitDataOpen(LootingData data)
    {
        if(data.isTraitRootRandom)
        {
            bool isAllTraitAvailable = true;
            List<int> notAvailableTraitCode = new List<int>();
            for (int i = 0; i < data.rootingTrait.Length; i++)
            {
                if (!TraitInventory.instance.CheckTraitAvailable(data.rootingTrait[i]))
                {
                    isAllTraitAvailable = false;
                    notAvailableTraitCode.Add(data.rootingTrait[i]);
                }
            }
            if (!isAllTraitAvailable)
            {
                UISelectionHolder.instance.NewToggleUI(4); // UI 선택창에서 새로 깜박이게 만듬.
                int randomNum = Random.Range(1, notAvailableTraitCode.Count) - 1;
                TraitInventory.instance.GetTrait(notAvailableTraitCode[ randomNum]);
            }
        }
        else
        {
            for (int i = 0; i < data.rootingTrait.Length; i++)
            {
                UISelectionHolder.instance.NewToggleUI(4); // UI 선택창에서 새로 깜박이게 만듬.
                TraitInventory.instance.GetTrait(data.rootingTrait[i]);
            }
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
