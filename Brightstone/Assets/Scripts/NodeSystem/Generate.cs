using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour{

    int state = -1;
    [SerializeField] GameObject[] preGeneratedRooms;
    private Node[] preGeneratedNodes;
    [SerializeField]List<Node> nodes;

    [Header("Logic Generation")]
    [SerializeField]Vector2Int nodeSize;
    [SerializeField]int nodeQuantity = 30;
    int nodeMult;
    int nodeQ = -1;
    int nodeBoss = 0;
    int nodeMarket = 0;
    Vector3 nextPos;
    Vector3 nullVec = new Vector3(-1,-1,-1);

    [Header("Set Exits")]
    [SerializeField]SnakeHead head;
    private bool[] exits;

    [Header("Node Atributes")]
    [SerializeField]private Color marketColor;
    [SerializeField]private Color bossColor;
    [SerializeField]private Color normalColor;
    [SerializeField]private Color miniBossColor;




    private void Start() {
       // nodeBoss = Random.Range(5,nodeQ);
        nodeMarket = Random.Range(0, nodeQ);
        //if(nodeBoss == nodeMarket)
        //    nodeBoss++;
        nodes = new List<Node>();
        CreateNode();
        nodeQ++;
        nextPos = head.transform.position;
        state = 0;
        //nodeMult = nodeSize / 10;
        nodeMult = 4;
        if(GameManager.Instance.isTutorial)
            PreChargeNodes();
    }


    private void PreChargeNodes(){
        preGeneratedNodes = new Node[preGeneratedRooms.Length];
        for (int i = 0; i < preGeneratedRooms.Length; i++){
            preGeneratedNodes[i] = new Node(preGeneratedRooms[i],preGeneratedRooms[i].transform.position,
                                            nodeSize);
            preGeneratedNodes[i].SetBehaviour(NodeBehaviour.Tutorial);
            if(i == preGeneratedRooms.Length-1)
                preGeneratedNodes[i].SetBehaviour(NodeBehaviour.Market);
        }

        for (int i = 0; i < preGeneratedNodes.Length; i++){   
            preGeneratedNodes[i].setTutorialExits(preGeneratedNodes);
        }

        for (int i = 0; i < preGeneratedRooms.Length; i++){
           // Debug.Log(preGeneratedNodes[i].ExitsDoors.Count);
            preGeneratedRooms[i].GetComponent<NodeExits>().SetExits = preGeneratedNodes[i].ExitsDoors;
            preGeneratedNodes[i].setNode(preGeneratedRooms[i]);
            GameObject mapNode = PoolManager.Instance.DrawExitsNode(preGeneratedNodes[i].NodeType,
                                    preGeneratedNodes[i].Position / nodeMult, preGeneratedNodes[i].ExitsDoors );
            RoomsBehaviour room = preGeneratedRooms[i].GetComponent<RoomsBehaviour>();
            room.SetMapNode(mapNode.GetComponent<RenderReference>().node, normalColor);
            if(i == preGeneratedNodes.Length-1)
                room.SetMapNode(mapNode.GetComponent<RenderReference>().node, marketColor);
        }
        GameManager.Instance.PlayerOn = true;
        UIManager.Instance.LoadingFinish();
    }

    private void Update() {
        if(!GameManager.Instance.isTutorial){
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
                    UIManager.Instance.LoadingFinish();
                    state = -1;
                break;
            }
        }
    }
#region StateOne
    private void CreateNode(){
        Node n = new Node(head.transform.position, nodeSize);
        if(nodeQ == nodeMarket){
            n.SetBehaviour(NodeBehaviour.Market);
        }else{
            //if(nodeQ == nodeBoss)
                //n.SetBehaviour(NodeBehaviour.Boss);
            //else
                n.SetBehaviour(NodeBehaviour.Normal);
        }
        nodes.Add(n); 
    }

    private Vector3 GetNewPosition(){
        Vector3 pos= head.transform.position;
        GetAvailablePos();
        int newPos = Random.Range(0,4);
        switch (newPos){
            case 0:
                if(exits[0]){
                    pos.x += nodeSize.x;
                    return pos;
                }
            break;
            case 1:
                if(exits[1]){
                    pos.x -= nodeSize.x;
                    return pos;
                }
            break;
            case 2:
                if(exits[2]){
                    pos.y += nodeSize.y;
                    return pos;
                }
            break;
            case 3:
                if(exits[3]){
                    pos.y -= nodeSize.y;
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
                    pos.x += nodeSize.x;
                    exits[0] = canGo(pos);
                break;
                case 1:
                    pos.x -= nodeSize.x;
                    exits[1] = canGo(pos);
                break;
                case 2:
                    pos.y += nodeSize.y;
                    exits[2] = canGo(pos);
                break;
                case 3:
                    pos.y -= nodeSize.y;
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
        bool haveBoss = false;
        for (int i = 0; i < nodes.Count; i++){
            nodes[i].setExits(nodes);
            if(i > 1 && nodes[i].getCantExits() == 1 && !haveBoss){
                nodes[i].SetBehaviour(NodeBehaviour.Boss);
                haveBoss = true;
            }
        }
        if(!haveBoss){
            nodes[nodes.Count-1].SetBehaviour(NodeBehaviour.Boss);
        }
    }
#endregion
#region StateThree
    private void Draw(){
        for (int i = 0; i < nodes.Count; i++){
            GameObject go =  DrawNodes.Instance.DrawExitsNode(nodes[i].NodeType, 
                                        nodes[i].Position, nodes[i].ExitsDoors);
            nodes[i].setNode(go);
            GameObject mapNode = PoolManager.Instance.DrawExitsNode(nodes[i].NodeType,
                                 nodes[i].Position / nodeMult, nodes[i].ExitsDoors );
            RoomsBehaviour room = go.GetComponent<RoomsBehaviour>();
            switch (room.NodeBehaviour)
            {
                case NodeBehaviour.Normal:
                    go.GetComponent<RoomsBehaviour>().SetMapNode(
                    mapNode.GetComponent<RenderReference>().node, 
                    normalColor);
                break;
                case NodeBehaviour.Boss:
                    go.GetComponent<RoomsBehaviour>().SetMapNode(
                    mapNode.GetComponent<RenderReference>().node, 
                    bossColor);
                break;
                case NodeBehaviour.Market:
                    go.GetComponent<RoomsBehaviour>().SetMapNode(
                    mapNode.GetComponent<RenderReference>().node, 
                    marketColor);
                break;
                case NodeBehaviour.MediumBoss:
                    go.GetComponent<RoomsBehaviour>().SetMapNode(
                    mapNode.GetComponent<RenderReference>().node, 
                    miniBossColor);
                break;
            }
        }
    }
#endregion
}
