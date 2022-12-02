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
    private List<MapData> mapData;
    [SerializeField]
    private int currentStage; // ���� �������� ��������
    [SerializeField]
    private MapVisual MapVisual;
    [SerializeField]
    private GameObject mapdataPrefab;

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
        currentStage = -1;
        mapData = null;
        MapVisual = GetComponent<MapVisual>();
        mapData = new List<MapData>();
    }

    /// <summary>
    /// ���� Mapmanager�� ������ �ִ� MapData�� �ش� �ε��� �����Ͱ� Valid���� Ȯ����.
    /// </summary>
    /// <returns></returns>
    private bool CheckMapDataValid(int index)
    {
        if(mapData == null)
        {
            return false;
        }
        else
        {
            if(mapData[index].CheckValidation() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public void TestMapGen()
    {
        MapGeneration(1, true);
    }
    public void MapGeneration(int genStageNum, bool isChangeStageInstantly)
    {
        GameObject temp = Instantiate(mapdataPrefab, transform.GetChild(0));
        temp.AddComponent<MapData>();
        mapData.Add(temp.GetComponent<MapData>());
        if (isChangeStageInstantly)
        {
            currentStage = mapData.Count-1;
            MapVisual.mapdata = mapData[currentStage];
            mapData[currentStage].MapFirstSetup(currentStage);
        }
        else
        {
            mapData[mapData.Count-1].MapFirstSetup(mapData.Count-1);
        }
    }

    public void MapChange(int stageNum)
    {
        currentStage = stageNum;
        MapVisual.mapdata = mapData[currentStage];
        MapVisual.UpdateVisual();
    }

    // �� ����, �� �� �̵�-> ���ε����ʹ� mapdata���� ó��, �� �����ֱ�(���־�) -> ������ mapvisual���� ó��.

    public void Move(int position)
    {
        mapData[currentStage].SetPosition((mapData[currentStage].GetPosition() + position));
        MapVisual.UpdateVisual();
    }


    public void MoveTo(int position)
    {
        mapData[currentStage].SetPosition(position);
    }

    private void DeleteMap()
    {
    }
    
}
