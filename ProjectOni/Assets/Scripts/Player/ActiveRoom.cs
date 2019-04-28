using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    private Transform activeRoom = null;

    private void OnTriggerEnter(Collider other) {
        if(activeRoom != null)
            ChangeLayer(0);
        
        activeRoom = other.gameObject.GetComponent<RoomReference>().thePadre;
        ChangeLayer(9);
        Debug.Log(activeRoom.gameObject.layer);
        Camera.main.GetComponent<CameraFollow>().MoveTo(activeRoom.position);
    }

    private void ChangeLayer(int layer){
        foreach (Transform child in activeRoom){
            if(child.gameObject.layer != layer)
                child.gameObject.layer = layer;
        }
    }
}
