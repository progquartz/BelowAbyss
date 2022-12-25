[System.Serializable]
public class RecipeData
{
    public int sizeX; // 조합창에서 X축의 크기.
    public int sizeY; // 조합창에서의 Y축의 크기.
    public int[] craftingRecipe; // 조합법 → [2,2,1,1,1,1]과 같이적힘.
    public int craftingItem;  // 조합되는 아이템코드. 
}

