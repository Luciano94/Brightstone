using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour {
    [Header("Attack")]
    [SerializeField]private float animTime;
    [SerializeField] private GameObject weapon;
    private BoxCollider2D weaponColl;
    [SerializeField]private EnemyAnimations eAnim;
    private float standTime;
    private float currentTime = 0.0f;
    private float currentTimeForBeingHit = 0.0f;
    private bool isChasing = false;
    private bool isAttaking = false;
    private bool isHit = false;
    private bool isParried = false;
    private float timeParalizedForHit = 0.15f;
    private float timeParalizedForParry = 1.0f;

    public bool IsAttacking {
        get { return isAttaking; }
    }

    public bool IsParried {
        get { return isParried; }
        set { isParried = value; }
    }

    public bool IsHit {
        get { return isHit; }
    }

    private Vector2 diff;
    private Vector3 player;

    private void Start() {
        standTime = animTime * 0.4f;
        isChasing = true;
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        player = GameManager.Instance.PlayerPos;

        GetComponent<BossStats>().OnHit.AddListener(Hit);
        GetComponent<BossStats>().OnParried.AddListener(Parried);
    }


    public void EndAttack(){
        currentTime += animTime;
    }

    private void Update() {
        if(isParried) {
            currentTimeForBeingHit += Time.deltaTime;
            if (currentTimeForBeingHit >= timeParalizedForParry) {
                currentTimeForBeingHit = 0.0f;
                isParried = false;
                isHit = false;

                // Color back to normal
                GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else return;
        }

        if(isHit) {
            currentTimeForBeingHit += Time.deltaTime;
            if (currentTimeForBeingHit >= timeParalizedForHit) {
                currentTimeForBeingHit = 0.0f;
                isHit = false;

                // Color back to normal
                GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            else return;
        }

        player = GameManager.Instance.PlayerPos;

        if(isChasing) {
            Chase();
        }
        if(isAttaking) {
            Attack();
        }
    }

    private void Chase() {
        diff = player - transform.position;
        
        if(diff.magnitude < 2.0f) {
            isChasing = false;
            isAttaking = true;
        }
    }

    private void Attack() {
        currentTime += Time.deltaTime;
        if(currentTime > standTime) {
            AudioManager.Instance.EnemyAttack();
            weapon.SetActive(true);
            weaponColl.enabled = true;
            eAnim.SetAttack();
        }
        if(currentTime > animTime) {
            weapon.SetActive(false);
            weaponColl.enabled = false;
            isAttaking = false;
            isChasing = true;
            currentTime = 0.0f;
        }
    }

    private void Hit() {
        currentTimeForBeingHit = 0.0f;
        currentTime = 0.0f;
        isChasing = true;
        if (isAttaking) {
            isAttaking = false;
            weapon.SetActive(false);
        }
        isHit = true;

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void Parried() {
        currentTimeForBeingHit = 0.0f;
        currentTime = 0.0f;
        isChasing = true;
        if (isAttaking) {
            isAttaking = false;
            weapon.SetActive(false);
        }
        isParried = true;

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }
}
