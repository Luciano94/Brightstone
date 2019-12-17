using System.Collections;
using UnityEngine;

public class Enemy : EnemyBase{
    public enum States{
        Attack=0,
        Chase,
        Wait,
        Relocate,
        Hurt,
        Death,
        Count
    }

    public enum Events{
        OnAttack=0,
        OnAttackStop,
        OnChase,
        OnHit,
        OnRestitution,
        NoHealth,
        ForceWtState,
        ForceRctState,
        Count
    }

    private EnemyFSM fsm;

    // ===========================================================
    // Inicialization
    // ===========================================================

    protected override void Awake(){
        base.Awake();
    }

    private void Start(){
        fsm = new EnemyFSM((int)States.Count, (int)Events.Count, (int)States.Wait);

                                  // Origin             // Event                   // Destiny
        fsm.SetRelation( (int)States.Attack,   (int)Events.OnAttackStop,  (int)States.Relocate );
        fsm.SetRelation( (int)States.Attack,   (int)Events.OnHit,         (int)States.Hurt     );
        fsm.SetRelation( (int)States.Attack,   (int)Events.NoHealth,      (int)States.Death    );
        fsm.SetRelation( (int)States.Attack,   (int)Events.ForceWtState,  (int)States.Wait     );

        fsm.SetRelation( (int)States.Chase,    (int)Events.OnAttack,      (int)States.Attack   );
        fsm.SetRelation( (int)States.Chase,    (int)Events.OnHit,         (int)States.Hurt     );
        fsm.SetRelation( (int)States.Chase,    (int)Events.NoHealth,      (int)States.Death    );
        fsm.SetRelation( (int)States.Chase,    (int)Events.ForceWtState,  (int)States.Wait     );
        fsm.SetRelation( (int)States.Chase,    (int)Events.ForceRctState, (int)States.Relocate );

        fsm.SetRelation( (int)States.Wait,     (int)Events.OnChase,       (int)States.Chase    );
        fsm.SetRelation( (int)States.Wait,     (int)Events.OnHit,         (int)States.Hurt     );
        fsm.SetRelation( (int)States.Wait,     (int)Events.NoHealth,      (int)States.Death    );
        fsm.SetRelation( (int)States.Wait,     (int)Events.ForceRctState, (int)States.Relocate );

        fsm.SetRelation( (int)States.Relocate, (int)Events.OnChase,       (int)States.Chase    );
        fsm.SetRelation( (int)States.Relocate, (int)Events.OnAttack,      (int)States.Attack   );
        fsm.SetRelation( (int)States.Relocate, (int)Events.OnHit,         (int)States.Hurt     );
        fsm.SetRelation( (int)States.Relocate, (int)Events.NoHealth,      (int)States.Death    );
        fsm.SetRelation( (int)States.Relocate, (int)Events.ForceWtState,  (int)States.Wait     );
        
        fsm.SetRelation( (int)States.Hurt,     (int)Events.OnRestitution, (int)States.Chase    );
        fsm.SetRelation( (int)States.Hurt,     (int)Events.NoHealth,      (int)States.Death    );
        fsm.SetRelation( (int)States.Hurt,     (int)Events.ForceWtState,  (int)States.Wait     );
    }

    // ===========================================================
    // Virtual Methods
    // ===========================================================
    override protected void OnUpdate(){
        //Debug.Log((States)fsm.GetState());

        Updating();

        switch (fsm.GetState()){
            case (int)States.Attack:
                Attacking();
                break;
            case (int)States.Chase:
                Chasing();
                break;
            case (int)States.Wait:
                Waiting();
                break;
            case (int)States.Relocate:
                Relocating();
                break;
            case (int)States.Hurt:
                Hurt();
                break;
            case (int)States.Death:
                Die();
                break;
        }
    }

    virtual protected void Updating(){

    }

    virtual protected void Chasing(){

    }

    virtual protected void Waiting(){

    }

    virtual protected void Relocating(){

    }

    virtual protected void Attacking(){

    }

    virtual protected void Hurt(){

    }

    private void Die(){
        
    }

    // ===========================================================
    // Events
    // ===========================================================
    protected override void OnChase(){
        if(IsInGuardState()){
            enemyMovement.StartChasing();
            guardState = false;
        }
        enemyMovement.IsMovingForward = true;
        fsm.SendEvent((int)Events.OnChase);
    }

    protected override void OnAttackRange(){
        enemyMovement.IsMovingForward = true;
        fsm.SendEvent((int)Events.OnAttack);
    }

    protected override void OnRelocate(){
        enemyMovement.IsMovingForward = false;
        if (chasing)
            fsm.SendEvent((int)Events.OnAttackStop);
        else
            fsm.SendEvent((int)Events.ForceWtState);
    }

    protected override void OnHit(){
        enemyMovement.IsMovingForward = true;
        enemyMovement.PrepareVariables(playerPos);
        if(fsm.GetState() == (int)States.Wait)
            enemyMovement.StartChasing();
        fsm.SendEvent((int)Events.OnHit);
    }

    protected override void OnRestitution(){
        fsm.SendEvent((int)Events.OnRestitution);
    }

    protected override void OnNoHealth(){
        fsm.SendEvent((int)Events.NoHealth);
    }

    protected override void OnReturnToWait(){
        guardState = true;
        fsm.SendEvent((int)Events.ForceWtState);
    }

    protected override void OnForceToRelocate(){
        fsm.SendEvent((int)Events.ForceRctState);
    }

    public int GetActualState(){
        return fsm.GetState();
    }
}