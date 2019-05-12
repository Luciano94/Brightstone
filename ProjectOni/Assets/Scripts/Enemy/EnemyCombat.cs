using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField]private float animTime = 0.3f;
    [SerializeField] private GameObject weapon;
    private float standTime;
    private float currentTime = 0.0f;
    private bool isChasing = false;
    private bool isAttaking = false;

    public bool IsAttacking{
        get{return isAttaking;}
    }

    private Vector2 diff;
    private Vector3 player;

    private void Start() {
        standTime = animTime * 0.5f;
        isChasing = true;
    }

    private void Update() {
        player = GameManager.Instance.PlayerPos;
        if(isChasing){
            Chase();
        }
        if(isAttaking){
            Attack();
        }
    }

    private void Chase(){
        diff = player - transform.position;
        if(diff.magnitude < 2.0f){
            isChasing = false;
            isAttaking = true;
        }
    }

    private void Attack(){
        currentTime += Time.deltaTime;
        if(currentTime > standTime){
            weapon.SetActive(true);
        }
        if(currentTime > animTime){
            weapon.SetActive(false);
            isAttaking = false;
            isChasing = true;
            currentTime = 0.0f;
        }
    }
}
