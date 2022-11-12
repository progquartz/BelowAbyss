using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    /// <summary>
    /// MapData는 기본적으로 맵의 데이터를 저장하도록 만들어진 클래스이다.
    /// 
    /// 저장되어야 할 목록들은 일반적으로 다음과 같다.
    /// - 모든 맵 내의 데이터들 (경로 유형, 이벤트 유형, 다음 방 유형)
    /// - 맵에 추가적으로 적용되어야 하는 멀티플라이어들. // bool값으로 저장되어, 이들을 MapManager에서 관리하여 처리될 예정.
    /// - 매 이동(경로, 이벤트)에서 소비되어야 할 수치.
    /// 
    /// </summary>

    private void Start()
    {
        
    }

    /// <summary>
    /// 정당성 여부 체크. 이상한 수치 있으면 알아서 걸러내기!
    /// </summary>
    /// <returns></returns>
    public bool CheckValidation()
    {
        return true;
    }
}

