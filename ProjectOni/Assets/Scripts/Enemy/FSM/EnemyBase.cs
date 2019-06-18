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
    protected bool isWaiting = true;

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
        OnChase();
    }

    public void Hit(){
        timeLeft = timeParalyzedForHit;
        OnHit();
    }

    public void Parried(){
        timeLeft = timeParalyzedForParry;
        OnHit();
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

    public bool IsWaiting(){
        return isWaiting;
    }

    public EnemyType GetEnemyType(){
        return enemyStats.enemyType;
    }
}