using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    �̺�Ʈ Ŭ������ ������� �� Ŭ��������, ��� �� �����͸��� �����Ѵ�.
    �������� ���� ���� �뵵�� ���� Ŭ�������� ������� �ʴ´�.
 
*/
[System.Serializable]
public class Event
{
    public int eventCode; // �̺�Ʈ �ڵ�

    public bool isAdditionalEvent; // �߰� �̺�Ʈ ���� ����
    public int additionalEventCode; // �ִٸ�, �̸� �����ϴ� �̺�Ʈ�ڵ�.

    public int outLookEventCode; // �ܺο��� �������� �̺�Ʈ �ڵ� ����.

}
