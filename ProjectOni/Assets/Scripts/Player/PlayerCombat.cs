using UnityEngine;

public struct FrameData{
    public float enterFrames;
    public float activeFrames;
    public float exitFrames;
    public int State;
}

public class PlayerCombat : MonoBehaviour{
    [SerializeField]private GameObject weapon;
    
    [Header("Attack")]
    [SerializeField]private float atckTime;
    [SerializeField]private int comboCount = 3;
    private int actualCombo = 0;
    public FrameData atkAnim;
    private float actAtkTime;
    private bool isAttacking;
    [Header("Attack Move")]
    [SerializeField]private float speed; 
	private Vector2 dir;
	private Vector3 targetPos;
    private Rigidbody2D rig;

    private bool needMove = false;
   
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

        actAtkTime = 0;
        isAttacking = false;
        atkAnim.enterFrames = 0.4f;
        atkAnim.activeFrames = 0.4f;
        atkAnim.exitFrames = atckTime;
        atkAnim.State = 0;

        rig = GetComponent<Rigidbody2D>();

        actParryTime = parryTime;
        isParryng = false;

        plStat = GameManager.Instance.playerSts;
    }

    void Update(){
        if(!isParryng && isAttacking){
            AttackMove();
            switch (atkAnim.State)
            {
                case 0:
                    EnterState();
                break;
                case 1:
                    ActiveState();
                break;
                case 2:
                    ExitState();
                break;
            }
        }
        if(!isParryng)
            Attack();
        if(!isAttacking)
            Parry();
    }

    private void Attack(){
        if(Input.GetButtonDown("Fire1") && !isAttacking){
            isAttacking = true;
            atkAnim.State = 0;
        }
    }

    private void EnterState(){
        actAtkTime += Time.deltaTime;
        if(!needMove){
            needMove = true;
        }
        if(actAtkTime >= atkAnim.enterFrames){
            atkAnim.State ++;
        } 
    }

    private void ActiveState(){
        actAtkTime += Time.deltaTime;
        weapon.SetActive(true);
        if(Input.GetButtonDown("Fire1") && actualCombo < comboCount){
            actualCombo++;
            actAtkTime = 0;
            atkAnim.State = 0;
            weapon.SetActive(false);
        }
        if(actAtkTime >= atkAnim.activeFrames + atkAnim.enterFrames){
            atkAnim.State++;
        }
    }

    private void ExitState(){
        actAtkTime += Time.deltaTime;
        if(actAtkTime >= atckTime){
            atkAnim.State = 3;
            actAtkTime = 0;
            actualCombo =0;
            weapon.SetActive(false);
            isAttacking = false;
        }
    }

    private void AttackMove(){
        if(needMove){
            rig.AddForce(dir * speed * Time.deltaTime,ForceMode2D.Impulse);
            needMove = false;
        }else{
            Vector2 hola = weapon.transform.position - transform.position;
            dir = hola.normalized;
        }
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

/*            if(!isAttacking){
                isAttacking = true;
                actAtkTime = 0.0f;
                weapon.SetActive(true);
            }
        }
        if(isAttacking && actAtkTime > ){
            weapon.SetActive(false);
            isAttacking = false;
        }else
            actAtkTime += Time.deltaTime;  */