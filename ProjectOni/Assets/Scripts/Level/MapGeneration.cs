﻿using UnityEngine;

public class MapGeneration : MonoBehaviour
{

   private static MapGeneration instance;

    public static MapGeneration Instance {
        get {
            instance = FindObjectOfType<MapGeneration>();
            if(instance == null) {
                GameObject go = new GameObject("MapGeneration");
                instance = go.AddComponent<MapGeneration>();
            }
            return instance;
        }
    }
    [SerializeField]private Transform mapParent;
    [SerializeField]private float roomScaler = 2;

    private LevelGeneration lvlGen;
    private MapManager mapManager;
    private Transform[] roomsPos;
    private Room[] roomsInfo;
    private bool startGeneration = false;
    private GameObject[] Rooms;
    private int roomsNum = 0;

    public int StartGeneration{
        set{
            lvlGen = LevelGeneration.Instance;
            mapManager = MapManager.Instance;
            roomsPos = lvlGen.Rooms;
            roomsInfo = lvlGen.RoomsInfo;
            roomsNum = value;
            startGeneration = true;
            Rooms = new GameObject[roomsNum];
        }
    }

    // Update is called once per frame
    void Update(){
        if(startGeneration){
            for (int i = 0; i < roomsNum; i++){
                transform.position = roomsPos[i].position * roomScaler;
                GameObject go = Instantiate(mapManager.GetRoomOfType(roomsInfo[i].rType), 
                             transform.position, Quaternion.identity);
                go.transform.parent = mapParent;
                Rooms[i] = go;
                SetLinks(i);
            }
            CameraPos();
            startGeneration = false;
        }
    }

    private void SetLinks(int i){
        Rooms[i].GetComponent<Room>().SetLinks(roomsInfo[i].left,roomsInfo[i].right, 
                                        roomsInfo[i].up,roomsInfo[i].down);
        Rooms[i].GetComponent<DoorManager>().SetDoors(roomsInfo[i].down,roomsInfo[i].up, 
                                        roomsInfo[i].left,roomsInfo[i].right);
    }

    private void CameraPos(){
        Camera.main.transform.position = new Vector3(0,0,-15);
    }
}
