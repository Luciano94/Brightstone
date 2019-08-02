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

        // Here i have to move the Enemy around the Player, like making a circle, with a Delta of the distance
        //  from the Player to the Enemy so that distance is not a perfect circle. Also, the speed of the
        //  enemies is slower.

        /*timeLeft -= Time.deltaTime;

        if (timeLeft < 0.0f){
            timeLeft = timeSurrounding + Random.Range(-deltaTimeSurrounding, deltaTimeSurrounding);
            movingRight = Random.value > 0.5f ? true : false;
        }

        Vector3 inverseDiff = transform.position - player;
        float radio = inverseDiff.magnitude;

        dir = inverseDiff.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector3 extraPos = ((radio * Mathf.Cos(angle) * Vector3.right) + (radio * Mathf.Sin(angle) * Vector3.up)) * (movingRight ? 1.0f : -1.0f);

        //transform.Translate(extraPos * surroundSpeed * Time.deltaTime);
        transform.position += extraPos * surroundSpeed * Time.deltaTime;
        */

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

    public void Relocate(){
        PrepareVariables();

        transform.Translate(-diff.normalized * speed * 0.4f  * Time.deltaTime);

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

    private void PrepareVariables(){
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

    public void SetSpeed(float speed){
        this.speed = speed;
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
