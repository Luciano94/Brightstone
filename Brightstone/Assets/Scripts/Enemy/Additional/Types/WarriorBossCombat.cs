using UnityEngine;

public class WarriorBossCombat : EnemyCombat{
    [Header("Boss Warrior Variables")]
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float secondAnimTime;
    [SerializeField] private float secondActiveMoment;

    override public void Attack(){
        base.Attack();

        if (whichAttack == 0)
            enemyAnim.Set8AxisDirection((int)Calculations.Get4AxisDirection(diff));
    }

    override public void Attacking(){
        base.Attacking();

        if (whichAttack == 0){
            if (currentTime > activeMoment && !active){
                weapon.SetActive(true);
                weaponColl.enabled = true;
                //AudioManager.Instance.EnemyAttack();
                SoundManager.Instance.BossSwordAttack(gameObject);
                active = true;
            }
            if (currentTime > animTime){
                weapon.SetActive(false);
                weaponColl.enabled = false;
                isAttacking = false;
                currentTime = 0.0f;
                active = false;
            }
        }
        else{
            if (currentTime > secondActiveMoment && !active)
            {
                weapon.SetActive(true);
                circleCollider.enabled = true;
                //AudioManager.Instance.EnemyAttack();
                SoundManager.Instance.BossSwordAttack(gameObject);
                active = true;
            }
            if (currentTime > secondAnimTime)
            {
                weapon.SetActive(false);
                circleCollider.enabled = false;
                isAttacking = false;
                currentTime = 0.0f;
                active = false;
            }
        }
    }
}
