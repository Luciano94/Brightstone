using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    private static GameManager instance;

    public static GameManager Instance{
        get {
            instance = FindObjectOfType<GameManager>();
            if(instance == null){
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelBoss;

    private const int mainMenuIndex = 0;

    private void Awake(){
        RunSaver.NewRun();

        if(!PlayerPrefs.HasKey("XP")){
            PlayerPrefs.SetInt("XP",(int)playerSts.Experience);
        }else{
            playerSts.SetExperience = 9999;//PlayerPrefs.GetInt("XP", 0);
        }
    }

    private void Update(){
        RunSaver.currentRun.data.time += Time.deltaTime;
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

    public PlayerMovement playerMovement{
        get{return player.GetComponent<PlayerMovement>();}
    }

    public void SetEnemyHitFrom(Vector3 enemyPos){
        player.GetComponent<PlayerMovement>().SetEnemyPos(enemyPos);
    }

    public GameObject SetBoss{
        set{
            levelBoss = value;
            UIManager.Instance.SetBossListener();
            UIManager.Instance.DesactiveBoss();
        }
    }
    public EnemyStats bossSts{
        get{return levelBoss.GetComponent<EnemyStats>();}
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

        RunSaver.currentRun.data.level = (ushort)playerSts.PlayerLevel;
        RunSaver.currentRun.data.runFinished = true;
        RunSaver.Save();

        // Temp
        player.transform.position = new Vector3(600.0f, 600.0f, 10.0f);
        player.SetActive(false);

        UIManager.Instance.RunFinished();
        MenuManager.Instance.LoseMenuCanvas = true;
        Invoke("GoToMenu", 10.0f);
    }

    public void PlayerWin(){
        PlayerPrefs.SetInt("XP", (int)playerSts.Experience);
        PlayerPrefs.Save();

        RunSaver.currentRun.data.level = (ushort)playerSts.PlayerLevel;
        RunSaver.currentRun.data.runFinished = true;
        RunSaver.currentRun.data.win = true;
        RunSaver.Save();

        UIManager.Instance.RunFinished();
        MenuManager.Instance.WinMenuCanvas = true;
        Invoke("GoToMenu", 10.0f);
    }

    private void GoToMenu(){
        SceneManager.LoadScene(0);
    }
}
