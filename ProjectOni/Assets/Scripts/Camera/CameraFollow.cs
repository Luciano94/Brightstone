using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{

    [SerializeField]Camera miniMap;
    private float speed = 100; 
	private Vector3 initPos;
	private Vector3 targetPos;
	private float accum;
    private bool needMove = false;

    public void MoveTo(Vector3 pos){
        targetPos = new Vector3 (pos.x, pos.y , transform.position.z);
        initPos = transform.position;
        accum = 0.0f;
    }

	void Update () {
            targetPos = GameManager.Instance.PlayerPos;
            accum += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(initPos, targetPos, accum);
            //miniMap.transform.position = Vector3.Lerp(initPos/2, targetPos/2, accum);
	}
}
