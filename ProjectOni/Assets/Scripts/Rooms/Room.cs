using UnityEngine;

public enum RoomsTypes
{
    Boss= 0,
    Normal,
    Start,
    Exit,
    Count,
};
public class Room: MonoBehaviour
{
    public RoomsTypes rType;
    public bool left, right, up, down;
    public float roomTam;
    public GameObject room;

    private void Start() {

    }

    public void AssignRoom(RoomsTypes _rType, bool _left, bool _right, bool _up , bool _down, GameObject _room){
        rType = _rType;
        left = _left;
        right = _right;
        up = _up;
        down = _down;
        room = _room;
    }
}
