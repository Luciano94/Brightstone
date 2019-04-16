using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    [SerializeField]private GameObject player;

    private void Start() {
        player.transform.position = new Vector3(transform.position.x, transform.position.y,
                                                player.transform.position.z);
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                                        transform.position.z);;
    }
}
