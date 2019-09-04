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
    private float attackStat;
    [SerializeField]private Text attackStatTxt;
    private float newAttackStat;
    [SerializeField]private Text newAttackStatTxt;
    [Header("Defense Stat")]
    private int defenseStat;
    [SerializeField]private Text defenseStatTxt;
    private int newDefenseStat;
    [SerializeField]private Text newDefenseStatTxt;

    [Header("Buttons")]
    [SerializeField]private Button plusLife;
    [SerializeField]private Button minusLife;
    [SerializeField]private Button plusAttack;
    [SerializeField]private Button minusAttack;
    [SerializeField]private Button confirmButton;


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
        UpdateUI();
        if(playerExperience >= requiredExperience){
            canPlus = true;
        }
        canConfirm = false;
        canMinus = false;
        UIManager.Instance.ExpUpdate();
    }

    public void EnterMarket(){
        confirmButton.interactable = false;
        playerSts = GameManager.Instance.playerSts;
        UpdateStats();
        UpdateUI();
        if(playerExperience >= requiredExperience){
            canPlus = true;
            playerExperienceTxt.color = Color.white;
            newPlayerExperienceTxt.color = Color.white;
        }else{
            playerExperienceTxt.color = Color.red;
        }
        canConfirm = false;
        canMinus = false;
        UIManager.Instance.ExpUpdate();

        plusLife.Select();
    }

    private void InitStats(){
        playerLevel = playerSts.PlayerLevel;
        playerExperience = (int)playerSts.Experience;
        if(playerLevel == 1)
            requiredExperience = 100;
        lifeLevel = playerSts.PlayerLevel;
        defenseLevel = playerSts.PlayerLevel;
        attackLevel = playerSts.PlayerLevel;
        lifeStat = (int)playerSts.setLifeStat;
        defenseStat = (int)playerSts.Defense;
        attackStat = playerSts.AtkMult;


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
        //requiredExperience = playerExperience * playerLevel;
        if(lifeLevel != newLifeLevel)
            lifeLevel = playerSts.PlayerLevel;
        if(defenseLevel != newDefenseLevel)
            defenseLevel = playerSts.PlayerLevel;
        if(attackLevel != newAttackLevel)
            attackLevel = playerSts.PlayerLevel;
        lifeStat = (int)playerSts.setLifeStat;
        defenseStat = (int)playerSts.Defense;
        attackStat = playerSts.setAtkMult;


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
        //playerSts.Experience = (float)playerExperience;
        oldLevelrequiredExp = 0;
        //UpdateStats();
        UpdateUI();
    }

    private void UpdateUI(){
        UpdateText();
        UpdateButtons();
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
        newDefenseStatTxt.text = newDefenseStat.ToString();
        
        newDefenseLevelTxt.text = newDefenseLevel.ToString();
        newAttackLevelTxt.text = newAttackLevel.ToString();
        newLifeLevelTxt.text = newLifeLevel.ToString();
        
        newPlayerLevelTxt.text = newPlayerLevel.ToString();
        newRequiredExperienceTxt.text = newRequiredExperience.ToString();
        newPlayerExperienceTxt.text = newPlayerExperience.ToString();
    }

    private void UpdateButtons(){
        if(newPlayerExperience < newRequiredExperience){
            plusAttack.interactable = false;
            plusLife.interactable = false;

        }else{
            plusAttack.interactable = true;
            plusLife.interactable = true;

        }

        if(playerLevel == newPlayerLevel){
            minusLife.interactable = false;
            minusAttack.interactable = false;
        }else{
            minusLife.interactable = true;
            minusAttack.interactable = true;            
        }
    }
    public void Confirm(){
        if(canConfirm){
            confirmButton.interactable = true;
            canConfirm = false;

            //updateo el nivel del personaje
            playerLevel = newPlayerLevel;
            requiredExperience = newRequiredExperience;
            playerExperience = newPlayerExperience;

            //updateo los stats en el player
            if(newLifeStat != lifeStat){
                playerSts.setLifeStat = newLifeStat;
                UIManager.Instance.UpdateFillBar(lifeStat);
            }
            playerSts.setAtkMult = newAttackStat;
            playerSts.SetExperience = playerExperience;
            //Falta agregar stat de defesa
            
            //updateo los stats
            lifeStat = (int)playerSts.Life;
            defenseStat = playerSts.Defense;
            attackStat = playerSts.setAtkMult;
            
            //updateo los stats en la UI
            lifeLevel = newLifeLevel;
            defenseLevel = newDefenseLevel;
            attackLevel = newAttackLevel;
            UIManager.Instance.ExpUpdate();
            UIManager.Instance.LifeUpdate();
            SetStats();
            newLifeLevelTxt.color = Color.white;
            newLifeStatTxt.color = Color.white;
            newAttackLevelTxt.color = Color.white;
            newAttackStatTxt.color = Color.white;

            GameManager.Instance.tutorialMarketComplete = true;

        }else{
            minusAttack.interactable = false;
            minusLife.interactable = false; 
            confirmButton.interactable = false;
        }
    }

    public void LifeLevel(bool operation){
        if(!operation && canMinus && newLifeLevel > lifeLevel){
            newPlayerExperience += oldLevelrequiredExp;
            newPlayerLevel--;
            newLifeLevel--;
            newLifeStat = newLifeStat - 50;
            newRequiredExperience = (int)(newRequiredExperience * 0.5f);
            newPlayerExperienceTxt.color = Color.white;
            if(playerLevel == newPlayerLevel){
                canMinus = false;
                canConfirm = false;
                newPlayerExperience = playerExperience;
                oldLevelrequiredExp = 0;
                newLifeLevelTxt.color = Color.white;
                newLifeStatTxt.color = Color.white;
            }
        }else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newLifeLevel++;
            newLifeStat = 50 + newLifeStat;
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            //Como se calcula la Exp
            newRequiredExperience = newRequiredExperience * 2; 
            newLifeLevelTxt.color = Color.cyan;
            newLifeStatTxt.color = Color.cyan;
            canMinus = true;
            confirmButton.interactable = true;
            if(newPlayerExperience < newRequiredExperience){
                canPlus = false;
                canMinus = true;
                canConfirm = true;
                newPlayerExperienceTxt.color = Color.red;
            }
        }
        UpdateUI();
    }

    public void AttackLevel(bool operation){
        if(!operation && canMinus && newAttackLevel > attackLevel){
            newPlayerExperience += oldLevelrequiredExp;
            newPlayerLevel--;
            newAttackLevel--;
            newAttackStat = newAttackStat - 0.5f;
            newRequiredExperience =(int)(newRequiredExperience * 0.5f);; 
            newPlayerExperienceTxt.color = Color.white;
            if(playerLevel == newPlayerLevel){
                newPlayerExperience = playerExperience;
                canConfirm = false;
                canMinus = false;
                newAttackLevelTxt.color = Color.white;
                newAttackStatTxt.color = Color.white;
            }
        } else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newAttackLevel++;
            newAttackStat = 0.5f +newAttackStat;
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            //Como se calcula la Exp

            newRequiredExperience = newRequiredExperience * 2;
            newAttackLevelTxt.color = Color.cyan;
            newAttackStatTxt.color = Color.cyan;
            canMinus = true;
            confirmButton.interactable = true;
            if(newPlayerExperience < newRequiredExperience){
                canPlus = false;
                canMinus = true;
                canConfirm = true;
                newPlayerExperienceTxt.color = Color.red;
            }
        }
        UpdateUI();
    }

    public void DefenseLevel(bool operation){
        if(!operation && canMinus && newDefenseLevel > defenseLevel){
            newPlayerExperience += newRequiredExperience;
            newPlayerLevel--;
            newDefenseLevel--;
            newRequiredExperience = (int)(newRequiredExperience * 0.5f);; 
            if(playerLevel == newPlayerLevel){
                newPlayerExperience = playerExperience;
                canMinus = false;
                canConfirm = false;
            }
        }else if(operation && newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newDefenseLevel++;
            newPlayerExperience -= newRequiredExperience;
            oldLevelrequiredExp = newRequiredExperience;
            //Como se calcula la Exp

            newRequiredExperience = newRequiredExperience * 2;
            if(newPlayerExperience < newRequiredExperience)
            canPlus = false;
            canMinus = true;
            canConfirm = true;
        }
        UpdateUI();
    }
}
