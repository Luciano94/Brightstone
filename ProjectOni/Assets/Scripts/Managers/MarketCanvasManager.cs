using UnityEngine;
using UnityEngine.UI;

public class MarketCanvasManager : MonoBehaviour{
    private int playerLevel;
    [SerializeField]private Text playerLevelTxt;
    private int newPlayerLevel;
    [SerializeField]private Text newPlayerLevelTxt;

    private int playerExperience;
    [SerializeField]private Text playerExperienceTxt;
    private int newPlayerExperience;
    [SerializeField]private Text newPlayerExperienceTxt;


    private int requiredExperience;
    [SerializeField]private Text requiredExperienceTxt;  
    private int newRequiredExperience;
    [SerializeField]private Text newRequiredExperienceTxt;


    private int lifeLevel;
    [SerializeField]private Text lifeLevelTxt;
    private int newLifeLevel;
    [SerializeField]private Text newLifeLevelTxt;


    private int attackLevel;
    [SerializeField]private Text attackLevelTxt;
    private int newAttackLevel;
    [SerializeField]private Text newAttackLevelTxt;


    private int defenseLevel;
    [SerializeField]private Text defenseLevelTxt;
    private int newDefenseLevel;
    [SerializeField]private Text newDefenseLevelTxt;

    private int lifeStat;
    [SerializeField]private Text lifeStatTxt;
    private int newLifeStat;
    [SerializeField]private Text newLifeStatTxt;

    private int attackStat;
    [SerializeField]private Text attackStatTxt;
    private int newAttackStat;
    [SerializeField]private Text newAttackStatTxt;

    private int defenseStat;
    [SerializeField]private Text defenseStatTxt;
    private int newDefenseStat;
    [SerializeField]private Text newDefenseStatTxt;

    private PlayerStats playerSts;

    private void Start() {   
        playerSts = GameManager.Instance.playerSts;
        UpdateStats();
        UpdateText();
    }

    private void UpdateStats(){
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
        //updateo el nivel del personaje
        playerLevel = newPlayerLevel;
        requiredExperience = newRequiredExperience;
        playerExperience = newPlayerExperience;

        //updateo los level stats 
        playerSts.LifeStat = (50 * (newLifeLevel - lifeLevel));
        playerSts.AtkMult = (50 * (newAttackLevel - attackLevel));
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

        UpdateText();
    }

    public void LifeLevel(){
        if(newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newLifeLevel++;
            newPlayerExperience -= newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel; 
        }

                UpdateText();
    }

    public void AttackLevel(){
        if(newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newAttackLevel++;
            newPlayerExperience -= newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel; 
        }

                UpdateText();
    }

    public void DefenseLevel(){
        if(newPlayerExperience >= newRequiredExperience){
            newPlayerLevel++;
            newDefenseLevel++;
            newPlayerExperience -= newRequiredExperience;
            newRequiredExperience = newRequiredExperience * newPlayerLevel; 
        }

                UpdateText();
    }
}
