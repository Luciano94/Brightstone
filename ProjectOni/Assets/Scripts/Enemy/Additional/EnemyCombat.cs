using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField]private float animTime;
    [SerializeField] private GameObject weapon;
    private BoxCollider2D weaponColl;
    [SerializeField] private EnemyAnimations enemyAnim;
    private float standTime;
    private float currentTime = 0.0f;
    private bool isAttacking = false;
    private bool isHit = false;
    private bool isParried = false;

    public bool IsAttacking{
        get { return isAttacking; }
    }

    public bool IsParried{
        get { return isParried; }
        set { isParried = value; }
    }
    public bool IsHit{
        get { return isHit; }
    }

    private Vector2 diff;
    private Vector3 player;

    private void Start(){
        standTime = animTime * 0.4f;
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        player = GameManager.Instance.PlayerPos;

        GetComponent<EnemyStats>().OnHit.AddListener(Hit);
        GetComponent<EnemyStats>().OnParried.AddListener(Parried);
    }

    private void Update(){
        /*if(isParried){
            currentTimeParalized += Time.deltaTime;
            if (currentTimeParalized >= timeParalizedForParry){
                currentTimeParalized = 0.0f;
                isParried = false;
                isHit = false;

                // Color back to normal
                GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else return;
        }

        if(isHit){
            currentTimeParalized += Time.deltaTime;
            if (currentTimeParalized >= timeParalizedForHit){
                currentTimeParalized = 0.0f;
                isHit = false;

                // Color back to normal
                GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else return;
        }

        player = GameManager.Instance.PlayerPos;

        if(isChasing){
            Chase();
        }
        if(isAttaking){
            Attack();
        }*/
    }

    public void Attack(){
        isAttacking = true;
    }

    public void Attacking(){
        currentTime += Time.deltaTime;
        if(currentTime > standTime){
            weapon.SetActive(true);
            weaponColl.enabled = true;
            AudioManager.Instance.EnemyAttack();
            enemyAnim.SetAttack();
        }
        if(currentTime > animTime){
            weapon.SetActive(false);
            weaponColl.enabled = false;
            isAttacking = false;
            currentTime = 0.0f;
        }
    }

    private void Hit(){
        ResetValues();

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void Parried(){
        ResetValues();

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void ResetValues(){
        currentTime = 0.0f;
        if (isAttacking){
            isAttacking = false;
            weapon.SetActive(false);
        }
    }

    public void Restitute(){
        // Color back to normal
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }
}
