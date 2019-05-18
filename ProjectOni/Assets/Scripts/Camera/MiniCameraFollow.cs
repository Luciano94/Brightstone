using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCameraFollow : MonoBehaviour{

	[SerializeField]private float nodeSize;

	private void Start() {
		nodeSize /= 10;
	}

	void Update () {
        	transform.position = Camera.main.transform.position / nodeSize;
	}
}
