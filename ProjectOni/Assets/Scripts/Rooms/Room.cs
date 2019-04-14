using UnityEngine;

public enum RoomsTypes{
    LR= 0,
    LRD,
    LRU,
    LRUD,
    L,
    R,
    U,
    D,
    Count,
};

public enum RoomBehaviour{
    Start = 0,
    Exit,
    Boss,
    Normal,
    Count,
};

public class Room: MonoBehaviour{
    public RoomsTypes rType;
    public bool left, right, up, down;
    public Directions dir;
    public float roomTam;
    public GameObject room;
    public RoomBehaviour rBehaviour = RoomBehaviour.Normal;

    private void Awake() {
        rBehaviour = RoomBehaviour.Normal;
    }
}
