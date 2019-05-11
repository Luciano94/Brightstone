using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeExits : MonoBehaviour{
    [SerializeField]GameObject doorU;
    [SerializeField]GameObject doorD;
    [SerializeField]GameObject doorR;
    [SerializeField]GameObject doorL;

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
}
