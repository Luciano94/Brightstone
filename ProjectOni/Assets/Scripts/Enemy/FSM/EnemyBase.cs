using UnityEngine;

public abstract class EnemyBase : MonoBehaviour{
    EnemyCombat enemyCombat;    

    virtual protected void Awake(){

    }

    protected void Update(){
        OnUpdate();
    }

    abstract protected void OnUpdate();

    virtual protected void OnChase(){

    }
    virtual protected void OnCloseRange(){

    }
    virtual protected void OnHit(){

    }
    virtual protected void OnNoHealth(){

    }
}