using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField] private float animTime;
    [SerializeField] private float activeMoment;
    [SerializeField] private GameObject weapon;
    [SerializeField] private EnemyAnimations enemyAnim;
    private BoxCollider2D weaponColl;
    private float currentTime = 0.0f;
    private bool isAttacking = false;
    private bool active = false;
    private bool drawGizmos = false;
    private GameObject throwableObject;
    private float distFromOrigin;

    public bool IsAttacking{
        get { return isAttacking; }
    }

    public float AnimTime{
        get{return animTime;}
    }

    private Vector3 diff;
    private Vector3 player;

    private void Start(){
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        player = GameManager.Instance.PlayerPos;

        GetComponent<EnemyStats>().OnHit.AddListener(Hit);
        GetComponent<EnemyStats>().OnParried.AddListener(Parried);
        GetComponent<EnemyStats>().OnLowHealth.AddListener(LowHealth);
        GetComponent<EnemyStats>().OnDeath.AddListener(Death);
    }

    public void Attack(){
        isAttacking = true;
        enemyAnim.SetAttack();
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

            case EnemyType.Archer:
                drawGizmos = true;
                player = GameManager.Instance.PlayerPos;

                if (currentTime <= activeMoment){
                    diff = player - transform.position;
                    diff.z = player.z;
                }
                if (currentTime > activeMoment && !active){
                    player = GameManager.Instance.PlayerPos;
                    active = true;
                }
                if (currentTime > animTime){
                    if (throwableObject)
                    {
                        Arrow arrow = Instantiate(throwableObject, transform.position + diff.normalized * distFromOrigin, transform.rotation, null).GetComponent<Arrow>();
                        arrow.transform.LookAt(player, Vector3.up);
                        //arrow.transform.eulerAngles = new Vector3(arrow.transform.eulerAngles.x, 0.0f, arrow.transform.eulerAngles.z);
                        arrow.SetEnemyStats(GetComponent<EnemyStats>());
                    }
                    isAttacking = false;
                    currentTime = 0.0f;
                    active = false;
                    drawGizmos = false;
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

    private void OnDrawGizmos(){
        if (drawGizmos){
            RaycastHit hit;
            Gizmos.color = Color.red;
            Vector3 pos = transform.position;
            pos.z = player.z;
            if (Physics.Raycast(pos, diff, out hit, 15.0f))
                Gizmos.DrawRay(pos, hit.point);
            else
                Gizmos.DrawRay(pos, diff.normalized * 15.0f);
        }
    }
}
