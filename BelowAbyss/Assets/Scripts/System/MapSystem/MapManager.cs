using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵 매니저는, 오직 한번에 한개만 존재할 수 있는 매니저이다.
/// 맵이 처음으로 전개될 경우, 맵매니저는 MapData내부의 데이터를 로드하게 되며, 만약 로드할 데이터가 없다면 이를 새로 만들어낸다.
/// 
/// 만약 MapData 내부의 데이터가 존재하게 된다면, MapManger는 이를 기반으로 Map에 데이터를 넣어 적용하도록 한다.
/// 이는 각각에 존재하는 PathNode와 EventNode가 수행하게 된다.
/// 
/// 만약에 게임 매니저가 새로운 스테이지를 만들어라는 명령을 내리게 된다면, 이를 진행하면 된다.
/// 
/// </summary>
public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    [SerializeField]
    private List<MapData> mapDatas;
    [SerializeField]
    private int currentStage; // 현재 진행중인 스테이지
    [SerializeField]
    private MapVisual MapVisual;
    [SerializeField]
    private GameObject mapdataPrefab;
    [SerializeField]
    private StageLoreUI stageLoreUI;


    // 맵에 추가적으로 적용되어야 하는 멀티플라이어.
    private float stageMultiplier; // 스테이지 증폭률. 나중에 가면 체력 / 공격력 등의 추가 보정으로 나뉠 예정.
    private bool isElite; // 엘리트여부? 존재할지도?

    private float supplyOutRate; // 매 이동마다 보급품 날아가는 비율.

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

    public void FlushAllMapDatas()
    {
        mapDatas = null;
        currentStage = -1;
        mapDatas = new List<MapData>();
        MapVisual = GetComponent<MapVisual>();
    }

    private void Start()
    {
    }

    /// <summary>
    /// 현재 Mapmanager가 가지고 있는 MapData의 해당 인덱스 데이터가 Valid한지 확인함.
    /// </summary>
    /// <returns></returns>
    private bool CheckMapDataValid(int index)
    {
        if(mapDatas == null)
        {
            return false;
        }
        else
        {
            if(mapDatas[index].CheckValidation() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    public void GenerateNextStage(bool isChangeStageInstantly)
    {
        GameObject temp = Instantiate(mapdataPrefab, transform.GetChild(0));
        temp.AddComponent<MapData>();
        mapDatas.Add(temp.GetComponent<MapData>());
        if (isChangeStageInstantly)
        {
            currentStage = mapDatas.Count-1;
            MapVisual.mapdata = mapDatas[currentStage];
            mapDatas[currentStage].MapFirstSetup(currentStage, stageLoreUI);
        }
        else
        {
            mapDatas[mapDatas.Count-1].MapFirstSetup(mapDatas.Count-1);
        }
    }

    // 맵 생성, 맵 내 이동-> 내부데이터는 mapdata에서 처리, 맵 보여주기(비주얼) -> 실제는 mapvisual에서 처리.

    /// <summary>
    /// 다음 함수는 현재 버튼으로 호출당하고 있기 떄문에 참조가 없음.
    /// </summary>
    /// <param name="position"></param>
    public void Move(int position)
    {
        int currentPos = mapDatas[currentStage].GetPosition() + position;

        if (currentPos == 15)
        {
            if (!mapDatas[currentStage].roomVisited)
            {
                EventManager.instance.LoadEvent(mapDatas[currentStage].GetBossEvent());
            }
        }
        else if (currentPos % 3 == 0 && currentPos != 0)
        {
            if (!mapDatas[currentStage].eventVisited[currentPos / 3 - 1])
            {
                Player.instance.
                EventManager.instance.LoadEvent(mapDatas[currentStage].GetEvent(currentPos / 3 - 1));
            }
        }


        mapDatas[currentStage].SetPosition(currentPos);
        MapVisual.UpdateVisual();
        
    }

    public void GoToNextStage()
    {
        GenerateNextStage(true);
    }

    public void MoveTo(int position)
    {
        mapDatas[currentStage].SetPosition(position);
        MapVisual.UpdateVisual();
    }

    private void DeleteMap()
    {
    }
    
}
