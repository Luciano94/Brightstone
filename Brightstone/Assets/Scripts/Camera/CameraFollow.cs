using UnityEngine;

public class CameraFollow: MonoBehaviour{

	[SerializeField]private Vector2 nodeSize;
	[SerializeField]private Transform target;
	[SerializeField]private float zCamera = -10;

	private Vector3 newPos;
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;
	private float distZ;
	private float vertExtent;
	private float horzExtent;
	private float offsetX;
	private float offsetY;
	private Vector2 position;
     
    private void Start() {
        vertExtent = Camera.main.orthographicSize;    
        horzExtent = vertExtent * Screen.width / Screen.height;
		offsetX = nodeSize.x;
		offsetY = nodeSize.y + 3;
		position = new Vector2(0,0);

        // Calculations assume map is position at the origin
		minX = horzExtent - offsetX * 0.5f;
		maxX = offsetX * 0.5f - horzExtent;
		minY = vertExtent - offsetY * 0.5f;
		maxY = offsetY * 0.5f - vertExtent;

    }

	public void ResetXY(Vector3 pos){
		transform.position = new Vector3(pos.x, pos.y, zCamera);
	}
}
