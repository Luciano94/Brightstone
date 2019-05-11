using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeExits : MonoBehaviour{
    [SerializeField]GameObject doorU;
    [SerializeField]GameObject doorD;
    [SerializeField]GameObject doorR;
    [SerializeField]GameObject doorL;
    private List<Exit> exits; 

    public GameObject GetDoor(Direction dir){
        switch (dir)
        {
            case Direction.Up:
                return doorU;
            case Direction.Down:
                return doorD;
            case Direction.Right:
                return doorR;
            default:
                return doorL;
        }
    }

    public List<Exit> SetExits{
        set{exits = value;}
    }

    public void OpenDoors(){
        for (int i = 0; i < exits.Count; i++){
            exits[i].OpenCloseDoor(false);
        }
    }

    public void CloseDoors(){
        for (int i = 0; i < exits.Count; i++){
            exits[i].OpenCloseDoor(true);
        }
    }
    
}
