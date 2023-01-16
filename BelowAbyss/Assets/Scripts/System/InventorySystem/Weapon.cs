public class Weapon : Item
{
    public int damage;
    public float attackSpeed;

    public string[] eff1;
    public string[] eff2;
    public string[] eff3;


    public Weapon(int _itemcode, int _stack) : base (_itemcode, _stack)
    {
        itemcode = _itemcode;
        stack = 1;
    }

    public Weapon(int _itemcode, int _stack, int _stacklimit) : base (_itemcode, _stack, _stacklimit)
    {
        itemcode = _itemcode;
        stack = 1;
        stacklimit = 1;
    }

    /// <summary>
    /// 해당 인덱스의 이펙트를 불러오는 함수. 정상적으로 불러오게 된다면 true를 리턴, 실패한다면 false를 리턴.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool EffectCalling(int index)
    {
        if(eff1.Length == 0)
        {
            return false;
        }
        return false;
        
    }
}
