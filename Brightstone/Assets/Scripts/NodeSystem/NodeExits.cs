using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeExits : MonoBehaviour{
    [SerializeField]GameObject doorU;
    [SerializeField]GameObject doorD;
    [SerializeField]GameObject doorR;
    [SerializeField]GameObject doorL;
    [SerializeField]private List<Animator> doorAnimators;
    private List<Exit> exits; 

    public BoxCollider2D GetDoor(Direction dir){
        switch (dir)
        {
            case Direction.Up:
                return doorU.GetComponent<BoxCollider2D>();
            case Direction.Down:
                return doorD.GetComponent<BoxCollider2D>();
            case Direction.Right:
                return doorR.GetComponent<BoxCollider2D>();
            default:
                return doorL.GetComponent<BoxCollider2D>();
        }
    }

    public List<Exit> SetExits{
        set{exits = value;}
    }

    public void OpenDoors(){
        for (int i = 0; i < exits.Count; i++){
            exits[i].OpenCloseDoor(false);
            doorAnimators[i].SetTrigger("OpenTrigger");
        }
    }

    public void CloseDoors(){
        for (int i = 0; i < exits.Count; i++){
            exits[i].OpenCloseDoor(true);
            doorAnimators[i].SetTrigger("CloseTrigger");
        }
    }
    
}
