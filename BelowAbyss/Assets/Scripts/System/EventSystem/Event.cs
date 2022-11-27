using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    이벤트 클래스를 기반으로 한 클래스들은, 모두 그 데이터만을 보관한다.
    데이터의 보관 외의 용도로 다음 클래스들은 사용하지 않는다.
 
*/
[System.Serializable]
public class Event
{
    public int eventCode; // 이벤트 코드

    public bool isAdditionalEvent; // 추가 이벤트 존재 여부
    public int additionalEventCode; // 있다면, 이를 실행하는 이벤트코드.

    public int outLookEventCode; // 외부에서 보여지는 이벤트 코드 유형.

}
