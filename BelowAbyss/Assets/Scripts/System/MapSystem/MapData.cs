using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    /// <summary>
    /// MapData�� �⺻������ ���� �����͸� �����ϵ��� ������� Ŭ�����̴�.
    /// 
    /// ����Ǿ�� �� ��ϵ��� �Ϲ������� ������ ����.
    /// - ��� �� ���� �����͵� (��� ����, �̺�Ʈ ����, ���� �� ����)
    /// - �ʿ� �߰������� ����Ǿ�� �ϴ� ��Ƽ�ö��̾��. // bool������ ����Ǿ�, �̵��� MapManager���� �����Ͽ� ó���� ����.
    /// - �� �̵�(���, �̺�Ʈ)���� �Һ�Ǿ�� �� ��ġ.
    /// 
    /// </summary>

    private void Start()
    {
        
    }

    /// <summary>
    /// ���缺 ���� üũ. �̻��� ��ġ ������ �˾Ƽ� �ɷ�����!
    /// </summary>
    /// <returns></returns>
    public bool CheckValidation()
    {
        return true;
    }
}

