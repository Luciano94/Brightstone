using UnityEngine;

public class EnemyStats : MonoBehaviour {
    [SerializeField] float life = 50;
    [SerializeField] float atkDmg = 10.0f;
    private float experience = 100;
    GameObject myRoom;

    public GameObject MyRoom{
        get{return myRoom;}
        set{myRoom = value;}
    }

    public float Life {
        get { return life; }
        set {
            if (life > 0)
                life -= value;
            else{
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath();
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                Destroy(gameObject);
            }
        }
    }

    public float AtkDmg {
        get { return atkDmg; }
        set {
            if (value > 0)
                atkDmg += value;
        }
    }
}