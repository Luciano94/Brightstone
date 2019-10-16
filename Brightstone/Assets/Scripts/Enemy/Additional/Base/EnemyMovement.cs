using UnityEngine;

public class EnemyMovement : MonoBehaviour{
    [Header("Common Variables")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject sword;
    [SerializeField] protected EnemyAnimations eAnim;
    [SerializeField] private float speedSurrounding;
    [SerializeField] private float timeSurrounding;
    [SerializeField] private float deltaTimeSurrounding;
    [SerializeField] private float timeWaiting;
    private Vector3 objective;
    private Vector3 diff;
    private Vector2 playerDir;
    private Vector2 moveDir;
    private float timeLeft = 0.0f;
    private bool movingForward = false;
    private bool movingRight = false;

    const float DELTA_SPEED = 0.3f;

    private void Start(){
        StartSurrounding(); // Temporary here
        speed += Random.Range(-DELTA_SPEED, DELTA_SPEED);
    }

    public void MoveToPlayer(){
        PrepareVariables(GameManager.Instance.PlayerPos);

        eAnim.Move();
        transform.Translate(diff.normalized * speed * Time.deltaTime);

        Rotation();
    }

    public void MoveToObjective(Vector3 pos){
        PrepareVariables(pos);
        moveDir = diff.normalized;
        transform.Translate(moveDir * speed * Time.deltaTime);

        CheckForward();
        Rotation();
    }

    public void SurroundPlayer(){
        PrepareVariables(GameManager.Instance.PlayerPos);

        timeLeft -= Time.deltaTime;

        if (timeLeft < timeWaiting && moveDir != Vector2.zero){
            moveDir = Vector2.zero;
            eAnim.Idle();
            StopSurrounding();
        }
        else if (timeLeft < 0.0f){
            timeLeft = timeSurrounding + Random.Range(-deltaTimeSurrounding, deltaTimeSurrounding) + timeWaiting;
            moveDir = Random.insideUnitCircle.normalized;
            eAnim.Move();
            StartSurrounding();
        }

        transform.Translate(moveDir * speed * speedSurrounding * Time.deltaTime);

        CheckForward();
    }

    virtual public void ApplyMovementStrategy(int chaserIndex){
        PrepareVariables(GameManager.Instance.PlayerPos);
    }

    public void Relocate(){
        PrepareVariables(GameManager.Instance.PlayerPos);

        transform.Translate(-diff.normalized * speed * 0.4f  * Time.deltaTime);

        moveDir = -diff.normalized;
        CheckForward();
        Rotation();
    }

    public void RelocateArcher(float minDistanceToMoveBack){
        PrepareVariables(GameManager.Instance.PlayerPos);

        Vector2 dir = diff.normalized;

        if (diff.magnitude < minDistanceToMoveBack){
            transform.Translate(-dir * speed * Time.deltaTime);

            IsMovingForward = false;
        }
        else{
            int multiplier = 1;
            if (!movingRight) multiplier = -1;

            moveDir = new Vector2(dir.y * multiplier, dir.x);

            transform.Translate(moveDir * speed * Time.deltaTime);

            CheckForward();
        }

        Rotation();
    }

    public void MoveByHit(){
        transform.Translate(-diff.normalized * speed * 0.3f * Time.deltaTime);
    }

    public void MoveByParried(){
        transform.Translate(-diff.normalized * speed * 0.05f * Time.deltaTime);
    }

    private void Rotation(){
        playerDir = diff.normalized;
        var angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    public void PrepareVariables(Vector3 objective){
        this.objective = objective;
        diff = objective - transform.position;

        if(GameManager.Instance.PlayerPos.x > transform.position.x)
            eAnim.SetDirection(0);
        else
            eAnim.SetDirection(1);
    }

    public float DistToPlayer(){
        return diff.magnitude;
    }

    public void StartSurrounding(){
        eAnim.UpdateSpeed(speedSurrounding);
    }

    public void StopSurrounding(){
        eAnim.UpdateSpeed(1.0f);
    }

    public void StartChasing(){
        eAnim.Move();
        StopSurrounding();
    }

    public void RandomizeDirection(){
        float dir = Mathf.Round(Random.value);
        if (dir == 0.0f)
            movingRight = true;
        else
            movingRight = false;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    private void CheckForward(){
        if(GameManager.Instance.PlayerPos.x > transform.position.x){
            if (moveDir.x > 0.0f)
                IsMovingForward = true;
            else
                IsMovingForward = false;
        }
        else{
            if (moveDir.x > 0.0f)
                IsMovingForward = false;
            else
                IsMovingForward = true;
        }
    }

    public bool IsMovingForward{
        get{ return movingForward; }
        set{
            if(movingForward == value)
                return;

            movingForward = value;
            if (movingForward)
                eAnim.MovingBackwards();
            else
                eAnim.StopMovingBackwards();
        }
    }
}
