using UnityEngine;
using UnityEngine.UI;
public class ExperienceTest : MonoBehaviour
{
    private ExperienceMarket expMrk;
    private PlayerStats plStats;
    [SerializeField]private bool isHit = false;

    private void Start() {
        expMrk = ExperienceMarket.Instance;
        plStats = GameManager.Instance.playerSts;
    }

    private void Update() {
        if(isHit)
            LevelUp();
    }

    private void LevelUp(){
        if (GameManager.Instance.playerMovement.IsConnected){
            if(Input.GetButtonUp("Yattack")){
                
                expMrk.LifeUp();
                AudioManager.Instance.MenuHit();
            }
            if(Input.GetButtonUp("Xattack")){
                
                expMrk.AtkUp();
                AudioManager.Instance.MenuHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isHit = true;
        GameManager.Instance.playerCombat.enabled = false;
        UIManager.Instance.EnterMarket();
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        isHit = false;
        GameManager.Instance.playerCombat.enabled = true;
        UIManager.Instance.ExitMarket();
    }

    public bool IsHit(){ return isHit; }
}
