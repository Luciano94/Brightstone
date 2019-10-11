public class WarriorBossCombat : EnemyCombat{
    override public void Attack(){
        base.Attack();

        enemyAnim.Set8AxisDirection((int)Calculations.Get4AxisDirection(diff));
    }

    override public void Attacking(){
        base.Attacking();

        if (currentTime > activeMoment && !active){
            weapon.SetActive(true);
            weaponColl.enabled = true;
            AudioManager.Instance.EnemyAttack();
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
}
