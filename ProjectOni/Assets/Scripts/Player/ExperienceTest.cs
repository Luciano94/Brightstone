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
        if(Input.GetButtonUp("Fire3")){
            expMrk.LifeUp();
        }
        if(Input.GetButtonUp("Jump")){
            expMrk.AtkUp();
        }
    }

    private void OnTriggerEnter(Collider other) {
        isHit = true;
    }
    
    private void OnTriggerExit(Collider other) {
        isHit = false;
    }
}
