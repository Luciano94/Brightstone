using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField]private Transform[] startingPositions;
    [SerializeField]private Transform[] exitPositions;
    [SerializeField]private GameObject[] rooms;
    [SerializeField]private Transform parent;
    [SerializeField]private int levelTam = 4;
    private Room[,] levelGrid;
    private Vector2 startRoom;
    private Vector2 exitRoom;
    private int direction;
    [SerializeField]private int moveAmount=10;
    private float timeBtwRoom = 0.0f;
    [SerializeField]private float startTimeBtwRoom = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        //create lvl grid
        levelGrid = new Room[levelTam,levelTam];
        
        //take exit room position
        int randExitPos = Random.Range(0, exitPositions.Length);
        //instanciate the exit room
        int randRoom = Random.Range(0, rooms.Length);
        GameObject go = Instantiate(rooms[randRoom], exitPositions[randExitPos].position, Quaternion.identity);
        go.transform.parent = parent; 
        //Fill exit room in the level grid;
        Room room = go.GetComponent<Room>();
        room.rType = RoomsTypes.Exit;
        levelGrid[randExitPos, levelTam-1] = room;
        exitRoom = new Vector2(randExitPos,levelTam-1);
        
        //take Start room position
        int randStartPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position; 
        //instanciate the start room
        randRoom = Random.Range(0, rooms.Length);
        go = Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
        go.transform.parent = parent;
        //Fill start room in the level grid
        room = go.GetComponent<Room>();
        room.rType = RoomsTypes.Start;
        levelGrid[randStartPos,0] = room;
        startRoom = new Vector2(randStartPos,0);
        
        //move the camera
        Vector3 cameraPos = new Vector3(0,0,-10) + transform.position; 
        Camera.main.transform.position = cameraPos;
        CreatePath();
    }

    private void CreatePath(){
        Debug.Log(startRoom.x);
        int acum = moveAmount;
        for (int i = (int)startRoom.x; i > 0; i--)
        {
            float x = levelGrid[(int)startRoom.x,(int)startRoom.y].room.transform.position.x;
            x -= acum;
            float y = levelGrid[(int)startRoom.x,(int)startRoom.y].room.transform.position.y;
            acum += moveAmount;
            CreateRoom(x,y,0, i);
        }
    }

    private void CreateRoom(float x, float y, int lvlX, int lvlY)
    {
        //instanciate the room
        int randRoom = Random.Range(0, rooms.Length);
        GameObject go = Instantiate(rooms[randRoom], new Vector3(x,y,0), Quaternion.identity);
        go.transform.parent = parent;
        //Fill start room in the level grid
        Room room = go.GetComponent<Room>();
        room.rType = RoomsTypes.Normal;
        levelGrid[lvlX,lvlY] = room;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwRoom <= 0.0f)
        {
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
