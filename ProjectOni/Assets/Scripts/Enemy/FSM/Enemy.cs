using System.Collections;
using UnityEngine;

public class Enemy : EnemyBase{
    public enum States{
        Attack=0,
        Chase,
        Wait,
        Hurt,
        Death,
        Count
    }

    public enum Events{
        OnAttack=0,
        OnChase,
        OnHit,
        OnCloseRange,
        OnRestitution,
        NoHealth,
        Count
    }

    EnemyFSM fsm;

    // ===========================================================
    // Inicialization
    // ===========================================================
    private void Start(){
        fsm = new EnemyFSM((int)States.Count, (int)Events.Count, (int)States.Wait);

                                  // Origin            // Event                    // Destiny
        fsm.SetRelation( (int)States.Attack,  (int)Events.OnHit,          (int)States.Hurt   );
        fsm.SetRelation( (int)States.Attack,  (int)Events.NoHealth,       (int)States.Death  );

        fsm.SetRelation( (int)States.Chase,   (int)Events.OnAttack,       (int)States.Attack );
        fsm.SetRelation( (int)States.Chase,   (int)Events.OnHit,          (int)States.Hurt   );
        fsm.SetRelation( (int)States.Chase,   (int)Events.NoHealth,       (int)States.Death  );

        fsm.SetRelation( (int)States.Wait,    (int)Events.OnChase,        (int)States.Chase  );
        fsm.SetRelation( (int)States.Wait,    (int)Events.OnCloseRange,   (int)States.Chase  );
        fsm.SetRelation( (int)States.Wait,    (int)Events.OnHit,          (int)States.Hurt   );
        fsm.SetRelation( (int)States.Wait,    (int)Events.NoHealth,       (int)States.Death  );
        
        fsm.SetRelation( (int)States.Hurt,    (int)Events.OnRestitution,  (int)States.Chase  );
        fsm.SetRelation( (int)States.Hurt,    (int)Events.NoHealth,       (int)States.Death  );
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
                Surround();
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

    virtual protected void Surround(){

    }

    virtual protected void Chasing(){

    }

    virtual protected void Attacking(){

    }

    virtual protected void Hurt(){

    }

    void Die(){
        
    }

    virtual protected void Attack(){

    }

    // ===========================================================
    // Events
    // ===========================================================
    protected override void OnChase(){
        fsm.SendEvent((int)Events.OnChase);
    }

    protected override void OnCloseRange(){
        fsm.SendEvent((int)Events.OnCloseRange);
    }

    protected override void OnHit(){
        fsm.SendEvent((int)Events.OnHit);
    }

    protected override void OnNoHealth(){
        fsm.SendEvent((int)Events.NoHealth);
    }

    public int GetActualState(){
        return fsm.GetState();
    }
}