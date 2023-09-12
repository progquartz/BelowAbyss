using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public WeaponItemData weaponData;
    public Image weaponImage;
    public Image weaponCooltimeUI;
    public Image weaponDelayUI;

    // 변경되지 않는 부분.
    private bool isSkillSlotFilled = false;


    // 업데이트에서 변경되는 부분.
    [SerializeField]
    private bool isWeaponEnabled = false;
    private double coolTimeLeft = 0.0f;

    public void TestSkillSlotGo()
    {
        WeaponItemData tempWeaponItemData = new WeaponItemData();
        tempWeaponItemData.itemName = "테스트 스킬";
        tempWeaponItemData.itemLore = "스킬을 테스트하기 위한 테스트 스킬입니다.";
        tempWeaponItemData.attackSpeed = 1.0f;
        tempWeaponItemData.itemCode = 101;
        tempWeaponItemData.criticalChance = 10;

        SetupWeaponItemData(tempWeaponItemData);
    }

    public void SetupWeaponItemData(WeaponItemData _weaponData)
    {
        if (_weaponData == null)
        {
            weaponData = null;
            isSkillSlotFilled = false;
        }
        else if (_weaponData.itemCode == 0)
        {
            weaponData = null;
            isSkillSlotFilled = false;
        }
        else
        {
            weaponData = _weaponData;
            isSkillSlotFilled = true;
            Sprite image;
            image = Resources.Load<Sprite>("Sprites/Item/" + _weaponData.itemCode.ToString());
            weaponImage.sprite = image;
        }
    }

    public void DeleteWeaponItemData()
    {
        weaponData = null;
        isSkillSlotFilled = false;
    }

    private void Update()
    {
        if (isSkillSlotFilled) // 스킬 슬롯에 데이터가 들어가있는지.
        {
            if (BattleManager.instance.isBattleStarted) // 전투 시작.
            {
                isWeaponEnabled = true;
                CheckCooltime();
            }
            else // 전투 종료 및 초기화.
            {
                isWeaponEnabled = false;
                coolTimeLeft = 0.0f;
                weaponDelayUI.fillAmount = 1.0f;
                weaponCooltimeUI.fillAmount = 1.0f;
            }

            UpdateVisual();
        }
        else
        {
            UpdateNoneVisual();
        }

    }

    private void CheckCooltime()
    {
        if (coolTimeLeft <= 0.0f)
        {
            UseWeapon();
            coolTimeLeft = weaponData.attackSpeed;
            coolTimeLeft -= Time.deltaTime;
        }
        else
        {
            coolTimeLeft -= Time.deltaTime;
        }
    }

    private void UpdateVisual()
    {
        weaponImage.color = new Color(1, 1, 1, 1);
        if (BattleManager.instance.isBattleStarted)
        {
            weaponDelayUI.fillAmount = 0.0f;
            weaponCooltimeUI.fillAmount = (float)(coolTimeLeft / weaponData.attackSpeed);
        }
        else
        {
            weaponDelayUI.fillAmount = 0.0f;
            weaponCooltimeUI.fillAmount = 0.0f;
        }
    }

    private void UpdateNoneVisual()
    {
        weaponDelayUI.fillAmount = 0.0f;
        weaponCooltimeUI.fillAmount = 0.0f;
        weaponImage.sprite = null;
        weaponImage.color = new Color(1, 1, 1, 0);
    }

    private void UseWeapon()
    {
        EffectData effectData = new EffectData();
        for(int i = 0; i < weaponData.hitEffect1.Length; i++)
        {
            effectData.SetEffectData(weaponData.hitEffect1[i], weaponData.hitEffect2[i], weaponData.hitEffect3[i]);
            EffectManager.instance.AmplifyEffect(effectData);
        }
        if(weaponData.criticalChance != 0)
        {
            int randomnum = Random.Range(1, 101); // 1에서 100까지.
            if(randomnum > weaponData.criticalChance)
            {
                for(int i = 0; i < weaponData.criticalEffect1.Length; i++)
                {
                    effectData.SetEffectData(weaponData.criticalEffect1[i], weaponData.criticalEffect2[i], weaponData.criticalEffect3[i]);
                }
            }
        }
        Debug.Log(weaponData.itemCode + "]" + weaponData.itemName + " 을 사용합니다.");
    }



}
