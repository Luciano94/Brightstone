using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour{
    [SerializeField] float life;
    [SerializeField] float atkDmg;
    [SerializeField] Transform numPos;
    [SerializeField] Gradient lifeColor;
    [SerializeField] float lowHealthPerc;
    private Color actualLifeColor;
    private float lifePercent;
    private float currentLife;
    private float experience = 100;
    private GameObject myRoom;
    private MonoBehaviour[] movSet; 
    public EnemyType enemyType;

    [HideInInspector][SerializeField] UnityEvent onHit;
    [HideInInspector][SerializeField] UnityEvent onParried;
    [HideInInspector][SerializeField] UnityEvent onLowHealth;

    private void Awake(){
        currentLife = life;
    }

    public GameObject MyRoom{
        get { return myRoom; }
        set { myRoom = value; }
    }

    public float Life{
        get { return currentLife; }
        set {
            currentLife -= value;
            RunSaver.currentRun.data.damageDealt += (uint)value;
            LifePercent(value);
            DamagePopup.Create(numPos.position, (int)value, 8, actualLifeColor);
            if (currentLife >= 0){
                OnHit.Invoke();
            }
            else{
                myRoom.GetComponent<RoomsBehaviour>().EnemyDeath(gameObject);
                GameManager.Instance.playerSts.Experience = experience;
                UIManager.Instance.ExpUpdate();
                if (enemyType == EnemyType.Boss){
                    RunSaver.currentRun.data.bossesKilled++;
                    GameManager.Instance.PlayerWin();
                }
                else{
                    RunSaver.currentRun.data.enemiesKilled++;
                }
                Destroy(gameObject);
            }
        }
    }

    private void LifePercent(float value){
        if (currentLife > 0){
            lifePercent = currentLife / life;
            if (lifePercent <= lowHealthPerc)
                OnLowHealth.Invoke();
        }
        else
            lifePercent = 0.01f;
        actualLifeColor =  lifeColor.Evaluate(lifePercent);
    }

    public float MaxLife() { return life; }

    public float AtkDmg{
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

    public void LowHealth(){

    }

    public UnityEvent OnHit{
        get { return onHit; }
    }

    public UnityEvent OnParried{
        get { return onParried; }
    }

    public UnityEvent OnLowHealth{
        get { return onLowHealth; }
    }
}