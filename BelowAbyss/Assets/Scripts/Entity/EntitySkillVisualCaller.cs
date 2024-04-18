using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EntitySkillVisualCaller : MonoBehaviour
{
    [SerializeField]
    private GameObject skillOriginalPrefab;
    [SerializeField]
    private GameObject buffOriginalPrefab;
    public void CallSkillVisual(int skillVisualCode)
    {
        LoadSkillAniData(skillVisualCode);
    }

    public void CallBuffVisual(string type)
    {
        Debug.Log(type + "로드됨");
        LoadBuffAniData(type);
    }

    // Skill / 기본 공격을 사용했을 경우의 효과를 불러옴.
    private void LoadSkillAniData(int skillVisualCode)
    {
        // prefab load 및 instantiate
        GameObject instantingObject = Instantiate(skillOriginalPrefab, this.transform);
        instantingObject.transform.SetParent(transform);

        // instantiate한 오브젝트의 sprite library 교체 이후 animation trigger 하게 SkillVisualEffect에서 calling.
        string path = "_Animations/Skills/" + skillVisualCode.ToString();
        Debug.Log(path + transform.parent.name);
        instantingObject.GetComponent<SpriteLibrary>().spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>(path);
        instantingObject.GetComponent<SkillEffectVisual>().SkillEffectStart();
    }

    // EffectManager상에서 독 데미지 / 출혈 데미지와 같은 효과를 불러옴.
    private void LoadBuffAniData(string skillVisualCode)
    {
        // prefab load 및 instantiate
        GameObject instantingObject = Instantiate(buffOriginalPrefab, this.transform);
        instantingObject.transform.SetParent(transform);

        // instantiate한 오브젝트의 sprite library 교체 이후 animation trigger 하게 SkillVisualEffect에서 calling.
        string path = "_Animations/BuffEffects/" + skillVisualCode.ToString();
        Debug.Log(path + transform.parent.name);
        instantingObject.GetComponent<SpriteLibrary>().spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>(path);
        instantingObject.GetComponent<SkillEffectVisual>().SkillEffectStart();
    }

}
