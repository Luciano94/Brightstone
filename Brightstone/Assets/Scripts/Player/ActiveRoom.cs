using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    [SerializeField]private Color activeColor;
    [SerializeField]private Color normalColor;
    [SerializeField]private CameraFollow cameraFollow;
    private Transform activeRoom = null;
    private RoomsBehaviour roomsBehaviour;
    private NodeExits doorManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 15){
            if(activeRoom != null){
                if(!roomsBehaviour.HaveMarket && !roomsBehaviour.HaveBoss)
                    roomsBehaviour.SetColorNode(normalColor);
                ChangeLayer(15);
            }
            activeRoom = other.gameObject.GetComponent<RoomReference>().thePadre;
            roomsBehaviour = activeRoom.GetComponent<RoomsBehaviour>();

            if(!roomsBehaviour.Complete){
                if(roomsBehaviour.NodeBehaviour == NodeBehaviour.Boss){
                    SoundManager.Instance.RoomBossEnter();
                } else {
                    SoundManager.Instance.RoomNewEnter();
                }
            }

            doorManager = activeRoom.GetComponent<NodeExits>();
            cameraFollow.ResetXY(activeRoom.transform.position);
            if(roomsBehaviour.NodeBehaviour == NodeBehaviour.Tutorial){
                HandleTutorialRooms(other);
            }else{
                HandleNormalRooms(other);
            }
        }
    }

    private void HandleNormalRooms(Collider2D other){
        if(!roomsBehaviour.HaveBoss && !roomsBehaviour.HaveMarket)
            roomsBehaviour.SetColorNode(activeColor);
        if(!roomsBehaviour.Complete){
            RunSaver.currentRun.data.roomsDiscovered++;
            roomsBehaviour.setEnemiesRoom();
            roomsBehaviour.ActiveEnemies();
            doorManager.CloseDoors();
        }else{
            doorManager.OpenDoors();
        }
        ChangeLayer(9);
    }

    private void HandleTutorialRooms(Collider2D other){
        if(!roomsBehaviour.HaveBoss && !roomsBehaviour.HaveMarket)
            roomsBehaviour.SetColorNode(activeColor);
            //doorManager.CloseDoors();
            ChangeLayer(9);
    }

    private void ChangeLayer(int layer){
        foreach (Transform child in activeRoom){
            if(child.gameObject.layer != layer)
                child.gameObject.layer = layer;
                foreach (Transform childofChild in child){
                    if(childofChild.gameObject.layer != layer)
                        childofChild.gameObject.layer = layer;
                }
        }
        if(roomsBehaviour.HaveMarket){
            roomsBehaviour.SwitchMarket();
        }
    }

    public NodeExits GetNodeExits(){
        return doorManager;
    }

    public RoomsBehaviour GetRoomsBehaviour(){
        return roomsBehaviour;
    }
}
