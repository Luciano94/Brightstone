using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour {
    [Header("Attack")]
    [SerializeField]private float animTime = 0.6f;
    [SerializeField] private GameObject weapon;
    [SerializeField]private EnemyAnimations eAnim;
    private float standTime;
    private float currentTime = 0.0f;
    private float currentTimeForBeingHit = 0.0f;
    private bool isChasing = false;
    private bool isAttaking = false;
    private bool isHit = false;
    private bool isParried = false;
    private float timeParalizedForHit = 0.2f;
    private float timeParalizedForParry = 2.0f;

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
        standTime = animTime * 0.5f;
        isChasing = true;

        player = GameManager.Instance.PlayerPos;

        GetComponent<BossStats>().OnHit.AddListener(Hit);
        GetComponent<BossStats>().OnHit.AddListener(Parried);
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
            }
            else return;
        }

        if(isHit) {
            currentTimeForBeingHit += Time.deltaTime;
            if (currentTimeForBeingHit >= timeParalizedForHit) {
                currentTimeForBeingHit = 0.0f;
                isHit = false;
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
            weapon.SetActive(true);
            eAnim.SetAttack();
        }
        if(currentTime > animTime) {
            weapon.SetActive(false);
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
    }
}
