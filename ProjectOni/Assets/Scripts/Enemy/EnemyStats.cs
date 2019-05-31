using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour {
    [SerializeField] float life = 50;
    [SerializeField] float atkDmg = 10.0f;
    [SerializeField] Transform numPos;
    [SerializeField]Gradient lifeColor;
    private Color actualLifeColor;
    float colorPercent;
    private float currentLife;
    private float experience = 100;
    GameObject myRoom;
    public EnemyType enemyType;
    MonoBehaviour[] movSet; 

    [HideInInspector][SerializeField] UnityEvent onHit;
    [HideInInspector][SerializeField] UnityEvent onParried;

    private void Awake() {
        currentLife = life;

    }

    public GameObject MyRoom {
        get { return myRoom; }
        set { myRoom = value; }
    }

    public float Life {
        get { return currentLife; }
        set {
            currentLife -= value;
            ColorPercent(value);
            DamagePopup.Create(numPos.position, (int)value, 8, actualLifeColor);
            if (currentLife >= 0) {
                OnHit.Invoke();
            }
            else {
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath();
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                Destroy(gameObject);
            }
        }
    }

    private void ColorPercent(float value){
        colorPercent = ((value * 100f) / currentLife) * 0.01f;
        actualLifeColor =  lifeColor.Evaluate(colorPercent);
    }

    public float AtkDmg {
        get { return atkDmg; }
        set {
            if (value > 0)
                atkDmg += value;
        }
    }

    public void Hit(){
        EnemyManager.Instance.PlusPercent(enemyType);
    }

    public void Parried(){
        onParried.Invoke();
    }

    public UnityEvent OnHit
    {
        get { return onHit; }
    }

    public UnityEvent OnParried
    {
        get { return onParried; }
    }
}