using UnityEngine;

public class PlayerCombat : MonoBehaviour{
    [SerializeField]private GameObject weapon;
    
    [Header("Attack")]
    [SerializeField]private float atckTime;
    private float actAtkTime;
    private bool isAttacking;
   
    [Header("Parry")]
    [SerializeField]private float parryTime;
    private float actParryTime;
    private bool isParryng;

    public bool isParry{
        get{return isParryng;}
    }

    public bool isAttack{
        get{return isAttacking;}
    }

    private PlayerStats plStat;

    private void Awake() {

        actAtkTime = atckTime;
        isAttacking = false;

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
            }
        }
        if(isParryng && actParryTime >= parryTime){
            weapon.SetActive(false);
            isParryng = false;
        }else
            actParryTime += Time.deltaTime; 
    }
}
