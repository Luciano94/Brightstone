using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraFollow : MonoBehaviour{

	[SerializeField]private int nodeSize;
	[SerializeField]private Transform playerTracker;
	private Vector3 pos;

	private void Start() {
		nodeSize = 4;
	}

	void Update () {
		pos = Camera.main.transform.position / nodeSize;
        transform.position = Camera.main.transform.position / nodeSize;
		pos = GameManager.Instance.PlayerPos / nodeSize;
		playerTracker.position = pos;	
	}
}
