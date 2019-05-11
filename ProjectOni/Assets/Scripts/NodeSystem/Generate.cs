using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour{

    int state = -1;
    List<Node> nodes;

    [Header("Logic Generation")]
    [SerializeField]int nodeSize = 20;
    [SerializeField]int nodeQuantity = 2;
    int nodeQ = 0;
    Vector3 nextPos;
    Vector3 nullVec = new Vector3(-1,-1,-1);

    [Header("Set Exits")]
    [SerializeField]SnakeHead head;
    private bool[] exits;

    private void Start() {
        nodes = new List<Node>();
        CreateNode();
        nextPos = head.transform.position;
        state = 0;
    }

    private void Update() {
        switch (state)
        {
            case 0:
                if(nodeQ < nodeQuantity){
                    nextPos = GetNewPosition();
                    if(nextPos != nullVec){
                        head.transform.position = nextPos;
                        CreateNode();
                        nodeQ ++;   
                    }
                    else{
                        int node = Random.Range(0, nodeQ);
                        head.transform.position = nodes[node].Position;
                    }
                }else{
                    state = 1;
                }
            break;
            case 1:
                SetExit();
                state = 2;
            break;
            case 2:
                Draw();
                state = -1;
            break;
        }
    }
#region StateOne
    private void CreateNode(){
       nodes.Add(new Node(head.transform.position, nodeSize)); 
    }

    private Vector3 GetNewPosition(){
        Vector3 pos= head.transform.position;
        GetAvailablePos();
        int newPos = Random.Range(0,4);
        Debug.Log(newPos);
        switch (newPos){
            case 0:
                if(exits[0]){
                    pos.x += nodeSize;
                    return pos;
                }
            break;
            case 1:
                if(exits[1]){
                    pos.x -= nodeSize;
                    return pos;
                }
            break;
            case 2:
                if(exits[2]){
                    pos.y += nodeSize;
                    return pos;
                }
            break;
            case 3:
                if(exits[3]){
                    pos.y -= nodeSize;
                    return pos;
                }
            break;
        }
        pos = nullVec;
        return pos;
    }

    private void GetAvailablePos(){
        Vector3 pos;
        exits = new bool[4]{false, false, false, false};
        for (int i = 0; i < 4; i++){
            pos = head.transform.position;
            switch (i){
                case 0:
                    pos.x += nodeSize;
                    exits[0] = canGo(pos);
                break;
                case 1:
                    pos.x -= nodeSize;
                    exits[1] = canGo(pos);
                break;
                case 2:
                    pos.y += nodeSize;
                    exits[2] = canGo(pos);
                break;
                case 3:
                    pos.y -= nodeSize;
                    exits[3] = canGo(pos);
                break;
            }
        }
    }

    private bool canGo(Vector3 pos){
        bool result = true;
        for (int i = 0; i < nodes.Count; i++){
            if(nodes[i].Position == pos)
                result = false;
        }
        return result;
    }
    
#endregion
#region StateTwo
    private void SetExit(){
        for (int i = 0; i < nodes.Count; i++){
            nodes[i].setExits(nodes);
        }
    }
#endregion
#region StateThree
    private void Draw(){
        for (int i = 0; i < nodes.Count; i++){
            nodes[i].setNode( DrawNodes.Instance.DrawExitsNode(nodes[i].NodeType, nodes[i].Position));
        }
    }
#endregion
}
