using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]private GameObject levelBoss;

    private void Awake() {
        if(!PlayerPrefs.HasKey("XP")){
            PlayerPrefs.SetFloat("XP",playerSts.Experience);
        }else{
            playerSts.SetExperience = PlayerPrefs.GetFloat("XP", 0);
        }
    }

    public Vector3 PlayerPos{
        get{return player.transform.position;}
    }

    public PlayerStats playerSts{
        get{return player.GetComponent<PlayerStats>();}
    }

    public GameObject SetBoss{
        set{levelBoss = value;
            UIManager.Instance.DesactiveBoss();
            }
    }
    public BossStats bossSts{
        get{return levelBoss.GetComponent<BossStats>();}
    }

    public bool PlayerIsParry{
        get{ return player.GetComponent<PlayerCombat>().isParry; }
    }
    
    public bool PlayerIsAttack{
        get{ return player.GetComponent<PlayerCombat>().isAttack; }
    }

    public void PlayerDeath(){
        PlayerPrefs.SetFloat("XP", playerSts.Death());
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void PLayerWin(){
        PlayerPrefs.SetFloat("XP", playerSts.Experience);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
