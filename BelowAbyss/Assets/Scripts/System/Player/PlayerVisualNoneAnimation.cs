using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EffectTypeVisualData
{
    public EffectType effectType;
    public List<Sprite> sprite;
}
public class PlayerVisualNoneAnimation : MonoBehaviour
{
    [SerializeField]
    private List<EffectTypeVisualData> effectTypeVisualDatas;
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private float frameDelay = 0.1f; // 프레임당 시간


    public void TestPlayEffect()
    {
        PlayEffect(EffectType.POISON);
    }

    public void PlayEffect(EffectType effectType)
    {
        List<Sprite> sprites = null;
        foreach (var effect in effectTypeVisualDatas)
        {
            if (effect.effectType == effectType)
            {
                sprites = effect.sprite;
                break;
            }
        }

        if (sprites == null || sprites.Count == 0)
        {
            Debug.LogWarning($"No sprites found for effect type: {effectType}");
            return;
        }

        GameObject effectObject = Instantiate(effectPrefab, parentTransform);
        effectObject.transform.localPosition = Vector3.zero;
        effectObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        SpriteRenderer sr = effectObject.GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("Effect prefab에 SpriteRenderer가 없습니다.");
            Destroy(effectObject);
            return;
        }

        StartCoroutine(PlayEffectAnimation(effectObject, sr, sprites));
    }

    private IEnumerator PlayEffectAnimation(GameObject effectObject, SpriteRenderer sr,  List<Sprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            if (sr != null)
            {
                sr.sprite = sprite;
                Debug.Log(sprite.name);
            }
                

            yield return new WaitForSeconds(frameDelay);
        }

        Destroy(effectObject);
    }
}
