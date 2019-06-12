using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Vector3 player;
    [SerializeField] private GameObject sword;
    [SerializeField] private EnemyAnimations eAnim;
    private Vector3 diff;
    private Vector3 dir;
    private bool movingBackwards = false;
    private EnemyCombat enemyCombat;
    private EnemyStats enemyStats;

    private void Start(){
        player = GameManager.Instance.PlayerPos;
        enemyCombat = GetComponent<EnemyCombat>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update(){
        /*player = GameManager.Instance.PlayerPos;
        
        if(enemyCombat.IsParried)
            return;
        if(enemyCombat.IsHit){
            if(enemyStats.enemyType != EnemyType.Boss)
                MoveByHit();
            return;
        }
        if(!enemyCombat.IsAttacking){
            Movement();
            Rotation();
        }*/
    }

    public void MoveToPlayer(){
        PrepareVariables();

        transform.Translate(diff.normalized * speed * Time.deltaTime);

        Rotation();
    }

    public void SurroundPlayer(){
        PrepareVariables();

        // Here i have to move the Enemy around the Player, like making a circle, with a Delta of the distance
        //  from the Player to the Enemy so that distance is not a perfect circle. Also, the speed of the
        //  enemies is slower.
        //transform.Translate(diff.normalized * speed * Time.deltaTime);

        Rotation();
    }

    public void Relocate(){
        PrepareVariables();

        transform.Translate(-diff.normalized * speed * 0.5f  * Time.deltaTime);

        Rotation();
    }

    public void MoveByHit(){
        transform.Translate(-diff.normalized * speed * 0.3f * Time.deltaTime);
    }

    private void Rotation(){
        dir = diff.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    private void PrepareVariables(){
        player = GameManager.Instance.PlayerPos;
        diff = player - transform.position;

        if(player.x > transform.position.x)
            eAnim.SetDirection(0);
        else
            eAnim.SetDirection(1);
    }

    public bool IsMovingBackwards{
        get{ return movingBackwards; }
        set{
            movingBackwards = value;
            if (movingBackwards)
                eAnim.MovingBackwards();
            else
                eAnim.StopMovingBackwards();
        }
    }
}
