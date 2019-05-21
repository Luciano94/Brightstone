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

    [Header("LoadingScreen")]
    [SerializeField] private GameObject loadingPanel;
    [Header("Texts")]
    [SerializeField] private Text expTxt;
    [SerializeField] private Text lifeTxt;
    [SerializeField] private Text atkTxt;
    [SerializeField] private Text mrkTxt;
    [SerializeField] private Text requiredTxt;
    [SerializeField] private GameObject market; 

    [Header("FillBars")]
    [SerializeField] Image playerHpFillBar;
    [SerializeField] Image playerHitHpFillBar;
    [SerializeField] float timeToDownHp;
    private float timeLeft;
    private float hpPercentage;
    private float hpHitPercentage;
    [SerializeField] Image bossHpFillBar;
    [SerializeField] GameObject bossHPBar;
    private GameManager gameM;

    

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
        //requiredTxt.enabled = true;
    }

    public void marketUPdate(){
        requiredTxt.text = ExperienceMarket.Instance.Required;
    }

    public void ExitMarket(){
        market.SetActive(false);
        //requiredTxt.enabled = false;
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
}
