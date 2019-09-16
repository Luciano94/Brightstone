using UnityEngine;

public class EnemyMovement : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 player;
    [SerializeField] private GameObject sword;
    [SerializeField] private EnemyAnimations eAnim;
    [SerializeField] private float speedSurrounding;
    [SerializeField] private float timeSurrounding;
    [SerializeField] private float deltaTimeSurrounding;
    [SerializeField] private float timeWaiting;
    private Vector3 diff;
    private Vector2 playerDir;
    private Vector2 moveDir;
    private float timeLeft = 0.0f;
    private bool movingForward = false;
    private bool movingRight = false;

    private void Start(){
        player = GameManager.Instance.PlayerPos;

        StartSurrounding(); // Temporary here
    }

    public void MoveToPlayer(){
        PrepareVariables();

        transform.Translate(diff.normalized * speed * Time.deltaTime);

        Rotation();
    }

    public void SurroundPlayer(){
        PrepareVariables();

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
        PrepareVariables();

        switch(EnemyBahaviour.Instance.warriorStrategy){
            // 1 enemy
            case WarriorStrategy.Melee11:

            break;
            case WarriorStrategy.Melee12:

            break;
            case WarriorStrategy.Melee13:

            break;
            case WarriorStrategy.Melee14:

            break;

            // 2 enemies
            case WarriorStrategy.Melee21:

            break;
            case WarriorStrategy.Melee22:

            break;
            case WarriorStrategy.Melee23:

            break;
            case WarriorStrategy.Melee24:

            break;

            // 3 enemies
            case WarriorStrategy.Melee31:

            break;
            case WarriorStrategy.Melee32:

            break;
            case WarriorStrategy.Melee33:

            break;
            case WarriorStrategy.Melee34:

            break;
        }
    }

    public void Relocate(){
        PrepareVariables();

        transform.Translate(-diff.normalized * speed * 0.4f  * Time.deltaTime);

        Rotation();
    }

    public void RelocateArcher(float minDistanceToMoveBack){
        PrepareVariables();

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

    protected void PrepareVariables(){
        player = GameManager.Instance.PlayerPos;
        diff = player - transform.position;

        if(player.x > transform.position.x)
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
        if(player.x > transform.position.x){
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
