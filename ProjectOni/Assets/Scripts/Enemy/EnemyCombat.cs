using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {
    [Header("Attack")]
    [SerializeField]private float animTime = 0.3f;
    [SerializeField] private GameObject weapon;
    [SerializeField] private EnemyAnimations eAnim;
    private float standTime;
    private float currentTime = 0.0f;
    private bool isChasing = false;
    private bool isAttaking = false;
    private bool isHit = false;
    private float timeParalized = 0.2f;

    public bool IsAttacking {
        get { return isAttaking; }
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

        GetComponent<EnemyStats>().OnHit.AddListener(Hit);
    }

    private void Update() {
        
        if(isHit) {
            currentTime += Time.deltaTime;
            if (currentTime >= timeParalized) {
                currentTime = 0.0f;
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

    public void EndAttack(){
        currentTime += animTime;
    }

    private void Attack(){
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
        currentTime = 0.0f;
        isChasing = true;
        isAttaking = false;
        isHit = true;
    }
}
