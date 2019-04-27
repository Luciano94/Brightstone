using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]private float life = 100;

    public float Life{
        get{return life;}
        set{
            if(life-value <= 0){
                //death resolve
            }else{
                life -= value;
            }
        }
    }
}
