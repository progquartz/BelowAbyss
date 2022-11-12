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
            Debug.Log("맵 정당성이 확인됨.");
        }
        else
        {
            Debug.Log("맵 정당성이 확인되지 못함.");
            Debug.Log("새 맵 데이터 생성중.");
        }
    }

    /// <summary>
    /// 현재 Mapmanager가 가지고 있는 MapData가 Valid한지 확인함.
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
