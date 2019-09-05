using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour{
    [SerializeField]private GameObject weapon;
    private BoxCollider2D weaponColl;
    
    private bool isHit = false;
    private float timeParalized = 0.15f;
    private float currentTime = 0.0f;
    private PlayerMovement pMovement;

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
    private bool isParrying = false;
    private bool parriedSomeone = false;
    [SerializeField]private PlayerAnimations plAnim;

    public bool IsHit {
        get { return isHit; }
    }

    public bool isParry{
        get{return isParrying;}
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

    [HideInInspector][SerializeField] UnityEvent onParriedSomeone;

    private PlayerStats plStat;

    private void Awake() {
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        pMovement = GetComponent<PlayerMovement>();
        actAtkTime = 0;
        isAttacking = false;
        atkAnim.State = ActionState.enterFrames;
        rig = GetComponent<Rigidbody2D>();

        actParryTime = parryTime;

        plStat = GameManager.Instance.playerSts;
    }

    private void Start(){
        GetComponent<PlayerStats>().OnHit.AddListener(Hit);
    }

    public void StartAction(Action _action, float animToRun){
        action=_action;
        plStat.AtkDmg = action.Damage;
        isAttacking = true;

        plAnim.SetAttackTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z), animToRun);
    }

    public void StopAction(){
        action = null;
        needMove = false;
        isAttacking = false;
    }

    void Update(){
        if(isHit){
            currentTime += Time.deltaTime;
            if (currentTime >= timeParalized){
                currentTime = 0.0f;
                isHit = false;

                // Color back to normal
                GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else return;
        }

        if(action != null){
            if(!isParrying && isAttacking){
                AttackMove();
                switch (action.Fdata.State){
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
        if(!isParrying)
            Attack();
        if(!isAttacking)
            Parry();
    }

    private void Attack(){
        if(!MenuManager.Instance.StartMenu){
          if(Input.GetButtonDown("Xattack")){
                cManager.ManageAction(Actions.X);
                isStrong = false;
            }
            if(Input.GetButtonDown("Yattack")){
                cManager.ManageAction(Actions.Y);
                isStrong = true;
            }
        }
    }

    private void EnterState(){
        if(!needMove){
            needMove = true;
        }
        weapon.SetActive(false);
        weaponColl.enabled = false;
    }

    private void ActiveState(){
        weapon.SetActive(true);
        weaponColl.enabled = true;
    }

    private void ExitState(){
        weapon.SetActive(false);
        weaponColl.enabled = false;
        action = null;
        needMove = false;
        isAttacking = false;
    }

    private void AttackMove(){
        if(needMove){
            rig.AddForce(dir * speed * Time.deltaTime,ForceMode2D.Impulse);
            needMove = false;
        }else{
            Vector2 diff = weapon.transform.position - transform.position;
            dir = diff.normalized;
        }
    }

    private void Parry(){
        if(Input.GetButtonDown("Parry")){
            if (GameManager.Instance.isTutorial)
                if (!GameManager.Instance.IsReadyToParry())
                    return;
            if(!isParrying){
                RunSaver.currentRun.data.timesParried++;
                isParrying = true;
                actParryTime = 0.0f;
                weapon.SetActive(true);
                weaponColl.enabled = true;
                plAnim.SetParryTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z));
            }
        }
        if(isParrying && actParryTime >= parryTime){
            weapon.SetActive(false);
            weaponColl.enabled = false;
            isParrying = false;
            parriedSomeone = false;
        }else
            actParryTime += Time.deltaTime; 
    }

    public void ParriedSomeone(){
        if (!parriedSomeone){
            OnParriedSomeone().Invoke();
            parriedSomeone = true;
            RunSaver.currentRun.data.goodParry++;
        }
    }

    private void Hit(){
        FilterManager.SetChromaticAberration(true);
        if (action)
            action.StopAction();
        currentTime = 0.0f;
        isAttacking = false;
        isParrying = false;
        parriedSomeone = false;
        isHit = true;
        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
        Invoke("TurnOffCA", 0.05f);
    }

    public void TurnOffCA(){
        FilterManager.SetChromaticAberration(false);
    }

    private void OnDisable(){
        isAttacking = false;
        isParrying = false;
        parriedSomeone = false;
    }

    public UnityEvent OnParriedSomeone(){
        return onParriedSomeone;
    }
}