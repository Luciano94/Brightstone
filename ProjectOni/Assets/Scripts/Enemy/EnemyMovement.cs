using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Vector3 player;
    [SerializeField] private GameObject sword;
    private Vector3 diff;
    private Vector3 dir;

    private void Start() {
        player = GameManager.Instance.PlayerPos;
    }

    private void Update() {
        player = GameManager.Instance.PlayerPos;
        if(!GetComponent<EnemyCombat>().IsAttacking){
            Movement();
            Rotation();
        }
    }

    private void Rotation() {
        diff = player - transform.position;
        dir = diff.normalized;
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    private void Movement() {

        diff = player - transform.position;
        float dist = diff.magnitude;
        if(dist > 1.5f)
            transform.Translate(diff.normalized * speed * Time.deltaTime); 
    }
}
