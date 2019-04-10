using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Up = 0,
    Down,
    Left,
    Right,
    Count
}

public class LevelGen2 : MonoBehaviour
{
    [SerializeField]private Transform[] startingPositions;
    [SerializeField]private GameObject[] rooms;
    [SerializeField]private int roomTam = 10;
    [SerializeField]private float roomCant = 10;
    private Directions directions;
    private Directions previusDirection;
    private List<Transform> lRooms;
    private bool stopGeneration=false; 
    void Start()
    {
        int randStartPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;
        GameObject go = Instantiate(rooms[0], transform.position, Quaternion.identity);
        previusDirection = Directions.Count;
        directions = (Directions)Random.Range(0,(int)Directions.Count);
        lRooms = new List<Transform>();
        lRooms.Add(go.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopGeneration){
            if(roomCant > 0){
                Move();
                roomCant--;
            }
        }
    }

    private void GetNextDirection(){
        switch (previusDirection)
        {
            case Directions.Count:
                directions = (Directions)Random.Range(0,(int)Directions.Count); 
            break;
            case Directions.Down:
                directions = (Directions)Random.Range(0,(int)Directions.Count); 
                if(directions == Directions.Down)
                    directions = Directions.Up;
            break;
            case Directions.Left:
                directions = (Directions)Random.Range(0,(int)Directions.Count); 
                if(directions == Directions.Left)
                    directions = Directions.Right;
            break;
            case Directions.Right:
                directions = (Directions)Random.Range(0,(int)Directions.Count); 
                if(directions == Directions.Right)
                    directions = Directions.Left;
            break;
            case Directions.Up:
                directions = (Directions)Random.Range(0,(int)Directions.Count); 
                if(directions == Directions.Up)
                    directions = Directions.Down;
            break;
        }
        switch (directions)
        {
            case Directions.Down:
                previusDirection = Directions.Up;
            break;
            case Directions.Left:
                previusDirection = Directions.Right;
            break;
            case Directions.Right:
                previusDirection = Directions.Left;
            break;
            case Directions.Up:
                previusDirection = Directions.Down;
            break;
        }
    }
    private void Move(){
        Vector2 newPos;
        switch (directions)
        {
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

        if(!stopGeneration){
            GetNextDirection();
            if(CanCreate(transform)){
                GameObject go = Instantiate(rooms[0], transform.position, Quaternion.identity);
                lRooms.Add(go.transform);
            }
        }
    }

    private bool CanCreate(Transform tr){
        foreach (Transform t in lRooms)
        {
            if(t.position == tr.position)
                return false;
        }
        return true;
    }
}
