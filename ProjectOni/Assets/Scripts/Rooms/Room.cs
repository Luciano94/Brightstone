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
    public SpriteRenderer room;
    private RoomBehaviour rBehaviour;

    private void Awake() {
        rBehaviour = RoomBehaviour.Normal;
    }

    public RoomBehaviour RBehaviour{
        set{
            if(value == RoomBehaviour.Exit)
                room.color = Color.red;
            if(value == RoomBehaviour.Boss)
                room.color = Color.green;
            if(value == RoomBehaviour.Start)
                room.color = Color.blue;
            rBehaviour = value;
        }
    }

    public void SetLinks(bool l,bool r,bool u,bool d){
        left = l;
        right = r;
        up = u;
        down = d;
    }

}
