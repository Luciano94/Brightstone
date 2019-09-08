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
    [SerializeField] private ShakerController shakerController;
    [SerializeField] private ZoomWhenParrying zoomWhenParrying;

    private bool isConnected;
    private const int mainMenuIndex = 0;

    public bool isTutorial = false;
    private bool playerOn = false;
    public EventManager eventManager;
    public GameObject tutorialMarket;
    public bool tutorialMarketComplete = false;

    public bool PlayerOn{
        set{player.SetActive(value);
            playerOn = value;}
    }

    private void Awake(){
        RunSaver.NewRun();
        if(isTutorial){
            player.SetActive(false);
        }
        if(!PlayerPrefs.HasKey("XP")){
            PlayerPrefs.SetInt("XP",(int)playerSts.Experience);
        }else{
            PlayerPrefs.GetInt("XP", 0);
        }
    }

    private void Update(){
        RunSaver.currentRun.data.time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.H)){
            playerSts.ChangeGodModeState();
            UIManager.Instance.ChangeGodModeState();
        }

        DetectDevice();
    }

    public void ExitToMainMenu(){
        SceneManager.LoadScene(mainMenuIndex);
    }

    private void DetectDevice(){
        if (Input.GetJoystickNames().Length > 0){
            if(Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0].Length > 10)
                isConnected = true;
            else
                isConnected = false;
        }
        else
            isConnected = false;
    }

    public bool IsConnected{
        get{return isConnected;}
    }

    public Vector3 PlayerPos{
        get{return player.transform.position;}
    }

    public void PlayBlood(){
        playerCombat.PlayBlood();
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
    
    public PlayerAnimations playerAnimations{
        get{return player.GetComponentInChildren<PlayerAnimations>();}
    }

    public ActiveRoom activeRoom{
        get{return player.GetComponent<ActiveRoom>();}
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

    public ShakerController ShakerController{
        get{ return shakerController; }
    }

    public ZoomWhenParrying ZoomWhenParrying{
        get{ return zoomWhenParrying; }
    }

    public bool IsReadyToParry(){
        return eventManager.GetActualStep() >= 8 ? true : false;
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

        AudioManager.Instance.StopTheme();
        UIManager.Instance.RunFinished();
        MenuManager.Instance.LoseMenuCanvas = true;
    }

    public void PlayerWin(){
        AudioManager.Instance.BossDeath();

        PlayerPrefs.SetInt("XP", (int)playerSts.Experience);
        PlayerPrefs.Save();

        RunSaver.currentRun.data.level = (ushort)playerSts.PlayerLevel;
        RunSaver.currentRun.data.runFinished = true;
        RunSaver.currentRun.data.win = true;
        RunSaver.Save();
        
        playerAnimations.enabled = false;
        playerCombat.enabled = false;
        playerSts.enabled = false;

        AudioManager.Instance.StopTheme();
        MenuManager.Instance.WinMenuCanvas = true;

        DisablePlayer();
    }

    public void EnablePlayer(){
        playerMovement.enabled = true;
        playerCombat.enabled = true;
        playerAnimations.enabled = true;
    }

    public void DisablePlayer(){
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        player.GetComponentInChildren<PlayerAnimations>().Idle();
        playerAnimations.enabled = false;
    }
}
