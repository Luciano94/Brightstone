using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeBehaviour{
    Normal,
    Boss,
    MediumBoss,
    Market,
}

public enum NodeType{
    L,
    R,
    U,
    D,
    LR,
    UD,
    UL,
    UR,
    DR,
    DL,
    LRD,
    LRU,
    UDL,
    UDR,
    LRUD,
}

public class Node{
    List<Exit> exits;
    int nodeSize;
    bool[] exitsTakes;
    Vector3 position;
    Vector3[] references;
    NodeBehaviour nBehaviour;
    GameObject node;
    NodeType nType;

    public Vector3 Position{
        get{return position;}
    }

    public NodeBehaviour Behaviour{
        get{return nBehaviour;}
    }

    public Node(Vector3 pos, int nSize){
        position = pos;
        nodeSize = nSize;
        CreateReference();
        exits = new List<Exit>();
        nBehaviour = NodeBehaviour.Normal;
    }

    private void CreateReference(){
        Vector3 pos = position;
        references = new Vector3[4];
        pos.y += nodeSize;
        references[0] = pos;
        pos = position;
        pos.y -= nodeSize;
        references[1] = pos;
        pos = position;
        pos.x += nodeSize;
        references[2] = pos;
        pos = position;
        pos.x -= nodeSize;
        references[3] = pos;
    }

    public bool[] Exits{
        get{return exitsTakes;}
    }

    public List<Exit> ExitsDoors{
        get{return exits;}
    }

    public NodeType NodeType{
        get{return nType;}
    }

    public void setExits(List<Node> nodes){
        exitsTakes = new bool[4]{false,false,false,false};
        for (int i = 0; i < references.Length; i++){
            for (int j = 0; j < nodes.Count; j++){
               if(references[i] == nodes[j].Position){
                   exitsTakes[i] = true;
                   exits.Add(new Exit((Direction)i));
               } 
            }
        }
        setType();
    }

    private void setType(){
        if(exits.Count == 1){
            DefineOneExitNode(exits[0].Pos);
        }
        if(exits.Count == 2){
            DefineTwoExitNode(exits[0].Pos, exits[1].Pos);
        }
        if(exits.Count == 3){
            DefineThreeExitNode(exits[0].Pos, exits[1].Pos, exits[2].Pos);
        }
        if(exits.Count == 4){
            nType = NodeType.LRUD;
        }
    }

    public void setNode(GameObject go){
        node = go;
        go.GetComponent<RoomsBehaviour>().Node = this;
        for (int i = 0; i < exits.Count; i++){
            exits[i].SetDoor(node.GetComponent<NodeExits>().GetDoor(exits[i].Pos));
            exits[i].OpenCloseDoor(false);
        }
    }

        private void DefineOneExitNode(Direction dir){
        switch (dir){
            case Direction.Up:
                nType = NodeType.U;
            break;
            case Direction.Down:
                nType = NodeType.D;
            break;
            case Direction.Left:
                nType = NodeType.L;
            break;
            default:
                nType = NodeType.R;
            break;
        }
    }

    private void DefineTwoExitNode(Direction dir1, Direction dir2){
        if(dir1 == Direction.Up){
            switch (dir2)
            {
                case Direction.Left:
                nType = NodeType.UL;
            break;
                case Direction.Right:
                nType = NodeType.UR;
            break;
                case Direction.Down:
                nType = NodeType.UD;
            break;
            }
        }
        if(dir1 == Direction.Down){
            switch (dir2)
            {
                case Direction.Left:
                    nType = NodeType.DL;
                break;
                case Direction.Right:
                    nType = NodeType.DR;
                break;
            }
        }
        if(dir1 == Direction.Right&& dir2 == Direction.Left)
            nType = NodeType.LR;
    }

    private void DefineThreeExitNode(Direction dir1, Direction dir2, Direction dir3){
        if(dir2 == Direction.Right){
            if(dir1 == Direction.Down){
                nType = NodeType.LRD;
            }else{
                nType = NodeType.LRU;
            }
        }else if(dir2 == Direction.Down){
            if(dir3 == Direction.Left){
                nType = NodeType.UDL;
            }else{
                nType = NodeType.UDR;
            }
        }
    }

    public bool SetBehaviour(NodeBehaviour type){
        switch (type){
            case NodeBehaviour.Boss:
                nBehaviour = NodeBehaviour.Boss;
                return true;
            case NodeBehaviour.MediumBoss:
                nBehaviour = NodeBehaviour.MediumBoss;
                return true;
            case NodeBehaviour.Market:
                nBehaviour = NodeBehaviour.Market;
                return true;
        }
        return false;
    }
}
