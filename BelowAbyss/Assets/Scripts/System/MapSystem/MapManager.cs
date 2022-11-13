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
/// ���࿡ ���� �Ŵ����� ���ο� ���������� ������� ����� ������ �ȴٸ�, �̸� �����ϸ� �ȴ�.
/// 
/// </summary>
public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private MapVisual MapVisual;
    [SerializeField]
    private int stageNum;

    // �ʿ� �߰������� ����Ǿ�� �ϴ� ��Ƽ�ö��̾�.
    private float stageMultiplier; // �������� ������. ���߿� ���� ü�� / ���ݷ� ���� �߰� �������� ���� ����.
    private bool isElite; // ����Ʈ����? ����������?

    private float supplyOutRate; // �� �̵����� ����ǰ ���ư��� ����.

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
        stageNum = -1;
        mapData = null;
        MapVisual = GetComponent<MapVisual>();
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

    public void MapGeneration(int genStageNum)
    {
        this.gameObject.AddComponent<MapData>();
        mapData = this.GetComponent<MapData>();
        MapVisual.mapdata = mapData;
        stageNum = genStageNum;
        mapData.MapFirstSetup(stageNum);
    }

    // �� ����, �� �� �̵�-> ���ε����ʹ� mapdata���� ó��, �� �����ֱ�(���־�) -> ������ mapvisual���� ó��.

    public void Move(int position)
    {
        mapData.SetPosition((mapData.GetPosition() + position));
        MapVisual.UpdateVisual();
    }


    public void MoveTo(int position)
    {
        mapData.SetPosition(position);
    }

    private void DeleteMap()
    {
    }
    
}
