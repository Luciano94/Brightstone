using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNodes : MonoBehaviour{
    
    private static DrawNodes instance;

    public static DrawNodes Instance {
        get {
            instance = FindObjectOfType<DrawNodes>();
            if(instance == null) {
                GameObject go = new GameObject("DrawNodes");
                instance = go.AddComponent<DrawNodes>();
            }
            return instance;
        }
    }

    [Header("One Exit")]
    [SerializeField]private GameObject[] nodeL;
    [SerializeField]private GameObject[] nodeR;
    [SerializeField]private GameObject[] nodeU;
    [SerializeField]private GameObject[] nodeD;
    [Header("Two Exit")]
    [SerializeField]private GameObject[] nodeLR;
    [SerializeField]private GameObject[] nodeUD;
    [SerializeField]private GameObject[] nodeUL;
    [SerializeField]private GameObject[] nodeUR;
    [SerializeField]private GameObject[] nodeDR;
    [SerializeField]private GameObject[] nodeDL;
    [Header("Three Exit")]
    [SerializeField]private GameObject[] nodeLRD;
    [SerializeField]private GameObject[] nodeLRU;
    [Header("Four Exit")]
    [SerializeField]private GameObject[] nodeLRUD;



    public GameObject DrawExitsNode(NodeType nType, Vector3 pos, List<Exit> exits){
        GameObject[] node;
        switch (nType)
        {
            case NodeType.U:
                node = nodeU;
            break;
            case NodeType.D:
                node = nodeD;
            break;
            case NodeType.L:
                node = nodeL;
            break;
            case NodeType.R:
                node = nodeR;
            break;
            case NodeType.LR:
                node = nodeLR;
            break;
            case NodeType.UD:
                node = nodeUD;
            break;
            case NodeType.UL:
                node = nodeUL;
            break;
            case NodeType.UR:
                node = nodeUR;
            break;
            case NodeType.DR:
                node = nodeDR;
            break;
            case NodeType.DL:
                node = nodeDL;
            break;
            case NodeType.LRU:
                node = nodeLRU;
            break;
            case NodeType.LRD:
                node = nodeLRD;
            break;
            default:
                node = nodeLRUD;
            break;
        }
        GameObject go = Instantiate(node[Random.Range(0,node.Length)], pos, Quaternion.identity);
        go.GetComponent<NodeExits>().SetExits = exits;
        return go;
    }
}
