using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SnakeHead : MonoBehaviour{
    [SerializeField]Transform[] referencePoints;
    float nodeSize = 20;

    private void Awake() {
        Vector3 pos = transform.position;
        pos.y += nodeSize;
        referencePoints[0].position = pos;
        pos = transform.position;
        pos.y -= nodeSize;
        referencePoints[1].position = pos;
        pos = transform.position;
        pos.x += nodeSize;
        referencePoints[2].position = pos;
        pos = transform.position;
        pos.x -= nodeSize;
        referencePoints[3].position = pos;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)){
            SceneManager.LoadScene(0);
        }   
    }

    public void NodeCollides(List<Node> nodes, ref bool[] exits){
        Debug.Log(nodes.Count);
        for (int j = 0; j < nodes.Count; j++){
            if(nodes[j].Position != transform.position)
                for (int i = 0; i < referencePoints.Length; i++){
                    Debug.Log(nodes[j].Position +" "+ (referencePoints[i].position + transform.position));
                    if(nodes[j].Position == (referencePoints[i].position + transform.position)){
                        exits[i] = true;
                    }
                } 
        }
        Debug.Log(exits[0] + "\n" +exits[1] + "\n" +exits[2] + "\n" +exits[3]);
    }
}
