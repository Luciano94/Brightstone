using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour{
    [SerializeField] float life;
    [SerializeField] float atkDmg;
    [SerializeField] Transform numPos;
    [SerializeField] Gradient lifeColor;
    [SerializeField] float lowHealthPerc;
    [SerializeField] ShadowEffect shadow;
    private Color actualLifeColor;
    private float lifePercent;
    private float currentLife;
    private float experience = 100;
    private float lastDamageRecieved;
    private GameObject myRoom;
    private MonoBehaviour[] movSet; 
    public EnemyType enemyType;

    public class OnDeathGetEnemy : UnityEvent<EnemyBase>{};


    [HideInInspector][SerializeField] UnityEvent onHit;
    [HideInInspector][SerializeField] UnityEvent onDeath;
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
            lastDamageRecieved = value;
            if (!GameManager.Instance.isTutorial){
                RunSaver.currentRun.data.damageDealt += (uint)value;
            }
            LifePercent(value);
            if (value > 0.0f){
                DamagePopup.Create(numPos.position, (int)value, 8, actualLifeColor);

                if (currentLife > 0){
                    OnHit.Invoke();
                }
                else{
                        GameManager.Instance.playerSts.Experience = experience;
                        UIManager.Instance.ExpUpdate();
                        
                    if (!GameManager.Instance.isTutorial){
                        myRoom.GetComponent<RoomsBehaviour>().EnemyDeath(GetComponent<EnemyBase>());
                        if (enemyType == EnemyType.Boss){
                            RunSaver.currentRun.data.bossesKilled++;
                            GameManager.Instance.PlayerWin();
                        }
                        else{
                            RunSaver.currentRun.data.enemiesKilled++;
                        }
                    }
                    OnDeath.Invoke();
                    shadow.onDeath();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void LifePercent(float value){
        if (currentLife > 0){
            lifePercent = currentLife / life;
            if (lifePercent <= lowHealthPerc){
                // Acá podrías llamar a la función del EnemyBehaviour
                OnLowHealth.Invoke();
            }
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
        OnParried.Invoke();
    }

    public void LowHealth(){

    }

    public float LastDamageRecieved() { return lastDamageRecieved; }

    public UnityEvent OnHit{
        get { 
            GameManager.Instance.PlayBlood();
            return onHit;
        }
    }

    public UnityEvent OnDeath{
        get { return onDeath; }
    }

    public UnityEvent OnParried{
        get { return onParried; }
    }

    public UnityEvent OnLowHealth{
        get { return onLowHealth; }
    }
}