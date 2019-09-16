using UnityEngine;

public enum AxisDirection{
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
        Count
    }

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField] protected float animTime;
    [SerializeField] protected float activeMoment;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected EnemyAnimations enemyAnim;
    protected BoxCollider2D weaponColl;
    protected LineRenderer lineRenderer;
    protected float currentTime = 0.0f;
    protected bool isAttacking = false;
    protected bool active = false;

    public bool IsAttacking{
        get { return isAttacking; }
    }

    public float AnimTime{
        get { return animTime; }
    }


    protected Vector3 diff;

    protected Vector3 player;

    private void Start(){
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer){
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }

        player = GameManager.Instance.PlayerPos;

        EnemyStats enemyStats = GetComponent<EnemyStats>();

        enemyStats.OnHit.AddListener(Hit);
        enemyStats.OnParried.AddListener(Parried);
        enemyStats.OnLowHealth.AddListener(LowHealth);
        enemyStats.OnDeath.AddListener(Death);
    }

    virtual public void Attack(){
        isAttacking = true;
        enemyAnim.SetAttack();

        diff = player - transform.position;
    }

    virtual public void Attacking(){
        currentTime += Time.deltaTime;
    }

    private void Hit(){
        ResetValues();
        enemyAnim.Hit();

        // Color by hit
        //GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void Parried(){
        ResetValues();
        enemyAnim.Hit();

        // Color by hit
        //GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void LowHealth(){
        enemyAnim.IsLowHealth();
    }

    virtual protected void ResetValues(){
        currentTime = 0.0f;
        active = false;
        if (lineRenderer)
            lineRenderer.enabled = false;
        if (isAttacking){
            isAttacking = false;
            weapon.SetActive(false);
        }
    }

    public void Restitute(){
        enemyAnim.Restore();

        // Color back to normal
        //GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void Death(){
        enemyAnim.Death();
    }
}