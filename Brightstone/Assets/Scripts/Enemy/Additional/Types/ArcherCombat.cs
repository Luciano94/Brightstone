using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCombat : EnemyCombat{
    [Header("Archer")]
    [SerializeField] private GameObject throwableObject;
    [SerializeField] private float distFromOrigin;
    [SerializeField] private float throwTime;

    private bool throwed = false;

    override public void Attack(){
        base.Attack();

        enemyAnim.Set8AxisDirection((int)Calculations.GetDirection(diff));
        lineRenderer.enabled = true;
    }

    override public void Attacking(){
        base.Attacking();

        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position;
        positions[1] = player;
        lineRenderer.SetPositions(positions);

        if (currentTime <= activeMoment){
            player = GameManager.Instance.PlayerPos;
            diff = player - transform.position;

            if (currentTime > 0.2f)
                enemyAnim.Set8AxisDirection((int)Calculations.GetDirection(diff));
        }
        if (currentTime > activeMoment && !active){
            active = true;
        }
        if (currentTime > throwTime && !throwed){
            if (throwableObject){
                Arrow arrow = Instantiate(throwableObject, transform.position, transform.rotation, null).GetComponent<Arrow>();
                arrow.transform.GetChild(0).Rotate(0.0f, 0.0f, Calculations.GetAngle(diff));
                arrow.SetValues(GetComponent<EnemyStats>(), diff.normalized);
                enemyAnim.Throw();
            }
            throwed = true;
        }
        if (currentTime > animTime){
            isAttacking = false;
            currentTime = 0.0f;
            active = false;
            throwed = false;

            lineRenderer.enabled = false;
        }
    }

    override protected void ResetValues(){
        base.ResetValues();

        throwed = false;
    }
}