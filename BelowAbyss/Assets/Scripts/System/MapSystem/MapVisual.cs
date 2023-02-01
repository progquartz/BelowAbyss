using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisual : MonoBehaviour
{

    public MapData mapdata;

    [SerializeField]
    List<SpriteRenderer> paths = new List<SpriteRenderer>();
    [SerializeField]
    List<SpriteRenderer> events = new List<SpriteRenderer>();

    [SerializeField]
    SpriteRenderer startRoom;
    [SerializeField]
    SpriteRenderer endRoom;

    // 색들
    Color NORMAL = new Color(0.31f, 0.31f, 0.31f, 1f);
    Color VISITED = new Color(0.64f, 0.64f, 0.64f, 1f);
    Color PLAYERPOS = new Color(0.6f, 0.87f, 0.63f, 1f);
    Color BATTLE = new Color();
    Color TRAIT = new Color();
    Color MOVEMENT = new Color();


    // Start is called before the first frame update
    void Start()
    {
        startRoom = transform.GetChild(1).GetComponent<SpriteRenderer>();
        endRoom = transform.GetChild(2).GetComponent<SpriteRenderer>();
        for (int i = 0; i < 10; i++)
        {
            paths.Add(transform.GetChild(3).GetChild(i).GetComponent<SpriteRenderer>());
        }
        for (int i = 0; i < 4; i++)
        {
            events.Add(transform.GetChild(4).GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    public void UpdateVisual()
    {
        UpdateVisualOnVisited();

        UpdateVisualOnPlayerPos();
        

    }

    private void UpdateVisualOnEvents()
    {
        for(int i = 0; i < 4; i++)
        {
            if (mapdata.GetEventVisited(i))
            {
                var eventCode = mapdata.GetEvent(i);
                EventType eventType =  EventManager.instance.LoadEventType(eventCode);
                switch (eventType)
                {
                    case EventType.BATTLE:
                        break;
                    case EventType.DIALOG:
                        break;
                    case EventType.SELECTION:
                        break;
                }
            }
        }

    }

    private void UpdateVisualOnVisited()
    {
        
        for (int i = 0; i < 10; i++)
        {
            if (mapdata.GetPathVisited(i))
            {
                ChangeColor(paths[i], VISITED);
            }
            else
            {
                ChangeColor(paths[i], NORMAL);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (mapdata.GetEventVisited(i))
            {
                ChangeColor(events[i], VISITED);
            }
            else
            {
                ChangeColor(events[i], NORMAL);
            }
        }
        

        if (mapdata.GetRoomVisited())
        {
            ChangeColor(endRoom, VISITED);
        }
        else
        {
            ChangeColor(endRoom, NORMAL);
        }

        ChangeColor(startRoom, VISITED);


    }

    private void UpdateVisualOnPlayerPos()
    {
        if (mapdata.GetPosition() == 0)
        {
            ChangeColor(startRoom, PLAYERPOS);
            return;
        }
        if(mapdata.GetPosition() == 15)
        {
            ChangeColor(endRoom, PLAYERPOS);
            return;
        }
        // 이벤트의 경우.
        if(mapdata.GetPosition() % 3 == 0)
        {
            ChangeColor(events[mapdata.GetPosition() / 3 - 1], PLAYERPOS);
            return;
        }
        if (mapdata.GetPosition() % 3 == 1)
        {
            ChangeColor(paths[(2 * (mapdata.GetPosition() / 3))], PLAYERPOS);
            return;
        }
        if(mapdata.GetPosition() % 3 == 2)
        {
            ChangeColor(paths[(2 * (mapdata.GetPosition() / 3)) + 1], PLAYERPOS);
            return;
        }
    }
    

    private void ChangeColor(SpriteRenderer target, Color color)
    {
        target.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisual();
    }
}
