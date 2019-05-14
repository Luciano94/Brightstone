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

    [SerializeField] private Text expTxt;
    [SerializeField] private Text lifeTxt;
    [SerializeField] private Text atkTxt;
    [SerializeField] private Text mrkTxt;
    [SerializeField] private Text requiredTxt;

    [Header("FillBars")]
    [SerializeField] Image playerHpFillBar;
    [SerializeField] Image bossHpFillBar;
    [SerializeField] GameObject bossHPBar;
    private GameManager gameM;

    private void Awake() {
        gameM = GameManager.Instance;
        mrkTxt.enabled = false;
    }

    private void Start(){
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        bossHPBar.SetActive(false);
        playerHpFillBar.fillAmount = actualHp / maxHp;
        lifeTxt.text = actualHp + " / " + maxHp;
        expTxt.text = "Exp: " + gameM.playerSts.Experience;
    }

    public void ExpUpdate(){
        expTxt.text = "Exp: " + gameM.playerSts.Experience;
    }

    public void lifeUpdate(){
        float actualHp = gameM.playerSts.LifeStat;
        float maxHp = gameM.playerSts.MaxLife();
        playerHpFillBar.fillAmount = actualHp / maxHp;
        lifeTxt.text = actualHp + " / " + maxHp;
        ExpUpdate();    
    }

    public void atkUpdate(){
        atkTxt.text = "Attack multiplier: " + gameM.playerSts.AtkMult.ToString();
        ExpUpdate();  
    }

    public void EnterMarket(){
        mrkTxt.enabled = true;
        requiredTxt.text = ExperienceMarket.Instance.Required;
        requiredTxt.enabled = true;
    }

    public void marketUPdate(){
        requiredTxt.text = ExperienceMarket.Instance.Required;
    }

    public void ExitMarket(){
        mrkTxt.enabled = false;
        requiredTxt.enabled = false;
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
