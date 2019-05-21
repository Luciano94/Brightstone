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
    private Vector3 weaponRotation;
    [SerializeField]private GameObject levelBoss;

    private const int mainMenuIndex = 0;

    private void Awake() {
        if(!PlayerPrefs.HasKey("XP")){
            PlayerPrefs.SetInt("XP",(int)playerSts.Experience);
        }else{
            playerSts.SetExperience = PlayerPrefs.GetInt("XP", 0);
        }
    }

    public void ExitToMainMenu(){
        SceneManager.LoadScene(mainMenuIndex);
    }

    public Vector3 PlayerPos{
        get{return player.transform.position;}
    }

    public PlayerStats playerSts{
        get{return player.GetComponent<PlayerStats>();}
    }

    public PlayerCombat playerCombat{
        get{return player.GetComponent<PlayerCombat>();}
    }

    public void SetEnemyHitFrom(Vector3 enemyPos){
        player.GetComponent<PlayerMovement>().SetEnemyPos(enemyPos);
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

    public static int GetDirection(float angle){
        if (angle > 45.0f && angle < 135.0f)
            return 3;
        else if (angle >= 135.0f && angle <= 225.0f)
            return 2;
        else if (angle > 225.0f && angle < 315.0f)
            return 1;
        else
            return 0;
    }

    public void PlayerDeath(){
        AudioManager.Instance.PlayerDeath();
        PlayerPrefs.SetInt("XP", (int)playerSts.Death());
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void PLayerWin(){
        PlayerPrefs.SetInt("XP", (int)playerSts.Experience);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
