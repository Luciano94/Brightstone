using UnityEngine;

public class CameraFollow: MonoBehaviour{

	[SerializeField]private float nodeSize = 40;
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
		offsetX = nodeSize;
		offsetY = nodeSize + 3;
		position = new Vector2(0,0);

        // Calculations assume map is position at the origin
		minX = horzExtent - offsetX * 0.5f;
		maxX = offsetX * 0.5f - horzExtent;
		minY = vertExtent - offsetY * 0.5f;
		maxY = offsetY * 0.5f - vertExtent;

    }

	public void ResetXY(Vector3 pos){
		if(position.x == pos.x){
			nodeSize = position.y - pos.y; 
		}else{
			nodeSize = position.x - pos.x;
		}
		if(nodeSize < 0){
			nodeSize*=-1;
		}
		position = pos;
		if(minX > pos.x){
			minX -= nodeSize;
			maxX -= nodeSize;
		}else if(maxX < pos.x){
			minX += nodeSize;
			maxX += nodeSize;
		}
		if(minY > pos.y){
			minY -= nodeSize;
			maxY -= nodeSize;
		}else if(maxY < pos.y){
			minY += nodeSize;
			maxY += nodeSize;
		}
		
	}
     
    private void Update() {
			newPos =new Vector3(target.position.x,target.position.y, zCamera );
			newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
			newPos.y = Mathf.Clamp(newPos.y, minY, maxY);	
			transform.position = newPos;
    }

/*	[SerializeField]private Transform target;
	private Vector3 newPos;
	private float maxX;
	private float minX;
	private float maxY;
	private float minY;
	private float distZ;


	private void Start() {
		distZ = transform.position.z;
 		maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distZ)).x;
 		minX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distZ)).x;
		minX *=-1; 
		
 		minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distZ)).y;
		maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distZ)).y;
		minY *=-1;

		
		Debug.Log(minX+"	"+maxX+"	"+minY+"	"+maxY);
		Debug.Log(transform.position.x+"	"+transform.position.y+"	"+transform.position.z);
	}

	public void ResetXY(Vector3 pos){
 		maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distZ)).x + pos.x;
 		minX = (maxX * -1);
		
 		maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distZ)).y + pos.y;
		minY = (maxY * -1);

		Debug.Log(minX+"	"+maxX+"	"+minY+"	"+maxY);
		Debug.Log(pos.x+"	"+(pos.y -3)+"	"+pos.z);
	}

	void Update () {
		newPos =new Vector3(target.position.x,target.position.y, -15 );
		newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
		newPos.y = Mathf.Clamp(newPos.y, minY, maxY);	
		transform.position = newPos;
	}*/
}
