using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField] private GameObject weapon;
    public EnemyWeapon Weapon{
        get{return weapon.GetComponent<EnemyWeapon>();}
    }
    [SerializeField] private float atckTime;
    private float actAtkTime;
    private bool isAttacking;
    private float fireRate = 1.0f;
    private float timeSinceAtk = 0.0f;

    [SerializeField] private Vector3 player;
    private BoxCollider boxCollider;

    private void Start()
    {
        player = GameManager.Instance.PlayerPos;
        actAtkTime = atckTime;
        isAttacking = false;
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        player = GameManager.Instance.PlayerPos;
        timeSinceAtk -= Time.deltaTime;
        if (timeSinceAtk < 0.0f)
        {
            Vector2 diff = player - transform.position;
            Debug.Log(diff);
            if (diff.magnitude < 2.0f)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    actAtkTime = 0.0f;
                    weapon.SetActive(true);
                }
            }
            timeSinceAtk = fireRate;
        }

        if (isAttacking && actAtkTime > atckTime)
        {
            weapon.SetActive(false);
            isAttacking = false;
        }
        else
            actAtkTime += Time.deltaTime;

            
            
    }
}
