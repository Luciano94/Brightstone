using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //POOLS
    [SerializeField]private GameObject[] roomsLR;
    [SerializeField]private GameObject[] roomsLRD;
    [SerializeField]private GameObject[] roomsLRU;
    [SerializeField]private GameObject[] roomsLRUD;
    [SerializeField]private GameObject[] roomsL;
    [SerializeField]private GameObject[] roomsR;
    [SerializeField]private GameObject[] roomsU;
    [SerializeField]private GameObject[] roomsD;

    private static PoolManager instance;

    public static PoolManager Instance {
        get {
            instance = FindObjectOfType<PoolManager>();
            if(instance == null) {
                GameObject go = new GameObject("PoolManager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    private GameObject[][] poolOfRooms;

    private void Start() {
        poolOfRooms = new GameObject[(int)RoomsTypes.Count][];
        poolOfRooms[(int)RoomsTypes.LR] =  roomsLR;
        poolOfRooms[(int)RoomsTypes.LRD] =  roomsLRD;
        poolOfRooms[(int)RoomsTypes.LRU] =  roomsLRU;
        poolOfRooms[(int)RoomsTypes.LRUD] =  roomsLRUD;
        poolOfRooms[(int)RoomsTypes.L] =  roomsL;
        poolOfRooms[(int)RoomsTypes.R] =  roomsR;
        poolOfRooms[(int)RoomsTypes.U] =  roomsU;
        poolOfRooms[(int)RoomsTypes.D] =  roomsD;
    }

    public GameObject GetRoomOfType(RoomsTypes type){
        int value = Random.Range(0, poolOfRooms[(int)type].Length);
        return poolOfRooms[(int)type][value];
    }

    public GameObject GetOneExitRoom(){
        int roomType = Random.Range((int)RoomsTypes.L, (int)RoomsTypes.D);
        int room = Random.Range(0, poolOfRooms[roomType].Length);
        return poolOfRooms[roomType][room];
    }

    public GameObject GetOneExitRoom(Directions dir){
        int roomType = (int)dir + 4;
        int room = Random.Range(0, poolOfRooms[roomType].Length);
        return poolOfRooms[roomType][room];
    }
}
