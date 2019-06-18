﻿using UnityEngine;

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

    public bool IsAttacking{
        get { return isAttacking; }
    }

    private Vector2 diff;
    private Vector3 player;

    private void Start(){
        weaponColl = weapon.GetComponent<BoxCollider2D>();
        player = GameManager.Instance.PlayerPos;

        GetComponent<EnemyStats>().OnHit.AddListener(Hit);
        GetComponent<EnemyStats>().OnParried.AddListener(Parried);
    }

    public void Attack(){
        isAttacking = true;
        enemyAnim.SetAttack();
    }

    public void Attacking(){
        currentTime += Time.deltaTime;
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
    }

    private void Hit(){
        ResetValues();
        enemyAnim.Hit();

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
    }

    private void Parried(){
        ResetValues();
        enemyAnim.Hit();

        // Color by hit
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f);
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
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }
}
