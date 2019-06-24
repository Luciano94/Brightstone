using UnityEngine;
using UnityEngine.UI;

public class MarketCanvasManager : MonoBehaviour{

    [Header("Player Level")]
    private int playerLevel;
    [SerializeField]private Text playerLevelTxt;
    private int newPlayerLevel;
    [SerializeField]private Text newPlayerLevelTxt;

    [Header("Player Experience")]
    private int playerExperience;
    [SerializeField]private Text playerExperienceTxt;
    private int newPlayerExperience;
    private int oldLevelrequiredExp;
    [SerializeField]private Text newPlayerExperienceTxt;


    private int requiredExperience;
    [SerializeField]private Text requiredExperienceTxt;  
    private int newRequiredExperience;
    [SerializeField]private Text newRequiredExperienceTxt;

    [Header("Player Life")]
    private int lifeLevel;
    [SerializeField]private Text lifeLevelTxt;
    private int newLifeLevel;
    [SerializeField]private Text newLifeLevelTxt;

    [Header("Player Attack")]
    private int attackLevel;
    [SerializeField]private Text attackLevelTxt;
    private int newAttackLevel;
    [SerializeField]private Text newAttackLevelTxt;

    [Header("Player Defense")]
    private int defenseLevel;
    [SerializeField]private Text defenseLevelTxt;
    private int newDefenseLevel;
    [SerializeField]private Text newDefenseLevelTxt;

    [Header("Life Stat")]
    private int lifeStat;
    [SerializeField]private Text lifeStatTxt;
    private int newLifeStat;
    [SerializeField]private Text newLifeStatTxt;
    [Header("Attack Stat")]
    private int attackStat;
    [SerializeField]private Text attackStatTxt;
    private int newAttackStat;
    [SerializeField]private Text newAttackStatTxt;
    [Header("Defense Stat")]
    private int defenseStat;
    [SerializeField]private Text defenseStatTxt;
    private int newDefenseStat;
    [SerializeField]private Text newDefenseStatTxt;

    private PlayerStats playerSts;
    private bool canPlus;
    private bool canMinus;
    private bool canConfirm;

    private static MarketCanvasManager instance;

    public static MarketCanvasManager Instance{
        get {
            instance = FindObjectOfType<MarketCanvasManager>();
            if(instance == null){
                GameObject go = new GameObject("MarketCanvasManager");
                instance = go.AddComponent<MarketCanvasManager>();
            }
            return instance;
        }
    }

    private void Start() {   
        playerSts = GameManager.Instance.playerSts;
        InitStats();
        UpdateText();
        if(playerExperience >= requiredExperience){
            canPlus = true;
        }
        canConfirm = false;
        canMinus = false;
    }

    private void InitStats(){
        playerLevel = playerSts.PlayerLevel;
        playerExperience = (int)playerSts.Experience;
        requiredExperience = 100;
        lifeLevel = playerSts.PlayerLevel;
        defenseLevel = playerSts.PlayerLevel;
        attackLevel = playerSts.PlayerLevel;
        lifeStat = (int)playerSts.Life;
        defenseStat = (int)playerSts.Defense;
        attackStat = (int)playerSts.AtkMult;


        newPlayerLevel = playerLevel;
        newAttackLevel = attackLevel;
        newDefenseLevel = defenseLevel;
        newLifeLevel = lifeLevel;
        newAttackStat = attackStat;
        newLifeStat = lifeStat;
        newDefenseStat = defenseStat;
        newRequiredExperience = requiredExperience;
        newPlayerExperience = playerExperience;
    }

    private void UpdateStats(){
        playerLevel = playerSts.PlayerLevel;
        playerExperience = (int)playerSts.Experience;
        requiredExperience = playerExperience * playerLevel;
        if(lifeLevel != newLifeLevel)
            lifeLevel = playerSts.PlayerLevel;
        if(defenseLevel != newDefenseLevel)
            defenseLevel = playerSts.PlayerLevel;
        if(attackLevel != newAttackLevel)
            attackLevel = playerSts.PlayerLevel;
        lifeStat = (int)playerSts.Life;
        defenseStat = (int)playerSts.Defense;
        attackStat = (int)playerSts.AtkMult;


        newPlayerLevel = playerLevel;
        newAttackLevel = attackLevel;
        newDefenseLevel = defenseLevel;
        newLifeLevel = lifeLevel;
        newAttackStat = attackStat;
        newLifeStat = lifeStat;
        newDefenseStat = defenseStat;
        newRequiredExperience = requiredExperience;
        newPlayerExperience = playerExperience;
    }

