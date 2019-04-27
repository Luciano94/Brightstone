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

    [SerializeField]private Text expTxt;
    [SerializeField]private Text lifeTxt;
    [SerializeField]private Text atkTxt;
    private GameManager gameM;

    private void Awake() {
        gameM = GameManager.Instance;
    }

    public void ExpUpdate(){
        expTxt.text = "Exp: " + gameM.playerSts.Experience.ToString();
    }

    public void lifeUpdate(){
        lifeTxt.text = "Life: " + gameM.playerSts.LifeStat.ToString(); 
        expTxt.text = "Exp: " + gameM.playerSts.Experience.ToString();       
    }

    public void atkUpdate(){
        atkTxt.text = "AtkDmg: " + gameM.playerSts.AtkDmg.ToString();
        expTxt.text = "Exp: " + gameM.playerSts.Experience.ToString();   
    }
}
