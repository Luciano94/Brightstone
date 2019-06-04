using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
    private static UIManager instance;

    public static UIManager Instance {
        get {
            instance = FindObjectOfType<UIManager>();
            if(instance == null) {
                GameObject go = new GameObject("UIManager");
                instance = go.AddComponent<UIManager>();
            }
            return instance;
        }
    }
    [Header("Popup text")]
    [SerializeField] private GameObject textPopupPrefab;
    [Header("LoadingScreen")]
    [SerializeField] private GameObject loadingPanel;

    [Header("UI Texts")]
    [SerializeField] private Text expTxt;
    [SerializeField] private Text lifeTxt;
    [SerializeField] private Text atkTxt;
    [SerializeField] private Text mrkTxt;

    [Header("Market Texts")]
    [SerializeField] private Text requiredTxt;
    [SerializeField] private Text mAttackTxt;
    [SerializeField] private Text mLifeTxt;
    [SerializeField] private GameObject market; 

    [Header("FillBars")]
    [SerializeField] private Image playerHpFillBar;
    [SerializeField] private Image playerHitHpFillBar;
    [SerializeField] private Color normalLifeColorBar;
    [SerializeField] private Color lowLifeColorBar;
    [SerializeField] private Color hitLifeColorBar;
    [SerializeField] private float timeToDownHp;
    private float timeLeft;
    private float hpPercentage;
    private float hpHitPercentage;
    [SerializeField] private Image bossHpFillBar;
    [SerializeField] private GameObject bossHPBar;

    [Header("PostProcess")]
    [SerializeField] private float smoothnessLimit;
    private GameManager gameM;

    public GameObject TextPopupPrefab{
        get{return textPopupPrefab;}
    }

    private void Awake(){
        gameM = GameManager.Instance;
        timeLeft = timeToDownHp;
        loadingPanel.SetActive(true);
    }

    private void Start(){
        // Fillbars
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        bossHPBar.SetActive(false);
        hpPercentage = hpHitPercentage = actualHp / maxHp;
        playerHitHpFillBar.fillAmount = playerHpFillBar.fillAmount = hpPercentage;
        lifeTxt.text = actualHp + " / " + maxHp;
        expTxt.text = "Exp: " + gameM.playerSts.Experience;

        playerHpFillBar.color = normalLifeColorBar;
        playerHitHpFillBar.color = hitLifeColorBar;

        FilterManager.SetActiveVignette(false);
    }

    public void LoadingFinish(){
        loadingPanel.SetActive(false);
    }

    private void Update(){
        // Fillbars
        if (hpHitPercentage > hpPercentage){
            if(timeLeft <= 0){
                hpHitPercentage -= Time.deltaTime * 0.5f;
                playerHitHpFillBar.fillAmount = hpHitPercentage;
            }
            else
                timeLeft -= Time.deltaTime;
        }

        if (gameM.playerSts.IsLowHealth){
            FilterManager.SetVignetteSmoothness(Mathf.PingPong(Time.time * 0.2f, smoothnessLimit));
        }

    }

    public void ExpUpdate(){
        expTxt.text = "Exp: " + gameM.playerSts.Experience;
    }

    public void lifeUpdate(){
        // Fillbars
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        hpPercentage = actualHp / maxHp;
        playerHpFillBar.fillAmount = hpPercentage;
        lifeTxt.text = actualHp + " / " + maxHp;

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

        // Timers
        timeLeft = timeToDownHp;

        ExpUpdate();    
    }

    public void atkUpdate(){
        atkTxt.text = "Attack multiplier: " + gameM.playerSts.AtkMult.ToString();
        ExpUpdate();  
    }

    public void EnterMarket(){
        market.SetActive(true);
        requiredTxt.text = ExperienceMarket.Instance.Required;
        
        if (!gameM.playerMovement.IsConnected){
            mAttackTxt.text = "\n  Attack\n   +0,25";
            mLifeTxt.text =  "\n     Life\n     +50";
        }
        else{
            mAttackTxt.text =  "Press X\n  Attack\n   +0,25";
            mLifeTxt.text =  " Press Y\n     Life\n     +50";
        }
    }

    public void marketUPdate(){
        requiredTxt.text = ExperienceMarket.Instance.Required;
    }

    public void ExitMarket(){
        market.SetActive(false);
    }

    public void BossDamaged(){
        float actualHp = gameM.bossSts.Life;
        float maxHp = gameM.bossSts.MaxLife();
        bossHpFillBar.fillAmount = actualHp / maxHp;
    }

    public void InitBoss(){
        bossHPBar.SetActive(true);
    }

    public void DesactiveBoss(){
        bossHPBar.SetActive(false);
    }

    public void ChangeState(GameObject go){
        go.SetActive(!go.activeSelf);
    }
}
