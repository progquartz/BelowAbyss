using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� �Ŵ�����, ���� �ѹ��� �Ѱ��� ������ �� �ִ� �Ŵ����̴�.
/// ���� ó������ ������ ���, �ʸŴ����� MapData������ �����͸� �ε��ϰ� �Ǹ�, ���� �ε��� �����Ͱ� ���ٸ� �̸� ���� ������.
/// 
/// ���� MapData ������ �����Ͱ� �����ϰ� �ȴٸ�, MapManger�� �̸� ������� Map�� �����͸� �־� �����ϵ��� �Ѵ�.
/// �̴� ������ �����ϴ� PathNode�� EventNode�� �����ϰ� �ȴ�.
/// 
/// </summary>
public class MapManager : MonoBehaviour
{

    private static MapManager instance;

    private MapData mapData;

    void Awake()
    {
        // Singletone
        if (null == instance)
        {   
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Singletone
    /// </summary>
    public static MapManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    private void Start()
    {
        if (CheckMapDataValid())
        {
            Debug.Log("�� ���缺�� Ȯ�ε�.");
        }
        else
        {
            Debug.Log("�� ���缺�� Ȯ�ε��� ����.");
            Debug.Log("�� �� ������ ������.");
        }
    }

    /// <summary>
    /// ���� Mapmanager�� ������ �ִ� MapData�� Valid���� Ȯ����.
    /// </summary>
    /// <returns></returns>
    private bool CheckMapDataValid()
    {
        if(mapData == null)
        {
            return false;
        }
        else
        {
            if(mapData.CheckValidation() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private void MapGeneration()
    {

    }
    
}

enum MapEventType
{

}
public class Path : MonoBehaviour
{

}

public class Event : MonoBehaviour
{

}
