using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraitInventory : MonoBehaviour
{
    public static TraitInventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public List<TraitData> traitDB;
    public int traitDBCount = 40;
    public int lastDBAvailableIndex = 0;
    public int visualSlotYIndex = 0;

    [SerializeField]
    private List<GameObject> traitSlot;

    [SerializeField]
    private GameObject upButton;
    [SerializeField]
    private GameObject downButton;

    public void ToggleInventory()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        FirstSetup();
    }
    /// <summary>
    ///  이 함수는 GameManager에서 실행되고 있음.
    /// </summary>
    public void FirstSetup()
    {
        traitDB = new List<TraitData>();

        for (int i = 0; i < traitDBCount; i++)
        {
            traitDB.Add(new TraitData());
            traitSlot.Add(transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).gameObject);
            
        }
    }

    private void Update()
    {
        /// 스킬 업데이트 풀어줘야함!
        UpdateSprite();
    }

    private int GetTraitIndex(int traitCode)
    {
        for (int i = 0; i < traitDBCount; i++)
        {
            if(traitDB[i].traitCode == traitCode)
            {
                return i;
            }
        }
        return -1;
    }

    public bool CheckTraitAvailable(int traitCode)
    {
        for (int i = 0; i < traitDBCount; i++)
        {
            if (traitDB[i].traitCode == traitCode)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateSprite()
    {

        UpdateTraitSlot();

    }

    private void UpdateTraitSlot()
    {
        for (int i = 0; i < traitDBCount; i++)
        {
            if(traitDB[i].traitCode != -1)
            {
                traitSlot[i].transform.GetChild(0).gameObject.SetActive(true);
                traitSlot[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    TraitDataBase.instance.GetTraitData(traitDB[i].traitCode).traitName.ToString();
                traitSlot[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    TraitDataBase.instance.GetTraitData(traitDB[i].traitCode).traitLore.ToString();

            }
            else
            {
                traitSlot[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public TraitData GetRealSkillFromIndex(int index)
    {
        return traitDB[index];
    }

    public void GetTrait(int traitCode)
    {
        if(!CheckTraitAvailable(traitCode))
        {
            traitDB[lastDBAvailableIndex] = TraitDataBase.instance.GetTraitData(traitCode);
            EffectTraitLooted(traitCode);
            lastDBAvailableIndex++;
        }
    }

    public void EffectTraitLooted(int traitCode)
    {
        TraitData traitEffData = TraitDataBase.instance.GetTraitData(traitCode);
        for (int effCount = 0; effCount < traitEffData.traitEffect1.Length;  effCount++)
        {
            EffectManager.instance.AmplifyEffect(traitEffData.traitEffect1[effCount], traitEffData.traitEffect2[effCount], traitEffData.traitEffect3[effCount]);
        }
    }

}
