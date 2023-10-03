using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EntitySkillVisualCaller : MonoBehaviour
{
    [SerializeField]
    private GameObject skillOriginalPrefab;
    public void CallSkillVisual(int skillVisualCode)
    {
        LoadSkillData(skillVisualCode);
    }

    private void LoadSkillData(int skillVisualCode)
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

    

}
