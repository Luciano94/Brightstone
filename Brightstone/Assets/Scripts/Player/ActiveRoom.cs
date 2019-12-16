using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private float distToTP;
    private Transform activeRoom = null;
    private RoomsBehaviour roomsBehaviour;
    private NodeExits doorManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 15 && other.name == "Floor"){
            if(activeRoom != null){
                if(!roomsBehaviour.HaveMarket && !roomsBehaviour.HaveBoss)
                    roomsBehaviour.SetColorNode(normalColor);
                ChangeLayer(15);
            }
            activeRoom = other.gameObject.GetComponent<RoomReference>().thePadre;
            roomsBehaviour = activeRoom.GetComponent<RoomsBehaviour>();

            UpdatePosition();
            
            doorManager = activeRoom.GetComponent<NodeExits>();
            cameraFollow.ResetXY(activeRoom.transform.position);
            if(roomsBehaviour.NodeBehaviour == NodeBehaviour.Tutorial){
                HandleTutorialRooms(other);
            }else{
                HandleNormalRooms(other);
            }

            if(!roomsBehaviour.Complete){
                doorManager.CloseDoors();
                
                if(roomsBehaviour.NodeBehaviour == NodeBehaviour.Boss){
                    SoundManager.Instance.RoomBossEnter();
                } else {
                    SoundManager.Instance.RoomNewEnter();
                }
            }
        }
    }

    private void UpdatePosition(){

        Axis4Direction dir = Calculations.Get4AxisDirection(roomsBehaviour.transform.position - transform.position);

        switch (dir)
        {
            case Axis4Direction.Up:
                transform.Translate(0.0f, distToTP + 0.5f, 0.0f);
            break;
            
            case Axis4Direction.Right:
                transform.Translate(distToTP, 0.0f, 0.0f);
            break;

            case Axis4Direction.Down:
                transform.Translate(0.0f, -distToTP - 0.5f, 0.0f);
            break;

            case Axis4Direction.Left:
                transform.Translate(-distToTP, 0.0f, 0.0f);
            break;
        }
    }

    private void HandleNormalRooms(Collider2D other){
        if(!roomsBehaviour.HaveBoss && !roomsBehaviour.HaveMarket)
            roomsBehaviour.SetColorNode(activeColor);
        if(!roomsBehaviour.Complete){
            RunSaver.currentRun.data.roomsDiscovered++;
            roomsBehaviour.setEnemiesRoom();
            roomsBehaviour.ActiveEnemies();
        }

        ChangeLayer(9);
    }

    private void HandleTutorialRooms(Collider2D other){
        if(!roomsBehaviour.HaveBoss && !roomsBehaviour.HaveMarket)
            roomsBehaviour.SetColorNode(activeColor);
            ChangeLayer(9);
    }

    private void ChangeLayer(int layer){
        foreach (Transform child in activeRoom){
            child.gameObject.layer = layer;
            ChangeChildLayer(child, layer);
        }
    }

    private void ChangeChildLayer(Transform child, int layer){
        child.gameObject.layer = layer;

        for (int i = 0; i < child.transform.childCount; i++)
            ChangeChildLayer(child.transform.GetChild(i), layer);
    }

    public NodeExits GetNodeExits(){
        return doorManager;
    }

    public RoomsBehaviour GetRoomsBehaviour(){
        return roomsBehaviour;
    }
}
