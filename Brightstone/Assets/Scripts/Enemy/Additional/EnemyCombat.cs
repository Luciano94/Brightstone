using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    enum Axis8Direction{
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

    enum Axis4Direction{
        Up,
        Right,
        Down,
        Left,
        Count
    }

    [Header("Attack")]
    [SerializeField] private float animTime;
    [SerializeField] private float activeMoment;
    [SerializeField] private float throwTime;
    [SerializeField] private GameObject weapon;
    [SerializeField] private EnemyAnimations enemyAnim;
    private BoxCollider2D weaponColl;
    private LineRenderer lineRenderer;
    private float currentTime = 0.0f;
    private bool isAttacking = false;
    private bool active = false;
    private bool throwed = false;
    private GameObject throwableObject;
    private float distFromOrigin;

    public bool IsAttacking{
        get { return isAttacking; }
    }

    public float AnimTime{
        get { return animTime; }
    }

    private Vector3 diff;
    private Vector3 player;

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

    public void Attack(EnemyType type){
        isAttacking = true;
        enemyAnim.SetAttack();
        
        player = GameManager.Instance.PlayerPos;
        diff = player - transform.position;

        if (type == EnemyType.Archer){
            enemyAnim.Set8AxisDirection((int)GetDirection(diff));
            lineRenderer.enabled = true;
            AudioManager.Instance.ArcherBowPull();
        }
        else if (type == EnemyType.Boss){
            enemyAnim.Set8AxisDirection((int)Get4AxisDirection(diff));
        }
    }

    public void Attacking(EnemyType type){
        currentTime += Time.deltaTime;
        switch(type){
            case EnemyType.Warrior:
                if(currentTime > activeMoment && !active){
                    weapon.SetActive(true);
                    weaponColl.enabled = true;
                    AudioManager.Instance.EnemyAttack();
                    active = true;
                }
                if(currentTime > animTime){
                    weapon.SetActive(false);
                    weaponColl.enabled = false;
                    isAttacking = false;
                    currentTime = 0.0f;
                    active = false;
                }
            break;

            case EnemyType.Boss:
                if (currentTime > activeMoment && !active)
                {
                    weapon.SetActive(true);
                    weaponColl.enabled = true;
                    AudioManager.Instance.EnemyAttack();
                    active = true;
                }
                if (currentTime > animTime)
                {
                    weapon.SetActive(false);
                    weaponColl.enabled = false;
                    isAttacking = false;
                    currentTime = 0.0f;
                    active = false;
                }
                break;

            case EnemyType.Archer:
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = player;
                lineRenderer.SetPositions(positions);

                if (currentTime <= activeMoment){
                    player = GameManager.Instance.PlayerPos;
                    diff = player - transform.position;

                    if (currentTime > 0.2f)
                        enemyAnim.Set8AxisDirection((int)GetDirection(diff));
                }
                if (currentTime > activeMoment && !active){
                    active = true;
                }
                if (currentTime > throwTime && !throwed){
                    if (throwableObject){
                        AudioManager.Instance.ArcherArrowThrow();
                        Arrow arrow = Instantiate(throwableObject, transform.position, transform.rotation, null).GetComponent<Arrow>();
                        arrow.transform.GetChild(0).Rotate(0.0f, 0.0f, GetAngle(diff));
                        arrow.SetValues(GetComponent<EnemyStats>(), diff.normalized);
                        enemyAnim.Throw();
                    }
                    throwed = true;
                }
                if (currentTime > animTime){
                    isAttacking = false;
                    currentTime = 0.0f;
                    active = false;
                    throwed = false;

                    lineRenderer.enabled = false;
                }
            break;
        }
        
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

    private void ResetValues(){
        currentTime = 0.0f;
        active = false;
        throwed = false;
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

    public void HasThrowableObject(GameObject throwableObject, float distFromOrigin){
        this.throwableObject = throwableObject;
        this.distFromOrigin = distFromOrigin;
    }

    private Axis8Direction GetDirection(Vector3 distance){
        float angle = Vector3.SignedAngle(distance, Vector3.up, Vector3.forward);
        
        if      (angle >  -22.5f && angle <=   22.5f)
            return Axis8Direction.Up;
        else if (angle >   22.5f && angle <=   67.5f)
            return Axis8Direction.UpRight;
        else if (angle >   67.5f && angle <=  112.5f)
            return Axis8Direction.Right;
        else if (angle >  112.5f && angle <=  157.5f)
            return Axis8Direction.DownRight;
        else if (angle >  157.5f || angle <= -157.5f)
            return Axis8Direction.Down;
        else if (angle > -157.5f && angle <= -112.5f)
            return Axis8Direction.DownLeft;
        else if (angle > -112.5f && angle <=  -67.5f)
            return Axis8Direction.Left;
        else if (angle >  -67.5f && angle <=  -22.5f)
            return Axis8Direction.UpLeft;

        return Axis8Direction.Down;
    }

    private Axis4Direction Get4AxisDirection(Vector3 distance){
        float angle = Vector3.SignedAngle(distance, Vector3.up, Vector3.forward);
        
        if      (angle >  -45.0f && angle <=   45.0f)
            return Axis4Direction.Up;
        else if (angle >   45.0f && angle <=  135.0f)
            return Axis4Direction.Right;
        else if (angle >  135.0f || angle <= -135.0f)
            return Axis4Direction.Down;
        else if (angle > -135.0f && angle <=  -45.0f)
            return Axis4Direction.Left;

        return Axis4Direction.Down;
    }

    private float GetAngle(Vector3 diff){
        float angle = Vector3.SignedAngle(diff, Vector3.up, Vector3.forward);

        if (angle >= 0.0f && angle <= 90.0f)
            return 90.0f - angle;
        else if (angle > 90.0f && angle <= 180.0f)
            return 450.0f - angle;
        else // if (angle < 0.0f && angle >= -180.0f)
            return 90.0f + Mathf.Abs(angle);
    }
}
