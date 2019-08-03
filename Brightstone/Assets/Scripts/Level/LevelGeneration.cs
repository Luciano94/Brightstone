﻿using UnityEngine;

public enum Directions
{
    Left = 0,
    Right,
    Up,
    Down,
    Count
}

public class LevelGeneration : MonoBehaviour
{
    [SerializeField]private Transform startingPositions;
    [SerializeField]private Transform roomsParent;
    [SerializeField]private Transform backStreetsParent;
    [SerializeField]private int roomCant = 10;
    

    private PoolManager poolM;
    private Directions directions;
    private Directions previusDirection;
    private int value;
    //Main Level
    private Transform[] rooms;
    private Room[] roomsInfo;
    //Room Info
    private float roomTam;
    private bool stopGeneration=false;
    private bool stopCreate = true;
    private int roomNumber;
    
    public Transform[] Rooms{
        get{return rooms;}
    }

    public Room[] RoomsInfo{
        get{return roomsInfo;}
    }

    private static LevelGeneration instance;

    public static LevelGeneration Instance {
        get {
            instance = FindObjectOfType<LevelGeneration>();
            if(instance == null) {
                GameObject go = new GameObject("LevelGeneration");
                instance = go.AddComponent<LevelGeneration>();
            }
            return instance;
        }
    }

    void Start(){
        poolM = PoolManager.Instance;
        transform.position = startingPositions.position;

        /*create first room */
       // GameObject go = Instantiate(poolM.GetOneExitRoom(), transform.position, Quaternion.identity);
        //go.transform.parent = roomsParent;
        //directions = go.GetComponent<Room>().dir;
        previusDirection = oppositeDirection(directions);

        /*Init rooms and rooms info arrays */
        roomNumber = 0;
        rooms = new Transform[roomCant*3];
        roomsInfo = new Room[roomCant*3];

        /*fill the arrays whith the first room */
        //rooms[roomNumber] = go.transform;
        //roomsInfo[roomNumber]= go.GetComponent<Room>();
        roomTam = roomsInfo[roomNumber].roomTam;
        roomsInfo[roomNumber].RBehaviour = RoomBehaviour.Start;
        roomNumber++;
    }

    // Update is called once per frame
    void Update(){
        if(!stopGeneration){
            Move();
            Create();
        }
        if(!stopCreate){
            for (int i = 0; i < roomCant; i++){
                ResolvePositionOfBackStreet(i);
            }
            SetEspecialRooms();
            stopCreate = true;
            MapGeneration.Instance.StartGeneration = roomNumber;
        }
    }

    private void SetEspecialRooms(){
        int bossRoom = Random.Range(roomCant, roomNumber);
        int exitRoom = Random.Range(roomCant, roomNumber);
        roomsInfo[bossRoom].RBehaviour = RoomBehaviour.Boss;
        roomsInfo[exitRoom].RBehaviour = RoomBehaviour.Exit;        
    }

    private void Move(){
        Vector2 newPos;
        switch (directions){
            case Directions.Down:
                newPos = new Vector2(transform.position.x  , transform.position.y - roomTam);
                transform.position = newPos;
            break;
            case Directions.Left:
                newPos = new Vector2(transform.position.x - roomTam , transform.position.y);
                transform.position = newPos;
            break;
            case Directions.Right:
               newPos = new Vector2(transform.position.x + roomTam , transform.position.y);
                transform.position = newPos;
            break;
            case Directions.Up:
                newPos = new Vector2(transform.position.x  , transform.position.y + roomTam);
                transform.position = newPos;
            break;
        }
    }

    private void Create(){
        if(CanCreate(transform)){
            //GameObject go = Instantiate(poolM.GetRoomOfType(GetNextTypeOfRoom()), 
            //                            transform.position, Quaternion.identity);
            //rooms[roomNumber] = go.transform;
            //roomsInfo[roomNumber]= go.GetComponent<Room>();
            //roomTam = roomsInfo[roomNumber].roomTam;
            //go.transform.parent = roomsParent;
            GenerateLinks(roomsInfo[roomNumber],roomsInfo[roomNumber-1]);
        }
        if(roomNumber < roomCant){
            value = Random.Range(0,14);
            if(roomsInfo[roomNumber] != null){
                GetNextDirection();
                roomNumber++;
            }
        }
        if(roomNumber == roomCant){
            stopGeneration = true;
            stopCreate = false;
        } 
    }

