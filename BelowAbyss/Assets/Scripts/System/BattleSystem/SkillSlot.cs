using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image skillImage;
    public Image skillCooltimeUI;
    public Image skillDelayUI;

    // 변경되지 않는 부분.
    private bool isSkillSlotFilled = false;
    private bool isSkillNeedDelay = false;


    // 업데이트에서 변경되는 부분.
    [SerializeField]
    private bool isSkillEnabled = false;
    private float coolTimeLeft = 0.0f;
    private float delayNeeded;


    public void TestSkillSlotGo()
    {
        SkillData tempSkillData = new SkillData();
        tempSkillData.skillName = "테스트 스킬";
        tempSkillData.skillLore = "스킬을 테스트하기 위한 테스트 스킬입니다.";
        tempSkillData.skillCooltime = 5.0f;
        tempSkillData.skillCode = 1;
        tempSkillData.skillIconCode = 1;
        tempSkillData.skillDelay = 3.0f;

        SetupSkillData(tempSkillData);
    }

    public void SetupSkillData(SkillData _skillData)
    {
        if(_skillData == null)
        {
            skillData = null;
            isSkillSlotFilled = false;
        }
        else if(_skillData.skillCode == 0)
        {
            skillData = null;
            isSkillSlotFilled = false;
        }
        else
        {
            skillData = _skillData;
            isSkillSlotFilled = true;
            if(skillData.skillDelay != 0.0f)
            {
                isSkillNeedDelay = true;
            }
            delayNeeded = skillData.skillDelay;
            Sprite image;
            image = Resources.Load<Sprite>("Sprites/Skill/" +  _skillData.skillIconCode.ToString());
            skillImage.sprite = image;
        }
    }

    public void DeleteSkillData()
    {
        skillData = null;
        isSkillSlotFilled = false;
    }

    private void Update()
    {
        if (isSkillSlotFilled) // 스킬 슬롯에 데이터가 들어가있는지.
        {
            if (BattleManager.instance.isBattleStarted) // 전투 시작.
            {
                if (isSkillNeedDelay && !isSkillEnabled) // 만약 딜레이가 필요한데 스킬 활성화가 되지 않은 경우.
                {
                    delayNeeded -= Time.deltaTime;
                    if (delayNeeded <= 0)
                    {
                        isSkillEnabled = true;
                    }
                }
                else // 쿨타임 작동 시작.
                {
                    isSkillEnabled = true;
                    CheckCooltime();
                }
            }
            else // 전투 종료 및 초기화.
            {
                delayNeeded = skillData.skillDelay;
                isSkillEnabled = false;
                coolTimeLeft = 0.0f;
                skillDelayUI.fillAmount = 1.0f;
                skillCooltimeUI.fillAmount = 1.0f;
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
        if(coolTimeLeft <= 0.0f)
        {
            UseSkill();
            coolTimeLeft = skillData.skillCooltime;
            coolTimeLeft -= Time.deltaTime;
        }
        else
        {
            coolTimeLeft -= Time.deltaTime;
        }
    }

    private void UpdateVisual()
    {
        skillImage.color = new Color(1, 1, 1, 1);
        if (BattleManager.instance.isBattleStarted)
        {
            if (!isSkillEnabled)
            {
                if (skillData.skillDelay != 0)
                {
                    skillDelayUI.fillAmount = delayNeeded / skillData.skillDelay;
                }
            }
            else
            {
                skillDelayUI.fillAmount = 0.0f;
                skillCooltimeUI.fillAmount = coolTimeLeft / skillData.skillCooltime;
            }
        }
        else
        {
            skillDelayUI.fillAmount = 0.0f;
            skillCooltimeUI.fillAmount = 0.0f;
        }
    }

    private void UpdateNoneVisual()
    {
        skillDelayUI.fillAmount = 0.0f;
        skillCooltimeUI.fillAmount = 0.0f;
        skillImage.sprite = null;
        skillImage.color = new Color(1, 1, 1, 0);
    }

    private void UseSkill()
    {
        Debug.Log(skillData.skillCode + "]" + skillData.skillName + " 을 사용합니다.");
        Player.instance.PlayerAttackAnimation();
        if (skillData.skillEffect1[0] != "")
        {
            for(int i = 0; i < skillData.skillEffect1.Length; i++)
            {
                EffectData effData = new EffectData(skillData.skillEffect1[i], skillData.skillEffect2[i], skillData.skillEffect3[i]);
                EffectManager.instance.AmplifyEffect(effData);
                EffectManager.instance.AmplifyVisualEffect(effData, skillData.skillAniAttackCode, skillData.skillAniBuffCode);
            }
        }
    }



}
