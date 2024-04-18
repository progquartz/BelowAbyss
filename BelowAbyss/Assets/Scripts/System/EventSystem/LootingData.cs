[System.Serializable]
public class LootingData
{
    public int eventCode;
    public bool isLastEvent;
    public int additionalEventCode;
    public int[] rootingItem;
    public int[] rootingMin;
    public int[] rootingMax;
    public bool isSkillRootRandom;
    public int[] rootingSkill;
    public bool isTraitRootRandom;
    public int[] rootingTrait;
    public string[] rootingEffect1;
    public string[] rootingEffect2;
    public string[] rootingEffect3;
}
