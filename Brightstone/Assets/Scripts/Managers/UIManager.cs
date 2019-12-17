using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour{
    private static UIManager instance;

    public static UIManager Instance{
        get {
            instance = FindObjectOfType<UIManager>();
            if(instance == null){
                GameObject go = new GameObject("UIManager");
                instance = go.AddComponent<UIManager>();
            }
            return instance;
        }
    }
    [Header("Popup text")]
    [SerializeField] private GameObject textPopupPrefab;
    [Header("LoadingScreen")]
    [SerializeField] private Image loadingPanel;
    private bool generating = true;

    [Header("UI Texts")]
    [SerializeField] private Text atkTxt;
    [SerializeField] private Text mrkTxt;
    [SerializeField] private Text runTimeTxt;

    [Header("Market Texts")]
    [SerializeField] private Text requiredTxt;
    [SerializeField] private Text mAttackTxt;
    [SerializeField] private Text mLifeTxt;
    [SerializeField] private GameObject market; 

    [Header("FillBars")]
    [SerializeField] private float sizePerLifePoint = 2.7f;
    [SerializeField] private Image playerHpFillBar;
    [SerializeField] private Image playerHitHpFillBar;
    [SerializeField] private Image playerEmptyFillBar;
    [SerializeField] private Color normalLifeColorBar;
    [SerializeField] private Color lowLifeColorBar;
    [SerializeField] private Color hitLifeColorBar;
    [SerializeField] private float timeToDownHp;
    private float timeLeft;
    private float hpPercentage;
    private float hpHitPercentage;
    [SerializeField] private Image bossHpFillBar;
    [SerializeField] private GameObject bossHPBar;

    [Header("Experience FillBar")]
    [SerializeField]private Transform experienceEmptyFillBar;
    [SerializeField]private Transform experienceActualFillBar;


    [Header("PostProcess")]
    [SerializeField] private float smoothnessLimit;

    [Header("Run Data")]
    [SerializeField] private GameObject statsHolder;
    [SerializeField] private Text damageDealtTxt;
    [SerializeField] private Text damageRecievedTxt;
    [SerializeField] private Text enemiesKilledTxt;
    [SerializeField] private Text bossesKilledTxt;
    [SerializeField] private Text expObtainedTxt;
    [SerializeField] private Text timeTxt;
    private GameManager gameM;

    [Header("Cheatcodes")]
    [SerializeField] private GameObject godMode;

    [Header("Pause Canvas")]
    [SerializeField] private GameObject pauseCamvas;

    [Header("Death Anims")]
    [SerializeField] private Animator[] animsToRun;
    [SerializeField] private Image youDieImg;
    
    private bool appearDieImage = false;

    public GameObject TextPopupPrefab{
        get{return textPopupPrefab;}
    }

    private void Awake(){
        gameM = GameManager.Instance;
        timeLeft = timeToDownHp;
        loadingPanel.enabled = true;
        experienceActualFillBar.localScale = new Vector3(0.0f, 1.0f);
    }

    private void Start(){
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        hpPercentage = hpHitPercentage = actualHp / maxHp;
        playerHitHpFillBar.fillAmount = playerHpFillBar.fillAmount = hpPercentage;
        //lifeTxt.text = actualHp + " / " + maxHp;

        playerHpFillBar.color = normalLifeColorBar;
        playerHitHpFillBar.color = hitLifeColorBar;

        FilterManager.SetActiveVignette(false);
    }

    public void LoadingFinish(){
        generating = false;
    }

    private void Update(){
        DisappearingLoadingPanel();

        if (hpHitPercentage > hpPercentage){
            if(timeLeft <= 0){
                hpHitPercentage -= Time.deltaTime * 0.5f;
                playerHitHpFillBar.fillAmount = hpHitPercentage;
                if(hpHitPercentage < hpPercentage){
                    hpHitPercentage = hpPercentage;
                }
            }
            else
                timeLeft -= Time.deltaTime;
        }

        if (gameM.playerAlive){

            if (gameM.playerSts.IsLowHealth){
                FilterManager.SetVignetteSmoothness(Mathf.PingPong(Time.time * 0.2f, smoothnessLimit));
            }

            TimeUpdate();
        }
        else if (appearDieImage){
            Color tempC = youDieImg.color;
            tempC.a += Time.deltaTime;
            youDieImg.color = tempC;
        }
    }

    void DisappearingLoadingPanel(){
        if (!generating && loadingPanel.color.a > 0.0f){
            Color color = loadingPanel.color;
            color.a -= Time.deltaTime;
            if (color.a <= 0.0f){
                loadingPanel.enabled = false;
                return;
            }
            loadingPanel.color = color;
        }
    }

    void TimeUpdate(){
        float timePassed = GameManager.Instance.timePassed;

        string minutes = Mathf.Floor(timePassed * 0.0167f).ToString("00");
        string seconds = Mathf.Floor(timePassed % 60).ToString("00");

        runTimeTxt.text = minutes + ":" + seconds;
    }

    public void ExpUpdate(){

        float value = gameM.playerSts.Experience / gameM.playerSts.experienceToNextLevel;
        SoundManager.Instance.PlayerXP(value);
        experienceActualFillBar.localScale = new Vector3(value, 1.0f);
    }

    public void LifeUpdate(){
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        hpPercentage = actualHp / maxHp;
        playerHitHpFillBar.fillAmount = hpHitPercentage;
        playerHpFillBar.fillAmount = hpPercentage;

        //lifeTxt.text = actualHp + " / " + maxHp;

        if(gameM.playerSts.IsLowHealth){
            playerHpFillBar.color = lowLifeColorBar;
            FilterManager.SetActiveVignette(true);
        }
        else{
            playerHpFillBar.color = normalLifeColorBar;

            FilterManager.SetVignetteSmoothness(0.0f);
            FilterManager.SetActiveVignette(false);
        }

        if(playerHitHpFillBar.color != hitLifeColorBar) playerHitHpFillBar.color = hitLifeColorBar;

        timeLeft = timeToDownHp;

        ExpUpdate();    
    }

    public void UpdateFillBar(float value){
        //Orange bar
        playerHitHpFillBar.rectTransform.sizeDelta = new Vector2(
            value * sizePerLifePoint,
            playerHitHpFillBar.rectTransform.rect.height
        );

        //Grey bar
        playerEmptyFillBar.rectTransform.sizeDelta = new Vector2(
            value * sizePerLifePoint,
            playerEmptyFillBar.rectTransform.rect.height
        );

        //Red bar
        playerHpFillBar.rectTransform.sizeDelta = new Vector2(
            value * sizePerLifePoint,
            playerHpFillBar.rectTransform.rect.height
        );
        LifeUpdate();
        hpHitPercentage = hpPercentage;
    }

    public void atkUpdate(){
        atkTxt.text = "Attack multiplier: " + gameM.playerSts.AtkMult.ToString();
        ExpUpdate();  
    }

    public void EnterMarket(){
        market.SetActive(true);
        MarketCanvasManager.Instance.EnterMarket();
        GameManager.Instance.PauseGame(true);
    }

    public void MarketUpdate(){
       // requiredTxt.text = ExperienceMarket.Instance.Required;
    }

    public void ExitMarket(){
        market.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }

    public void BossDamaged(){
        float actualHp = gameM.bossSts.Life;
        float maxHp = gameM.bossSts.MaxLife();
        bossHpFillBar.fillAmount = actualHp / maxHp;
    }

    public void SetBossListener(){
        gameM.bossSts.OnHit.AddListener(BossDamaged);
    }

    public void InitBoss(){
        bossHPBar.SetActive(true);
    }

    public void DesactiveBoss(){
        bossHPBar.SetActive(false);
    }

    public void RunFinished(){
        statsHolder.SetActive(true);

        RunDataManager.Data cData = RunSaver.currentRun.data;

        string minutes = Mathf.Floor(cData.time / 60).ToString("00");
        string seconds = Mathf.Floor(cData.time % 60).ToString("00");
        
        damageDealtTxt.text    = "" + cData.damageDealt;
        damageRecievedTxt.text = "" + cData.damageRecieved;
        enemiesKilledTxt.text  = "" + cData.enemiesKilled;
        bossesKilledTxt.text   = "" + cData.bossesKilled;
        expObtainedTxt.text    = "" + cData.expObtained;
        timeTxt.text           = "" + minutes + ":" + seconds;
    }

    public void ChangeState(GameObject go){
        go.SetActive(!go.activeSelf);
    }

    public void ChangeGodModeState(){
        godMode.SetActive(!godMode.activeSelf);
    }

    public void ShowPause(){
        //pauseCamvas.SetActive(true);
    }

    public void UnshowPause(){
        //pauseCamvas.SetActive(false);
    }

    public void RunDeathAnimations(){
        foreach (Animator anim in animsToRun)
            anim.SetTrigger("RunAnim");
    }

    public void YouDieImgAppear(){
        appearDieImage = true;
        youDieImg.enabled = true;
        Invoke("RunKanjiAnim", 1.5f);
    }

    public void RunKanjiAnim(){
        Animator anim = youDieImg.GetComponent<Animator>();
        SoundManager.Instance.PostMortem();

        anim.SetTrigger("Run");
    }
}
