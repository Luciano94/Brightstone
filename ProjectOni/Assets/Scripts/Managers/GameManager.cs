using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance {
        get {
            instance = FindObjectOfType<GameManager>();
            if(instance == null) {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField]private GameObject player;

    public Vector3 PlayerPos{
        get{return player.transform.position;}
    }

    public PlayerStats playerSts{
        get{return player.GetComponent<PlayerStats>();}
    }
}
