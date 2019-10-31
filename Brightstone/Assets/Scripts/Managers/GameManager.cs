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

    /* Player and Boss components */
    private ActiveRoom _activeRoom;
    private PlayerDash _playerDash;
    private PlayerStats _playerStats;
    private PlayerCombat _playerCombat;
    private PlayerMovement _playerMovement;
    private PlayerAnimations _playerAnimations;

    public bool isTutorial = false;
    private bool playerOn = false;
    public EventManager eventManager;
    public GameObject tutorialMarket;
    public bool tutorialMarketComplete = false;
    public bool playerAlive = true;

    public bool PlayerOn{
        set{player.SetActive(value);
            playerOn = value;}
    }

    private void Awake(){
        DetectDevice();
        RunSaver.NewRun();

        _activeRoom = player.GetComponent<ActiveRoom>();
        _playerDash = player.GetComponent<PlayerDash>();
        _playerStats = player.GetComponent<PlayerStats>();
        _playerCombat = player.GetComponent<PlayerCombat>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerAnimations = player.GetComponentInChildren<PlayerAnimations>();

        if (isTutorial){
            player.SetActive(false);
        }
        if(!PlayerPrefs.HasKey("XP")){
            PlayerPrefs.SetInt("XP",(int)_playerStats.Experience);
        }else{
            PlayerPrefs.GetInt("XP", 0);
        }

        if (PlayerPrefs.GetInt("PlayerDeathInBossRoom") == 1)
            PlayerPrefs.SetInt("PlayerDeathInBossRoom", 0);
        else
            SoundManager.Instance.LevelEnter();
    }

    private void Update(){
        DetectDevice();
        RunSaver.currentRun.data.time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.H)){
            playerSts.ChangeGodModeState();
            UIManager.Instance.ChangeGodModeState();
        }

    }

    public void ExitToMainMenu(){
        PlayerPrefs.SetInt("PlayerDeathInBossRoom", 0);
        SceneManager.LoadScene(mainMenuIndex);
    }

    private void DetectDevice(){
        if (Input.GetJoystickNames().Length > 0){
            if(Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0].Length > 10){
                isConnected = true;
                UIManager.Instance.UnshowPause();
                Time.timeScale = 1.0f;
            }
            else{
                isConnected = false;
                UIManager.Instance.ShowPause();
                Time.timeScale = 0.0f;
            }
        }
        else{
            isConnected = false;
            UIManager.Instance.ShowPause();
            Time.timeScale = 0.0f;
        }

    }

    public bool IsConnected{
        get{return isConnected;}
    }

    public Vector3 PlayerPos{
        get{return player.transform.position;}
    }

    public void PlayBlood(){
        _playerCombat.PlayBlood();
    }

    public PlayerStats playerSts{
        get{return _playerStats;}
    }

    public PlayerCombat playerCombat{
        get{return _playerCombat;}
    }

    public PlayerMovement playerMovement{
        get{return _playerMovement;}
    }
    
    public PlayerAnimations playerAnimations{
        get{return _playerAnimations;}
    }

    public ActiveRoom activeRoom{
        get{return _activeRoom;}
    }

    public void SetEnemyHitFrom(Vector3 enemyPos){
        _playerMovement.SetEnemyPos(enemyPos);
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
        get{ return _playerCombat.isParry; }
    }
    
    public bool PlayerIsAttack{
        get{ return _playerCombat.isAttack; }
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
        PlayerPrefs.SetInt("XP", (int)playerSts.Death());
        PlayerPrefs.Save();

        RunSaver.currentRun.data.level = (ushort)playerSts.PlayerLevel;
        RunSaver.currentRun.data.runFinished = true;
        RunSaver.Save();

        // Temp
        //player.transform.position = new Vector3(600.0f, 600.0f, 10.0f);
        //player.SetActive(false);

        if (!_activeRoom.GetRoomsBehaviour().HaveBoss)
            PlayerPrefs.SetInt("PlayerDeathInBossRoom", 1);
        EnemyBehaviour.Instance.OnPlayerDeath();
        playerAlive = false;

        _playerCombat.enabled = false;
        _playerMovement.enabled = false;
        _playerStats.enabled = false;
        _playerDash.enabled = false;

        //AudioManager.Instance.StopTheme();
        MenuManager.Instance.LoseMenuCanvas = true;
    }

    public void PlayerWin(){
        PlayerPrefs.SetInt("XP", (int)playerSts.Experience);
        PlayerPrefs.Save();

        RunSaver.currentRun.data.level = (ushort)playerSts.PlayerLevel;
        RunSaver.currentRun.data.runFinished = true;
        RunSaver.currentRun.data.win = true;
        RunSaver.Save();
        
        _playerStats.enabled = false;
        _playerDash.enabled = false;

        //AudioManager.Instance.StopTheme();
        MenuManager.Instance.WinMenuCanvas = true;

        DisablePlayer();
    }

    public void EnablePlayer(){
        _playerMovement.enabled = true;
        _playerCombat.enabled = true;
        _playerAnimations.enabled = true;
    }

    public void DisablePlayer(){
        _playerMovement.enabled = false;
        _playerCombat.enabled = false;
        _playerAnimations.Idle();
        _playerAnimations.enabled = false;
    }
}
