using UnityEngine;

public class PlayerCombat : MonoBehaviour{
    [SerializeField]private GameObject weapon;
    
    [Header("Attack")]
    Action action = null;
    [SerializeField]private ComboManager cManager;
    [SerializeField]private float atckTime;
    public FrameData atkAnim;
    private float actAtkTime;
    private bool isAttacking;
    private bool isStrong;
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
    [SerializeField]private PlayerAnimations plAnim;

    public bool isParry{
        get{return isParryng;}
    }

    public bool isAttack{
        get{return isAttacking;}
    }

    public ActionState State{
        get{return atkAnim.State;}
    }

    public float ActualTime{
        get{return actAtkTime;}
    }

    private PlayerStats plStat;

    private void Awake() {

        actAtkTime = 0;
        isAttacking = false;
        atkAnim.State = ActionState.enterFrames;
        rig = GetComponent<Rigidbody2D>();

        actParryTime = parryTime;
        isParryng = false;

        plStat = GameManager.Instance.playerSts;
    }

    public void StartAction(Action _action){
        action=_action;
        plStat.AtkDmg = action.Damage;
        isAttacking = true;
    }

    public void StopAction(){
        action = null;
        needMove = false;
        isAttacking = false;
    }

    void Update(){
        if(action != null){
            if(!isParryng && isAttacking){
                AttackMove();
                switch (action.Fdata.State)
                {
                    case ActionState.enterFrames:
                        EnterState();
                    break;
                    case ActionState.activeFrames:
                        ActiveState();
                    break;
                    case ActionState.exitFrames:
                        ExitState();
                    break;
                }
            }
        }
        if(!isParryng)
            Attack();
        if(!isAttacking)
            Parry();
    }

    private void Attack(){
        if(Input.GetButtonDown("Fire1")){
            cManager.ManageAction(Actions.X);
            isStrong = false;
        }
        if(Input.GetButtonDown("Fire2")){
            cManager.ManageAction(Actions.Y);
            isStrong = true;
        }
    }

    private void EnterState(){
        if(!needMove){
            needMove = true;
        }
        weapon.SetActive(false);
    }

    private void ActiveState(){
        weapon.SetActive(true);
        plAnim.SetAttackTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z), isStrong);
    }

    private void ExitState(){
        weapon.SetActive(false);
        action = null;
        needMove = false;
        isAttacking = false;
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
        if(Input.GetButtonDown("Jump")){
            if(!isParryng){
                isParryng = true;
                actParryTime = 0.0f;
                weapon.SetActive(true);
                plAnim.SetParryTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z));
            }
        }
        if(isParryng && actParryTime >= parryTime){
            weapon.SetActive(false);
            isParryng = false;
        }else
            actParryTime += Time.deltaTime; 
    }

}

/*
    private void Awake() {

        actAtkTime = 0;
        isAttacking = false;
        atkAnim.State = ActionState.enterFrames;

        rig = GetComponent<Rigidbody2D>();

        actParryTime = parryTime;
        isParryng = false;

        plStat = GameManager.Instance.playerSts;
    }

    public void StartAttack(Action act){
        actAtkTime = 0;
        atkAnim.enterFrames = act.FData.enterFrames;
        atkAnim.activeFrames = act.FData.activeFrames;
        atkAnim.exitFrames = act.FData.exitFrames;
        atkAnim.State = act.FData.State;
        isAttacking = true;
    }

    public void StopAttack(){
        weapon.SetActive(false);
        actAtkTime = 0;
        isAttacking = false;
    }

    void Update(){
        if(!isParryng && isAttacking){
            AttackMove();
            switch (atkAnim.State)
            {
                case ActionState.enterFrames:
                    EnterState();
                break;
                case ActionState.activeFrames:
                    ActiveState();
                break;
                case ActionState.exitFrames:
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
            cManager.ManageAction(Actions.X);
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
        if(actAtkTime >= atkAnim.activeFrames + atkAnim.enterFrames){
            atkAnim.State = ActionState.exitFrames;
        }
    }

    private void ExitState(){
        actAtkTime += Time.deltaTime;
        weapon.SetActive(false);
        if(actAtkTime >= atckTime){
            //cManager.EndCombo();
            atkAnim.State = ActionState.exitFrames;
            actAtkTime = 0;
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
 */