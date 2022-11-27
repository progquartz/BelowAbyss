using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public TextAsset textJSON;
    public TextAsset textJSON2;

    [System.Serializable]
    public class Player
    {
        public string dialog;
        public int test1;
        public int test2;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player; // 여기의 player가 json에서의 첫 "player"로 적힌것과 같아야지 적용이 됨.
    }

    [System.Serializable]
    public class DialogEvents
    {
        public DialogEvent[] dialogEvents;
    }
    public PlayerList myPlayerList = new PlayerList();

    public DialogEvents DialogEventList = new DialogEvents();

    private void Start()
    {
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
        DialogEventList = JsonUtility.FromJson<DialogEvents>(textJSON2.text);
    }
}
