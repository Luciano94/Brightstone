using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("One Exit")]
    [SerializeField]private GameObject[] nodeL;
    [SerializeField]private GameObject[] nodeR;
    [SerializeField]private GameObject[] nodeU;
    [SerializeField]private GameObject[] nodeD;
    [SerializeField]private GameObject[] nodeHUB;


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
    [SerializeField]private GameObject[] nodeUDL;
    [SerializeField]private GameObject[] nodeUDR;
    [Header("Four Exit")]
    [SerializeField]private GameObject[] nodeLRUD;
    private static PoolManager instance;

    public static PoolManager Instance {
        get {
            instance = FindObjectOfType<PoolManager>();
            if(instance == null) {
                GameObject go = new GameObject("PoolManager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    public GameObject DrawHUBNode(Vector3 pos, List<Exit> exits){
        GameObject go = Instantiate(nodeHUB[Random.Range(0,nodeHUB.Length)], pos, Quaternion.identity);
        return go;
    }


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
            case NodeType.UDR:
                node = nodeUDR;
            break;
            case NodeType.UDL:
                node = nodeUDL;
            break;
            default:
                node = nodeLRUD;
            break;
        }
        GameObject go = Instantiate(node[Random.Range(0,node.Length)], pos, Quaternion.identity);
       // go.GetComponent<NodeExits>().SetExits = exits;
        return go;
    }
}
