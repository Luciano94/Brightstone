using UnityEngine;

public abstract class EnemyBase : MonoBehaviour{
    [SerializeField] private float minDistanceToChase;
    [SerializeField] private float minDistanceToAttack;
    [SerializeField] protected float timeRelocating;
    [SerializeField] private float timeParalyzedForHit;
    [SerializeField] private float timeParalyzedForParry;
    protected EnemyMovement enemyMovement;
    protected EnemyCombat enemyCombat;
    protected EnemyStats enemyStats;
    protected Vector3 playerPos;
    protected float timeLeft = 0.0f;
    protected float timeLeftHit = 0.0f;
    protected float timeLeftParried = 0.0f;
    protected bool chasing = false;
    protected bool guardState = true;
    public int enemyIndex = -1;
    public bool feinting;
    
    public bool isMyAttackingTurn = false;

    virtual protected void Awake(){
        enemyMovement   = GetComponent<EnemyMovement>();
        enemyCombat     = GetComponent<EnemyCombat>();
        enemyStats      = GetComponent<EnemyStats>();

        enemyStats.OnHit.AddListener(Hit);
        enemyStats.OnParried.AddListener(Parried);
    }

    protected void Update(){
        playerPos = GameManager.Instance.PlayerPos;
        OnUpdate();
    }

    abstract protected void OnUpdate();

    public void Chase(){
        chasing = true;
        OnChase();
    }

    public void Hit(){
        if (enemyStats.enemyType == EnemyType.Boss)
            return;
        timeLeftHit = timeParalyzedForHit;
        OnHit();
    }

    public void Parried(){
        timeLeftParried = timeParalyzedForParry;
        OnHit();
    }

    public void ForceToGuardState(){
        chasing = false;
        timeLeft = timeRelocating * 2.0f;
        OnRelocate();
    }

    protected bool IsOnChaseRange(){
        float distance = (playerPos - transform.position).magnitude;
        if (distance < minDistanceToChase)
            return true;
        return false;
    }

    protected bool IsOnAttackRange(){
        float distance = (playerPos - transform.position).magnitude;
        if (distance < minDistanceToAttack)
            return true;
        return false;
    }

    virtual protected void OnChase(){

    }
    virtual protected void OnAttackRange(){

    }
    virtual protected void OnRelocate(){

    }
    virtual protected void OnHit(){

    }
    virtual protected void OnRestitution(){

    }
    virtual protected void OnNoHealth(){

    }

    virtual protected void OnReturnToWait(){

    }

    virtual protected void OnForceToRelocate(){

    }

    public bool IsInGuardState(){
        return guardState;
    }

    public EnemyType GetEnemyType(){
        return enemyStats.enemyType;
    }

    public float GetHP(){
        return enemyStats.Life;
    }
    public float GetMaxHP(){
        return enemyStats.MaxLife();
    }

    protected bool isInvokingAttack = false;
    public void InvokeAttackingTurn(float time = 0.0f){
        if(!isInvokingAttack){
            isInvokingAttack = true;
            Invoke("SetAttackingTurnTrue", time);
            //Debug.Log(gameObject + " attacking in " + time + " seconds");
        }
        
    }
    private void SetAttackingTurnTrue(){
        isMyAttackingTurn = true;
    }
}