    private void SetStats(){
        playerSts.PlayerLevel = playerLevel;
        playerSts.Experience = (float)playerExperience;
        oldLevelrequiredExp = 0;
        UpdateStats();
        UpdateText();
    }

    private void UpdateText(){
        playerLevelTxt.text = playerLevel.ToString();
        playerExperienceTxt.text = playerExperience.ToString();
        requiredExperienceTxt.text = requiredExperience.ToString();
        lifeLevelTxt.text = lifeLevel.ToString();
        defenseLevelTxt.text = defenseLevel.ToString();
        attackLevelTxt.text = attackLevel.ToString();
        attackStatTxt.text = attackStat.ToString();
        lifeStatTxt.text = lifeStat.ToString();
        defenseStatTxt.text = defenseStat.ToString();

        newAttackStatTxt.text = newAttackStat.ToString();
        newLifeStatTxt.text = newLifeStat.ToString();
        newDefenseLevelTxt.text = newDefenseStat.ToString();
        newPlayerLevelTxt.text = newPlayerLevel.ToString();
        newAttackLevelTxt.text = newAttackLevel.ToString();
        newDefenseLevelTxt.text = newDefenseLevel.ToString();
        newLifeLevelTxt.text = newLifeLevel.ToString();
        newRequiredExperienceTxt.text = newRequiredExperience.ToString();
        newPlayerExperienceTxt.text = newPlayerExperience.ToString();

    }

    public void Confirm(){
        if(canConfirm){
            canConfirm = false;
            //updateo el nivel del personaje
            playerLevel = newPlayerLevel;
            requiredExperience = newRequiredExperience;
            playerExperience = newPlayerExperience;

            //updateo los level stats 
            playerSts.LifeStat = (50 * (newLifeLevel - lifeLevel));
            playerSts.AtkMult = (5 * (newAttackLevel - attackLevel));
            //Falta agregar stat de defesa
            playerSts.SetExperience = playerExperience;
            
            //updateo los stats
            lifeStat = (int)playerSts.Life;
            defenseStat = (int)playerSts.Defense;
            attackStat = (int)playerSts.AtkMult;
            
            //updateo los stats en la UI
            lifeLevel = newLifeLevel;
            defenseLevel = newDefenseLevel;
            attackLevel = newAttackLevel;
            SetStats();
        }
    }

    public void LifeLevel(bool operation){
        if(!operation && canMinus && newLifeLevel > lifeLevel){
            newPlayerExperience += oldLevelrequiredExp;
            newPlayerLevel--;
            newLifeLevel--;
            newLifeStat =lifeStat + (50 * (newLifeLevel));
            newRequiredExperience -= oldLevelrequiredExp; 
            if(playerLevel == newPlayerLevel){
                canMinus = false;
                canConfirm = false;
                newPlayerExperience = playerExperience;
                newRequiredExperience = 100;
                oldLevelrequiredExp = 0;
            }
        }else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newLifeLevel++;
            newLifeStat = lifeStat + (50 * (newLifeLevel));
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel; 
            if(newPlayerExperience < newRequiredExperience)
            canPlus = false;
            canMinus = true;
            canConfirm = true;
        }
        UpdateText();
    }

    public void AttackLevel(bool operation){
        if(!operation && canMinus && newAttackLevel > attackLevel){
            newPlayerExperience += oldLevelrequiredExp;
            newPlayerLevel--;
            newAttackLevel--;
            newAttackStat = attackStat + (5 * (newAttackLevel));
            newRequiredExperience -= oldLevelrequiredExp; 
            if(playerLevel == newPlayerLevel){
                newRequiredExperience = 100;
                newPlayerExperience = playerExperience;
                                canConfirm = false;
                canMinus = false;
            }
        } else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newAttackLevel++;
            newAttackStat = attackStat + (5 * (newAttackLevel));
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel;
            if(newPlayerExperience < newRequiredExperience)
            canPlus = false;
            canMinus = true;
            canConfirm = true;
        }
        UpdateText();
    }

    public void DefenseLevel(bool operation){
        if(!operation && canMinus && newDefenseLevel > defenseLevel){
            newPlayerExperience += newRequiredExperience;
            newPlayerLevel--;
            newDefenseLevel--;
            newRequiredExperience = newRequiredExperience * newPlayerLevel; 
            if(playerLevel == newPlayerLevel){
                newRequiredExperience = 100;
                newPlayerExperience = playerExperience;
                canMinus = false;
                canConfirm = false;
            }
        }else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newDefenseLevel++;
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel;
            if(newPlayerExperience < newRequiredExperience)
            canPlus = false;
            canMinus = true;

            canConfirm = true;
        }
        UpdateText();
    }
}
