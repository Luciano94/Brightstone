using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{ 
    Up = 0,
    Down,
    Right,
    Left,
}

public class Exit{
    private Direction pos;
    private bool active;
    private BoxCollider2D door;
    

    public bool DoorState{
        get{return active;}
    }

    public Direction Pos{
        get{return pos;}
    }

    public Exit(Direction _pos){
        pos = _pos;
    }

    public void SetDoor(BoxCollider2D dr){
        door = dr;
    }

    public void OpenCloseDoor(bool key){
        
        active = key;
        door.enabled = key;
        
    }

}