    private void ResolvePositionOfBackStreet(int room){
        switch(roomsInfo[room].rType){
            case RoomsTypes.LR:
                if(!roomsInfo[room].left){
                    directions = Directions.Left;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].right){
                    directions = Directions.Right;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
            break;
            case RoomsTypes.LRD:
                if(!roomsInfo[room].left){
                    directions = Directions.Left;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].right){
                    directions = Directions.Right;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].down){
                    directions = Directions.Down;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
            break;
            case RoomsTypes.LRU:
                if(!roomsInfo[room].left){
                    directions = Directions.Left;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].right){
                    directions = Directions.Right;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].up){
                    directions = Directions.Up;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
            break;
            case RoomsTypes.LRUD:
                if(!roomsInfo[room].left){
                    directions = Directions.Left;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].right){
                    directions = Directions.Right;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].up){
                    directions = Directions.Up;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
                if(!roomsInfo[room].down){
                    directions = Directions.Down;
                    previusDirection = oppositeDirection(directions);
                    transform.position = rooms[room].position;
                    Move();
                    CreateBackStreet(roomsInfo[room]);
                }
            break;
        }
    }

    private void CreateBackStreet(Room prevRoom){
        if(CanCreate(transform)){
            //GameObject go = Instantiate(poolM.GetOneExitRoom(previusDirection), 
            //                            transform.position, Quaternion.identity);
            //rooms[roomNumber] = go.transform;
            //roomsInfo[roomNumber]= go.GetComponent<Room>();
            //roomTam = roomsInfo[roomNumber].roomTam;
            //go.transform.parent = backStreetsParent;
            GenerateLinks(roomsInfo[roomNumber],prevRoom);
            roomNumber++;
        }
    }

    private RoomsTypes GetNextTypeOfRoom(){
        int value = Random.Range(0,10);
        switch (previusDirection){
            case Directions.Down:
                    if(value < 4)
                        return RoomsTypes.LRD;
                    else
                        return RoomsTypes.LRUD;
            case Directions.Up:
                    if(value < 4)
                        return RoomsTypes.LRU;
                    else
                        return RoomsTypes.LRUD;
            case Directions.Left:
                    if(value < 4)
                        return RoomsTypes.LRD;
                    else
                        return RoomsTypes.LRU;
            case Directions.Right:
                if(value < 4)
                    return RoomsTypes.LRU;
                else
                    return RoomsTypes.LRD;
        }
        return RoomsTypes.LRUD;
    }
    
    private void GetNextDirection(){ 
        switch (roomsInfo[roomNumber].rType){
            case RoomsTypes.LR:
                switch (previusDirection){
                    case Directions.Left:
                        directions = Directions.Right;
                    break;
                    case Directions.Right:
                        directions = Directions.Left;
                    break;
                }
            break;
            case RoomsTypes.LRD:
                switch (previusDirection){
                    case Directions.Left:
                        if(value < 15)
                            directions = Directions.Down;
                        else
                            directions = Directions.Right;
                    break;
                    case Directions.Right:
                        if(value < 15)
                            directions = Directions.Left;
                        else
                            directions = Directions.Down;
                    break;
                    case Directions.Down:
                        if(value < 15)
                            directions = Directions.Left;
                        else
                            directions = Directions.Right;
                    break;
                }
            break;
            case RoomsTypes.LRU:
                switch (previusDirection){
                    case Directions.Left:
                        if(value < 15)
                            directions = Directions.Up;
                        else
                            directions = Directions.Right;
                    break;
                    case Directions.Right:
                        if(value < 15)
                            directions = Directions.Up;
                        else
                            directions = Directions.Left;
                    break;
                    case Directions.Up:
                        if(value < 15)
                            directions = Directions.Left;
                        else
                            directions = Directions.Right;
                    break;
                }
            break;
            case RoomsTypes.LRUD:
                switch (previusDirection){
                    case Directions.Left:
                        if(value < 10)
                            directions = Directions.Down;
                        else if(value < 20)
                            directions = Directions.Up;
                        else
                            directions = Directions.Right;
                    break;
                    case Directions.Right:
                        if(value < 10)
                            directions = Directions.Down;
                        else if(value < 20)
                            directions = Directions.Up;
                        else
                            directions = Directions.Left;
                    break;
                    case Directions.Up:
                        if(value < 10)
                            directions = Directions.Down;
                        else if(value < 20)
                            directions = Directions.Right;
                        else
                            directions = Directions.Left;
                    break;
                    case Directions.Down:
                        if(value < 10)
                            directions = Directions.Left;
                        else if(value < 20)
                            directions = Directions.Right;
                        else
                            directions = Directions.Up;
                    break;
                }
            break;
        }
        previusDirection = oppositeDirection(directions);
    }

    private Directions oppositeDirection(Directions dir){
        switch (dir){
            case Directions.Down:
                return Directions.Up;
            case Directions.Left:
                return Directions.Right;
            case Directions.Right:
                return Directions.Left;
            case Directions.Up:
                return Directions.Down;
        }
        return Directions.Count;
    }


    private bool CanCreate(Transform tr){
        for (int i = 0; i < roomNumber; i++){
            if(rooms[i].position == tr.position){
                return false;
            }
        }
        return true;
    }

    private void GenerateLinks(Room actRoom, Room prevRoom){
        switch(previusDirection){
            case Directions.Down:
                actRoom.down = true;
            break;
            case Directions.Up:
                actRoom.up = true;
            break;
            case Directions.Left:
                actRoom.left = true;
            break;
            case Directions.Right:
                actRoom.right = true;
            break;
        }
        switch(directions){
            case Directions.Down:
                prevRoom.down = true;
            break;
            case Directions.Up:
                prevRoom.up = true;
            break;
            case Directions.Left:
                prevRoom.left = true;
            break;
            case Directions.Right:
                prevRoom.right = true;
            break;
        }
    }
}