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

    private void FixedUpdate()
    {
        if (isSkillSlotFilled) // 스킬 슬롯에 데이터가 들어가있는지.
        {
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
        skillDelayUI.fillAmount = 0.0f;
        skillCooltimeUI.fillAmount = 0.0f;
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
            }
        }
    }
}
