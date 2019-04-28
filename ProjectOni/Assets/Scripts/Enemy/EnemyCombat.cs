using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour{
    [Header("Attack")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private float atckTime;
    private float actAtkTime;
    private bool isAttacking;

    [SerializeField] private Transform player;
    private BoxCollider boxCollider;

    private void Awake()
    {
        actAtkTime = atckTime;
        isAttacking = false;

        boxCollider = GetComponentInChildren<Transform>().GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        Vector3 diff = player.position - transform.position;

        if (diff.magnitude < 2.0f)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                actAtkTime = 0.0f;
                weapon.SetActive(true);
            }
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
