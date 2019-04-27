using UnityEngine;

public class PlayerCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField]private GameObject weapon;
    [SerializeField]private float atckTime;
    private float actAtkTime;
    private bool isAttacking;
   
    [Header("Parry")]
    [SerializeField]private float parryTime;
    private float actParryTime;
    private bool isParryng;

    private PlayerStats plStat;

    private void Awake() {
        actAtkTime = atckTime;
        isAttacking = true;

        actParryTime = parryTime;
        isParryng = false;

        plStat = GameManager.Instance.playerSts;
    }

    void Update(){
        if(!isParryng)
            Attack();
        if(!isAttacking)
            Parry();

        if(Input.GetKeyUp(KeyCode.R)){
            plStat.Experience = 100;
            UIManager.Instance.ExpUpdate();
        }
    }

    private void Attack(){
        if(Input.GetButtonDown("Fire1")){
            if(!isAttacking){
                isAttacking = true;
                actAtkTime = 0.0f;
                weapon.SetActive(true);
            }
        }
        if(isAttacking && actAtkTime >= atckTime){
            weapon.SetActive(false);
            isAttacking = false;
        }else
            actAtkTime += Time.deltaTime; 
    }

    private void Parry(){
        if(Input.GetButtonDown("Fire2")){
            if(!isParryng){
                isParryng = true;
                actParryTime = 0.0f;
                weapon.SetActive(true);
                weapon.transform.Rotate(0,0,90);
            }
        }
        if(isParryng && actParryTime >= parryTime){
            weapon.SetActive(false);
            weapon.transform.Rotate(0,0,-90);
            isParryng = false;
        }else
            actParryTime += Time.deltaTime; 
    }
}
