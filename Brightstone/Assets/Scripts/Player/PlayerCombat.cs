using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour{
    [SerializeField]private GameObject weapon;
    private BoxCollider2D boxWeapColl;
    private CircleCollider2D cirWeapColl;
    
    private bool isHit = false;
    private float timeParalized = 0.15f;
    private float currentTime = 0.0f;
    private PlayerMovement pMovement;

    [Header("Attack")]
    ActionInfo actionInfo;
    [HideInInspector] public Actions actualAttackAction = Actions.Blank;
    [SerializeField]private ComboManager cManager;
    [SerializeField]private float atckTime;
    [SerializeField]private ParticleSystem blood;
    public FrameData atkAnim;
    private float actAtkTime;
    public bool isAttacking {get; private set;}
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
    public bool isParrying {get; private set;}
    private bool parriedSomeone = false;
    [SerializeField]private PlayerAnimations plAnim;
    [SerializeField]private ParticleSystem sparks;

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
        boxWeapColl = weapon.GetComponent<BoxCollider2D>();
        cirWeapColl = weapon.GetComponent<CircleCollider2D>();
        pMovement = GetComponent<PlayerMovement>();
        actAtkTime = 0;
        isAttacking = false;
        isParrying = false;
        atkAnim.State = ActionState.enterFrames;
        rig = GetComponent<Rigidbody2D>();
        actParryTime = parryTime;
        actionInfo.action = null;
        blood.gameObject.SetActive(false);
        plStat = GameManager.Instance.playerSts;
    }

    private void Start(){
        PlayerStats ps =  GetComponent<PlayerStats>();
        ps.OnHit.AddListener(Hit);
        ps.OnHit.AddListener(SoundManager.Instance.PlayerDamaged);
    }

    public void StartAction(ActionInfo _actionInfo, float animToRun){
        actionInfo=_actionInfo;
        plStat.AtkDmg = actionInfo.action.Damage;
        isAttacking = true;

        plAnim.SetAttackTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z), animToRun);
    }

    public void StopAction(){
        actionInfo.action = null;
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

        if(actionInfo.action != null){
            if(!isParrying && isAttacking){
                AttackMove();
                switch (actionInfo.action.Fdata.State){
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
       // if(!isParrying)
            Attack();
       // if(!isAttacking)
            //Parry();
    }

    private void Attack(){
        if(!MenuManager.Instance.StartMenu){
            if(InputManager.Instance.GetActionBeatdown()){
                cManager.ManageAction(Actions.X);
                actualAttackAction = Actions.X;
                isStrong = false;
            }
            if(InputManager.Instance.GetActionThrust()){
                cManager.ManageAction(Actions.Y);
                actualAttackAction = Actions.Y;
                isStrong = true;
            }
            if (InputManager.Instance.GetActionShuriken()){
                cManager.ManageAction(Actions.B);
                actualAttackAction = Actions.B;
                isStrong = true;
            }
            if (InputManager.Instance.GetActionZone()){
                cManager.ManageAction(Actions.A);
                actualAttackAction = Actions.A;
                isStrong = true;
            }
        }
    }

    private void EnterState(){
        if(!needMove){
            needMove = true;
        }
        weapon.SetActive(false);
        boxWeapColl.enabled = false;
        cirWeapColl.enabled = false;
    }

    private void ActiveState(){
        weapon.SetActive(true);

        if (actionInfo.action != null){

            switch(actionInfo.colType){
                case ColliderType.Box:
                    boxWeapColl.enabled = true;
                    boxWeapColl.offset = actionInfo.offset;
                    boxWeapColl.size = actionInfo.size;
                break;

                case ColliderType.Circle:
                    cirWeapColl.enabled = true;
                    cirWeapColl.offset = actionInfo.offset;
                    cirWeapColl.radius = actionInfo.radius;
                break;
            }
        }
    }

    private void ExitState(){
        weapon.SetActive(false);
        boxWeapColl.enabled = false;
        cirWeapColl.enabled = false;
        actionInfo.action = null;
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

    public void Dash(){
        plAnim.SetDashTrigger(GameManager.GetDirection(weapon.transform.eulerAngles.z));
    }

    private void Parry(){
        if(isParrying && actParryTime >= parryTime){
            weapon.SetActive(false);
            boxWeapColl.enabled = false;
            cirWeapColl.enabled = false;
            isParrying = false;
            parriedSomeone = false;
        }else
            actParryTime += Time.deltaTime; 
    }

    public void ParriedSomeone(){
        if (!parriedSomeone){
            OnParriedSomeone().Invoke();
            parriedSomeone = true;
        }
    }

    private void Hit(){
        FilterManager.SetChromaticAberration(true);
        if (actionInfo.action)
            actionInfo.action.StopAction();
        currentTime = 0.0f;
        isAttacking = false;
        isParrying = false;
        parriedSomeone = false;
        isHit = true;
        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
        Invoke("TurnOffCA", 0.05f);
    }

    public void Death(){
        plAnim.Death();
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
        PlaySparks();
        SoundManager.Instance.PlayerParryHit();
        return onParriedSomeone;
    }

    private void PlaySparks(){
        sparks.transform.position = weapon.transform.position;
       // sparks.transform.rotation = weapon.transform.rotation;

        sparks.Play();
    }

    public void PlayBlood(){
    }
}